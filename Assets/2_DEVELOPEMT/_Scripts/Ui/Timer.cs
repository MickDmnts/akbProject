using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;

    float startTime = 0;

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;
        timer.SetText(startTime.ToString());
    }
}
