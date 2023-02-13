using System.Collections;
using UnityEngine;

using akb.Core.Managing;
using akb.Core.Managing.PCG;

public class EntryToRoomTransit : MonoBehaviour
{
    enum TransistorType
    {
        HubToLevels,
        LevelToLevel,
        LevelsToHub
    }

    [Header("Select transistor type")]
    [SerializeField] TransistorType transistorType;
    [SerializeField] GameObject portalActivatedGFX;

    Collider transistorCollider;
    IEnumerator activeBehaviour;

    GameObject particleEffectCache;

    private void Awake()
    {
        transistorCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        if (particleEffectCache != null) Destroy(particleEffectCache.gameObject);
        particleEffectCache = Instantiate(portalActivatedGFX);
        particleEffectCache.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TransitBehaviour();
        }
    }

    void TransitBehaviour()
    {
        switch (transistorType)
        {
            case TransistorType.HubToLevels:
                {
                    ManagerHUB.GetManager.UIManager.EnablePanel("GamePlayScreenPanel");
                    ManagerHUB.GetManager.LevelManager.LoadNext(false);
                }
                break;

            case TransistorType.LevelToLevel:
                {
                    activeBehaviour = CreateRoomAndMovePlayer();
                    StartCoroutine(activeBehaviour);
                }
                break;

            case TransistorType.LevelsToHub:
                {
                    ManagerHUB.GetManager.LevelManager.TransitToHub();
                    ManagerHUB.GetManager.UIManager.EnablePanel("GamePlayScreenPanel");
                }
                break;

            default:
                {
                    Debug.LogWarning("Invalid type");
                }
                break;
        }
    }

    IEnumerator CreateRoomAndMovePlayer()
    {
        RoomWorld activeWorld = ManagerHUB.GetManager.RoomSelector.GetActiveWorld;

        GameObject nextRoom = ManagerHUB.GetManager.RoomSelector.PlaceNextRoom(activeWorld);
        Vector3 nextRoomEntry = nextRoom.GetComponent<RoomData>().GetRoomEntryPoint().position;

        //Move the player
        ManagerHUB.GetManager.PlayerEntity.PlayerMovement.TeleportEntity(nextRoomEntry);
        ManagerHUB.GetManager.GameEventsHandler.OnFadeOut();

        //Waits until the player is properly moved
        while (ManagerHUB.GetManager.PlayerEntity.transform.position != nextRoomEntry)
        { yield return null; }

        ManagerHUB.GetManager.GameEventsHandler.OnNextRoomEntry();
    }

    private void OnDestroy()
    {
        if (particleEffectCache != null) Destroy(particleEffectCache.gameObject);
    }
}
