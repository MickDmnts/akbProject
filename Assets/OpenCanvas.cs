using System.Collections;
using System.Collections.Generic;
using akb.Core.Managing;
using UnityEngine;

public class OpenCanvas : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.enabled = false;
        }
    }
}
