using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace akb.Core.Managing.UI
{
    public class AdvancementsUpdatePanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        bool pointerDown;

        float pointerDownTimer;

        float requiredHoldTime = 2f;

        int currentlvl = 0;

        [SerializeField] Image abilityImage;

        [SerializeField] Image buttonImage;

        [SerializeField] TextMeshProUGUI lvl;

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            pointerDown = true;

            buttonImage.color = Color.grey;

        }

        //Detect if clicks are no longer registering
        public void OnPointerUp(PointerEventData pointerEventData)
        {
            buttonImage.color = Color.white;

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
            else if (currentlvl == 2)
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
                    if (currentlvl == 0 && GameManager.GetManager.Souls > 3)
                    {
                        currentlvl++;
                        GameManager.GetManager.Souls = GameManager.GetManager.Souls - 3;
                        lvl.text = currentlvl + " / " + 3;
                        Debug.Log(GameManager.GetManager.Souls);
                        Reset();
                    }
                    else if (currentlvl == 1 & GameManager.GetManager.Souls > 5)
                    {
                        currentlvl++;
                        GameManager.GetManager.Souls = GameManager.GetManager.Souls - 5;
                        lvl.text = currentlvl + " / " + 3;
                        Debug.Log(GameManager.GetManager.Souls);
                        Reset();
                    }
                    else if (currentlvl == 2 & GameManager.GetManager.Souls > 8)
                    {
                        currentlvl++;
                        GameManager.GetManager.Souls = GameManager.GetManager.Souls - 8;
                        lvl.text = currentlvl + " / " + 3;
                        Debug.Log(GameManager.GetManager.Souls);
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

        private void Reset()
        {
            pointerDown = false;
            pointerDownTimer = 0;
        }
    }
}