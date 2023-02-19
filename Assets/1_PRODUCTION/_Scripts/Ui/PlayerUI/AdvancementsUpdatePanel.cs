using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

using akb.Core.Managing;
using akb.Core.Managing.UpdateSystem;

namespace akb.Core.Managing.UI
{
    public class AdvancementsUpdatePanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        enum SinnerSoulCostPerTier
        {
            Tier1 = 3,
            Tier2 = 5,
            Tier3 = 8
        }

        [Header("Set in inspector")]
        [SerializeField] AdvancementType typeOfAdvancement;
        [SerializeField] Image abilityImage;
        [SerializeField] Image buttonImage;
        [SerializeField] TextMeshProUGUI lvl;

        bool pointerDown;

        float pointerDownTimer;

        float requiredHoldTime = 2f;

        int currentlvl = 0;

        [SerializeField] int lvlsCanUpgade;

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            pointerDown = true;
            buttonImage.color = Color.grey;
        }

        //Detect if clicks are no longer registering
        public void OnPointerUp(PointerEventData pointerEventData)
        {
            buttonImage.color = Color.white;

            if (currentlvl <= lvlsCanUpgade)
            {
                if (lvlsCanUpgade == 2)
                {
                    if (currentlvl == 0)
                    {
                        Reset();

                        abilityImage.fillAmount = 0;
                    }
                    else if (currentlvl == 1)
                    {
                        Reset();

                        abilityImage.fillAmount = 0.50f;
                    }
                }

                if (lvlsCanUpgade == 3)
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
                    else if (currentlvl == 2)
                    {
                        Reset();

                        abilityImage.fillAmount = 0.66f;
                    }
                }
            }
        }

        private void Update()
        {
            if (pointerDown)
            {
                pointerDownTimer += Time.deltaTime;

                if (pointerDownTimer > requiredHoldTime)
                {
                    if (lvlsCanUpgade == 2)
                    {
                        if (currentlvl == 0 && ManagerHUB.GetManager.CurrencyHandler.GetSinnerSouls > (int)SinnerSoulCostPerTier.Tier1)
                        {
                            currentlvl++;

                            ManagerHUB.GetManager.CurrencyHandler.DecreaseSinnerSoulsBy((int)SinnerSoulCostPerTier.Tier1);
                            lvl.text = currentlvl + " / " + 2;

                            ManagerHUB.GetManager.AdvancementHandler.AdvanceTierOf(typeOfAdvancement);

                            Reset();
                        }
                        else if (currentlvl == 1 & ManagerHUB.GetManager.CurrencyHandler.GetSinnerSouls > (int)SinnerSoulCostPerTier.Tier2)
                        {
                            currentlvl++;
                            ManagerHUB.GetManager.CurrencyHandler.DecreaseSinnerSoulsBy((int)SinnerSoulCostPerTier.Tier2);

                            ManagerHUB.GetManager.AdvancementHandler.AdvanceTierOf(typeOfAdvancement);

                            lvl.text = currentlvl + " / " + 2;

                            Reset();
                        }
                    }

                    if (lvlsCanUpgade == 3)
                    {
                        if (currentlvl == 0 && ManagerHUB.GetManager.CurrencyHandler.GetSinnerSouls > (int)SinnerSoulCostPerTier.Tier1)
                        {
                            currentlvl++;

                            ManagerHUB.GetManager.CurrencyHandler.DecreaseSinnerSoulsBy((int)SinnerSoulCostPerTier.Tier1);
                            lvl.text = currentlvl + " / " + 3;

                            ManagerHUB.GetManager.AdvancementHandler.AdvanceTierOf(typeOfAdvancement);

                            Reset();
                        }
                        else if (currentlvl == 1 & ManagerHUB.GetManager.CurrencyHandler.GetSinnerSouls > (int)SinnerSoulCostPerTier.Tier2)
                        {
                            currentlvl++;
                            ManagerHUB.GetManager.CurrencyHandler.DecreaseSinnerSoulsBy((int)SinnerSoulCostPerTier.Tier2);

                            ManagerHUB.GetManager.AdvancementHandler.AdvanceTierOf(typeOfAdvancement);

                            lvl.text = currentlvl + " / " + 3;

                            Reset();
                        }
                        else if (currentlvl == 2 & ManagerHUB.GetManager.CurrencyHandler.GetSinnerSouls > (int)SinnerSoulCostPerTier.Tier3)
                        {
                            currentlvl++;
                            ManagerHUB.GetManager.CurrencyHandler.DecreaseSinnerSoulsBy((int)SinnerSoulCostPerTier.Tier3);

                            ManagerHUB.GetManager.AdvancementHandler.AdvanceTierOf(typeOfAdvancement);

                            lvl.text = currentlvl + " / " + 3;

                            Reset();
                        }
                    }

                    Reset();
                }

                if (lvlsCanUpgade == 2)
                {
                    if (currentlvl == 0)
                    {
                        //The interpolation value between the two floats.
                        float t = pointerDownTimer / requiredHoldTime;
                        abilityImage.fillAmount = Mathf.Lerp(0, 0.5f, t);
                    }

                    if (currentlvl == 1)
                    {
                        //The interpolation value between the two floats.
                        float t = pointerDownTimer / requiredHoldTime;
                        abilityImage.fillAmount = Mathf.Lerp(0.5f, 1, t);
                    }
                }

                if (lvlsCanUpgade == 3)
                {
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
        }

        private void Reset()
        {
            pointerDown = false;
            pointerDownTimer = 0;
        }
    }
}