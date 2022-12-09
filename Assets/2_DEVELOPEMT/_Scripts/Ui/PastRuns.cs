using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastRuns : MonoBehaviour
{
    [SerializeField] GameObject pastRunsPrefab;
    [SerializeField] GameObject pastRunsContainer;

    [SerializeField]
    List<int> numbers = new List<int>();

    //den to deixnei otan pataw play alla doulebei

    // Start is called before the first frame update
    void Start()
    {
        foreach (int num in numbers)
        {
            GameObject pastRunsGameobject = Instantiate(pastRunsPrefab);
            pastRunsGameobject.transform.SetParent(pastRunsContainer.transform);
            pastRunsGameobject.transform.localScale = Vector3.one;
        }
    }
}
