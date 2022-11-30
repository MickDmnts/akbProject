using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace AKB.Entities.Interactions
{
    /* [CLASS DOCUMENTATION]
     * 
     * Every variable present in the class is private.
     * 
     * [Must know]
     * 1. Each detector is handled from its AIEntityFOVManager instance.
     * 2. Multiple detectors can be constructed in a gameObject.
     * 3. Every detector value can be set in its creation, further modification is not currently present.
     *      A. External modifications will be available in future updates.
     */
    [System.Serializable]
    public class AttackFOVDetector
    {
        #region PRIVATE_VARIABLES
        Transform detector;
        float maxAngle;
        float maxRadius;
        bool isEnabled = false;

        EntityAttackFOV detectorHandler;
        LayerMask layersToDetectColliders;

        Color radiusColor;
        Color frustrumColor;
        #endregion

        /// <summary>
        /// Call to construct a Detector object.
        /// </summary>
        /// <param name="detectorHandler">This detectors' manager.</param>
        /// <param name="origin">The center of the detector</param>
        /// <param name="radiusColor">The detectors' radius color. (Alpha set to 255f)</param>
        /// <param name="frustrumColor">The detectors' drustrum color. (Alpha set to 255f)</param>
        /// <param name="maxAngle">The detectors' max frustrum angle (ie. 45 = 90 degrees.)</param>
        /// <param name="maxRadious">The detectors' radius.</param>
        /// <param name="rotation">The detector's facing direction.</param>
        public AttackFOVDetector(EntityAttackFOV detectorHandler, LayerMask layersToDetectColliders,
            Transform origin, Color radiusColor, Color frustrumColor,
            float maxAngle, float maxRadious, Vector3 rotation)
        {
            this.detectorHandler = detectorHandler;
            this.layersToDetectColliders = layersToDetectColliders;

            this.detector = origin;

            this.detector.rotation = Quaternion.Euler(rotation);

            radiusColor.a = 255f;
            frustrumColor.a = 255f;

            this.radiusColor = radiusColor;
            this.frustrumColor = frustrumColor;

            this.maxAngle = maxAngle;
            this.maxRadius = maxRadious;

            IsEnabled(true);
        }

        public List<Transform> InitiateDetection()
        {
            return GetHitsInFOV(detector, layersToDetectColliders, maxAngle, maxRadius);
        }

        /// <summary>
        /// Call to check for collisions inside the specified frustrum.
        /// </summary>
        /// <param name="observer">The center of the raycast.</param>
        /// <param name="maxAngle">The detector angle.</param>
        /// <param name="maxRadius">The detector radius.</param>
        /// <returns>A list of valid collisions based on the specified LayerMasks.</returns>
        private List<Transform> GetHitsInFOV(Transform observer, LayerMask layersToCheckHits, float maxAngle, float maxRadius)
        {
            List<Transform> validHits = new List<Transform>();

            Collider[] overlaps = new Collider[50];
            int count = Physics.OverlapSphereNonAlloc(observer.position, maxRadius, overlaps, layersToCheckHits.value);

            for (int i = 0; i < count + 1; i++)
            {
                if (overlaps[i] == null) continue;

                Vector3 dirBetween = (overlaps[i].transform.position - observer.position).normalized;
                float angle = Vector3.Angle(observer.forward, dirBetween);

                if (angle <= maxAngle)
                {
                    Ray ray = new Ray(observer.position, overlaps[i].transform.position - observer.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, maxRadius))
                    {
                        if (hit.transform.Equals(overlaps[i].transform))
                        {
                            validHits.Add(overlaps[i].transform);
                        }
                    }
                }
            }

            return validHits;
        }

        /// <summary>
        /// Call to set the detector active state to the passed value.
        /// </summary>
        /// <param name="state"></param>
        public void IsEnabled(bool state)
        {
            this.isEnabled = state;
        }

        #region FUTURE_EXPANSION
        /// <summary>
        /// *For future expansion*
        /// <para>Call to set THIS detectors' new radius value.</para>
        /// </summary>
        public void SetRadius(float newRadiusValue)
        {
            this.maxRadius = newRadiusValue;
        }

        /// <summary>
        /// *For future expansion*
        /// <para>Call to set THIS detectors' new frustrum angle value.</para>
        /// </summary>
        public void SetFrustrumAngle(float newFrustrumAngle)
        {
            this.maxAngle = newFrustrumAngle; ;
        }
        #endregion

        #region VISUALIZATION
#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            if (!isEnabled) return;

            //Draw the detection radius around the enemy
            Handles.color = radiusColor;
            Handles.DrawWireDisc(detector.transform.position, Vector3.up, maxRadius);

            //Create the left and right linesOfSight
            Vector3 fovLineFront1 = Quaternion.AngleAxis(maxAngle, Vector3.up) * detector.forward * maxRadius;
            Vector3 fovLineFront2 = Quaternion.AngleAxis(-maxAngle, Vector3.up) * detector.forward * maxRadius;

            //Draw the FOV
            Gizmos.color = frustrumColor;
            Gizmos.DrawRay(detector.position, fovLineFront1);
            Gizmos.DrawRay(detector.position, fovLineFront2);

            Gizmos.color = Color.green;

            //Draw the middle black ray
            Gizmos.color = Color.black;
            Gizmos.DrawRay(detector.position, detector.forward * maxRadius);
        }
#endif
        #endregion
    }
}