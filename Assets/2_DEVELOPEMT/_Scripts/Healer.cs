using UnityEngine;
using akb.Core.Managing;
using akb.Core.Sounds;
public class Healer : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField] GameObject healEffect;
    [SerializeField] int healAmount = 15;
    [SerializeField] float healInterval;

    bool canHeal = false;
    float currentInterval;

    private void OnTriggerEnter(Collider other)
    {
        canHeal = true;
    }

    private void Update()
    {
        if (!canHeal) { return; }

        currentInterval += Time.deltaTime;
        if (currentInterval >= healInterval)
        {
            ManagerHUB.GetManager.PlayerEntity.IncrementPlayerHealthBy(healAmount);
            ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.HealthReceived);
            Transform playerTransform = ManagerHUB.GetManager.PlayerEntity.transform;
            GameObject effect = Instantiate(healEffect, playerTransform.position, Quaternion.identity);

            currentInterval = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canHeal = false;
    }
}
