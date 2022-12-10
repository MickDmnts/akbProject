using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPAPanel : MonoBehaviour
{
    [SerializeField] GameObject persistentAdvancementsPanel;

    // Start is called before the first frame update
    void Start()
    {
        persistentAdvancementsPanel.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            persistentAdvancementsPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            persistentAdvancementsPanel.SetActive(false);
        }
    }
}
