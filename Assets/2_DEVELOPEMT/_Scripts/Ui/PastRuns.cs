using System.Collections.Generic;
using UnityEngine;

namespace akb.Core.Managing.UI
{
    public class PastRuns : MonoBehaviour
    {
        [SerializeField] GameObject pastRunsPrefab;
        [SerializeField] GameObject pastRunsContainer;

        [SerializeField]
        List<int> numbers = new List<int>();

        // Start is called before the first frame update
        void Start()
        {
            foreach (int num in numbers)
            {
                GameObject pastRunsGameobject = Instantiate(pastRunsPrefab);
                pastRunsGameobject.transform.SetParent(pastRunsContainer.transform, false);
                pastRunsGameobject.transform.localScale = Vector3.one;
            }
        }
    }
}