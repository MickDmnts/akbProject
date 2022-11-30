using System.Collections;
using UnityEngine;

using AKB.Entities.Interactions;

namespace AKB.Entities.Player.SpearHandling
{
    enum TravelDirection
    {
        Thrown,
        Recall,
    }

    public class SpearHandler : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField, Tooltip("The initial spear travel speed.")] float spearSpeed;
        [SerializeField] AnimationCurve spearRetractionCurve;
        [SerializeField] float spearHitRadius = 1f;
        [SerializeField] LayerMask demonsHitLayer;

        //Public accessors
        public Collider SpearCollider { get; private set; }
        public Rigidbody SpearRB { get; private set; }

        PlayerSpearThrow playerSpearThrow;
        IEnumerator runningBehaviour;

        float maxTravelTime;
        float currentTravelTime;

        bool inTransit = false;
        TravelDirection currentTravelDirection;

        #region STARTUP_SETUP
        private void Awake()
        {
            CacheNeededComponents();

            SpearRB.isKinematic = true;
        }

        /// <summary>
        /// Call to cache the needed script components.
        /// </summary>
        void CacheNeededComponents()
        {
            SpearCollider = GetComponent<BoxCollider>();
            SpearRB = GetComponent<Rigidbody>();
        }
        #endregion

        #region EXTERNALLY_CALLED
        /// <summary>
        /// Call to set the max travel time and initial travel speed of the spear.
        /// </summary>
        public void StartSpearThrowSimulation(float spearSpeed, float maxTravelTime, PlayerSpearThrow playerSpearThrow)
        {
            this.spearSpeed = spearSpeed;
            this.maxTravelTime = maxTravelTime;
            this.playerSpearThrow = playerSpearThrow;

            inTransit = true;

            runningBehaviour = SimulateSpearTravel();
            StartCoroutine(runningBehaviour);
        }
        #endregion

        //Change to async/await if possible
        IEnumerator SimulateSpearTravel()
        {
            currentTravelDirection = TravelDirection.Thrown;

            //Simulate spear air travel
            while (SpearRB.isKinematic)
            {
                transform.position += transform.forward * spearSpeed * Time.deltaTime;
                currentTravelTime += 0.1f;

                if (currentTravelTime > maxTravelTime)
                {
                    break;
                }
                yield return null;
            }

            //Simulate spear drop.
            SpearCollider.isTrigger = false;
            SpearRB.isKinematic = false;
            SpearRB.AddForce(transform.forward * spearSpeed, ForceMode.VelocityChange);

            //Rotate ONLY when the spear has not collided with anything
            while (SpearRB.constraints != RigidbodyConstraints.FreezeAll)
            {
                TiltSpearTowards(transform.rotation, new Vector3(25f, 0f, 0f));
                yield return new WaitForEndOfFrame();
            }

            StopCoroutine(runningBehaviour);
        }

        /// <summary>
        /// Call to tilt the spear GameObject a bit forward.
        /// </summary>
        void TiltSpearTowards(Quaternion currentRotation, Vector3 rotation, float lerpTime = 2f)
        {
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, currentRotation * Quaternion.Euler(rotation.x, rotation.y, rotation.z), lerpTime * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            SpearRB.constraints = RigidbodyConstraints.FreezeAll;

            if (!inTransit) return;

            if (currentTravelDirection == TravelDirection.Thrown)
            {
                StartCoroutine(HitSurroundings(playerSpearThrow.GetSpearThrowDamage()));
            }
            else
            {
                StartCoroutine(HitSurroundings(playerSpearThrow.GetSpearRecallDamage()));
            }
        }

        IEnumerator HitSurroundings(int damageValue)
        {
            yield return new WaitForFixedUpdate();

            Collider[] hits = new Collider[50];
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, spearHitRadius, hits, demonsHitLayer.value);

            for (int i = 0; i < hitCount; i++)
            {
                if (hits[i] == null) continue;

                IInteractable interactable;

                if (hits[i].TryGetComponent<IInteractable>(out interactable))
                {
                    interactable.AttackInteraction(damageValue);
                }

                yield return null;
            }
        }

        #region SPEAR_RETRACTING
        public void StartSpearRetraction(Transform spearRecallPoint)
        {
            runningBehaviour = SimulateSpearRetraction(spearRecallPoint);
            currentTravelDirection = TravelDirection.Recall;

            StartCoroutine(runningBehaviour);
        }

        IEnumerator SimulateSpearRetraction(Transform spearRecallPoint)
        {
            currentTravelDirection = TravelDirection.Recall;

            //face the caller
            transform.forward = spearRecallPoint.position - transform.position;

            SpearRB.constraints = RigidbodyConstraints.None;
            SpearRB.AddForce(Vector3.up * 5f, ForceMode.VelocityChange);

            SpearRB.isKinematic = true;
            SpearCollider.isTrigger = true;

            while (true)
            {
                TiltSpearTowards(transform.rotation, new Vector3(0f, 0f, 0f));
                transform.position = Vector3.Lerp(transform.position, spearRecallPoint.position, spearRetractionCurve.Evaluate((transform.position - spearRecallPoint.position).magnitude) * spearSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, spearRecallPoint.position) <= 2f)
                {
                    gameObject.SetActive(false);

                    inTransit = false;
                    break;
                }

                yield return null;
            }

            StopCoroutine(runningBehaviour);
        }

        public void StopSpearRetraction()
        {
            StopCoroutine(runningBehaviour);

            runningBehaviour = StopSpearRetractionSimulation();
            StartCoroutine(runningBehaviour);
        }

        IEnumerator StopSpearRetractionSimulation()
        {
            SpearCollider.isTrigger = false;
            SpearRB.isKinematic = false;
            SpearRB.AddForce(transform.forward * 5f, ForceMode.VelocityChange);

            while (SpearRB.constraints != RigidbodyConstraints.FreezeAll)
            {
                TiltSpearTowards(transform.rotation, new Vector3(25f, 0f, 0f));
                yield return null;
            }

            StopCoroutine(runningBehaviour);
        }
        #endregion

        private void OnDisable()
        {
            StopAllCoroutines();

            if (playerSpearThrow != null)
                playerSpearThrow.EnableSpearThrowing();
        }
    }
}