using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace akb.Entities.Interactions
{
    /// <summary>
    /// A class used to transfer inpector given data to the FOV manager in runtime.
    /// </summary>
    [System.Serializable]
    class DetectorValue
    {
        public string detectorName;
        public Vector3 facing;
        public Color radiusColor;
        public Color frustrumColor;
        public float maxAngle;
        public float maxRadius;
    }

    /* [CLASS DOCUMENTATION]
     * 
     * Inspector variables: Detector values must be set from the inspector so detectors can be constructed.
     * Private variables: These variables change throughout the game.
     * 
     * [Must know]
     * 1. The manager creates every detector passed in the inspectorData list and stores it in a dictionary along with its GameObject anchor object
     *      so we have GameObject - Detector pairs.
     * 2. Detectors are updated through this manager.
     */
    public class EntityAttackFOV : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] List<DetectorValue> inspectorData;
        [SerializeField] LayerMask layersToDetectColliders;

        public bool IsDetectorActive { get; private set; }

        #region PRIVATE_VARIABLES
        Entity parentEntity;

        Dictionary<GameObject, DetectorValue> gameObjectDetectors;

        List<AttackFOVDetector> detectors;
        #endregion

        private void Awake()
        {
            parentEntity = GetComponentInParent<Entity>();
            gameObjectDetectors = new Dictionary<GameObject, DetectorValue>();
        }

        private void Start()
        {
            //Initialize the detectors list
            detectors = new List<AttackFOVDetector>();

            InitializeHoldersAndDetectors();

            ActivateDetectors();

            //Mark the detectors as active.
            IsDetectorActive = true;
        }

        /// <summary>
        /// Call to create a detector parent gameObject with the inspector given (DetectorName) and assign it every detector passed from the inpector.
        /// </summary>
        void InitializeHoldersAndDetectors()
        {
            foreach (DetectorValue detector in inspectorData)
            {
                //Create the anchor
                GameObject tempGO = new GameObject
                {
                    name = detector.detectorName
                };
                tempGO.transform.SetParent(transform);
                tempGO.transform.position = transform.position;

                //Init the detector data
                InitializeDetector(detector, tempGO.transform);

                //Add the detector and its anchor to the dictionar.
                gameObjectDetectors.Add(tempGO, detector);
            }
        }

        void ActivateDetectors()
        {
            foreach (AttackFOVDetector detector in detectors)
            {
                detector.IsEnabled(true);
            }
        }

        /// <summary>
        /// Call to create a AIEntityFOVDetector instance and add it as a value to the detectors list.
        /// </summary>
        /// <param name="inspectorData">The data given from the inspector to construct the detector.</param>
        /// <param name="parent">The anchor of the detector.</param>
        void InitializeDetector(DetectorValue inspectorData, Transform parent)
        {
            AttackFOVDetector tempDetector = new AttackFOVDetector(this, layersToDetectColliders, parent, inspectorData.radiusColor,
                inspectorData.frustrumColor, inspectorData.maxAngle, inspectorData.maxRadius, inspectorData.facing);

            detectors.Add(tempDetector);
        }

        public List<Transform> GetHitsInsideFrustrum(int detectorInpectorID)
        {
            return detectors[detectorInpectorID].InitiateDetection();
        }

        /// <summary>
        /// Call to get THIS FOV managers' enemy entity.
        /// </summary>
        /// <returns></returns>
        public Entity GetParentEntity()
        { return parentEntity; }

        #region VISUALIZATION
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!EditorApplication.isPlaying)
            {
                if (inspectorData.Count <= 0) return;

                foreach (DetectorValue inspectorSetup in inspectorData)
                {
                    if (inspectorData == null) continue;

                    //Draw the detection radius around the enemy
                    Handles.color = inspectorSetup.radiusColor;
                    Handles.DrawWireDisc(transform.position, Vector3.up, inspectorSetup.maxRadius);

                    //Create the left and right linesOfSight
                    Vector3 fovLineFront1 = Quaternion.AngleAxis(inspectorSetup.maxAngle, Vector3.up) * transform.forward * inspectorSetup.maxRadius;
                    Vector3 fovLineFront2 = Quaternion.AngleAxis(-inspectorSetup.maxAngle, Vector3.up) * transform.forward * inspectorSetup.maxRadius;

                    //Draw the FOV
                    Gizmos.color = inspectorSetup.frustrumColor;
                    Gizmos.DrawRay(transform.position, fovLineFront1);
                    Gizmos.DrawRay(transform.position, fovLineFront2);

                    //Draw the middle black ray
                    Gizmos.color = Color.black;
                    Gizmos.DrawRay(transform.position, transform.forward * inspectorSetup.maxRadius);

                    //Find the angle on a disc to visualize the editor facing values.
                    //So we can see where the detector will face in edit time.
                    Vector3 start = transform.position;
                    float angle = inspectorSetup.facing.y;
                    float radiants = angle * Mathf.Deg2Rad;

                    float x = Mathf.Cos(radiants) * inspectorSetup.maxRadius + transform.position.x;
                    float y = transform.position.y;
                    float z = Mathf.Sin(radiants) * inspectorSetup.maxRadius + transform.position.z;

                    Vector3 end = new Vector3(x, y, z);

                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(start, end);
                }
            }
            else
            {
                if (detectors.Count <= 0) return;

                foreach (AttackFOVDetector detector in detectors)
                {
                    detector.OnDrawGizmos();
                }
            }
        }
#endif
        #endregion
    }
}