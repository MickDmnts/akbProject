using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PersistentAdvancements : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool pointerDown;

    float pointerDownTimer;

    float requiredHoldTime = 2f;

    int currentlvl = 0;

    [SerializeField] Image abilityImage;

    [SerializeField] TextMeshProUGUI lvl;

    //[SerializeField] UnityEvent onHold;


    public void OnPointerDown(PointerEventData pointerEventData)
    {
        pointerDown = true;
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (currentlvl == 0)
        {
            Reset();

            abilityImage.fillAmount = 0;
        }
        else if (currentlvl == 1)
        {
            Reset();

            abilityImage.fillAmount = 0.33f;
        }
        else if(currentlvl == 2)
        {
            Reset();

            abilityImage.fillAmount = 0.66f;
        }
    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;

            if (pointerDownTimer > requiredHoldTime)
            {
                //if (onHold != null)
                //{
                //    onHold.Invoke();
                //}

                if (currentlvl == 0)
                {
                    currentlvl++;
                    lvl.text = currentlvl + " / " + 3;

                    Reset();
                }
                else if(currentlvl == 1)
                {
                    currentlvl++;
                    lvl.text = currentlvl + " / " + 3;

                    Reset();
                }
                else if (currentlvl == 2)
                {
                    currentlvl++;
                    lvl.text = currentlvl + " / " + 3;

                    Reset();
                }
            }

            if (currentlvl == 0)
            {
                //The interpolation value between the two floats.
                float t = pointerDownTimer / requiredHoldTime;
                abilityImage.fillAmount = Mathf.Lerp(0, 0.33f, t);
            }
            if (currentlvl == 1)
            {
                //The interpolation value between the two floats.
                float t = pointerDownTimer / requiredHoldTime;
                abilityImage.fillAmount = Mathf.Lerp(0.33f, 0.66f, t);
            }
            if (currentlvl == 2)
            {
                //The interpolation value between the two floats.
                float t = pointerDownTimer / requiredHoldTime;
                abilityImage.fillAmount = Mathf.Lerp(0.66f, 1, t);
            }
        }
    }

    public void Testing()
    {
        Debug.Log("testing");
    }


    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
    }
}
