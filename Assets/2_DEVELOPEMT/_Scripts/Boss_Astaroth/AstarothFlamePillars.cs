using UnityEngine;

using akb.Core.Managing;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothFlamePillars : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] GameObject pillarGo;
        [SerializeField] float pillarCd;

        bool canSpawn = false;
        float currentInterval;

        private void Start()
        {
            canSpawn = false;

            ManagerHUB.GetManager.GameEventsHandler.onAstarothThirdPhase += StartPillarSpawn;
            ManagerHUB.GetManager.GameEventsHandler.onAstarothDeath += StopPillarSpawn;
        }

        void StartPillarSpawn()
        {
            canSpawn = true;
        }

        void StopPillarSpawn()
        {
            canSpawn = false;
        }

        private void Update()
        {
            if (!canSpawn) { return; }

            currentInterval += Time.deltaTime;
            if (currentInterval >= pillarCd)
            {
                SpawnPillar();
                currentInterval = 0f;
            }
        }

        void SpawnPillar()
        {
            Vector3 playerPos = ManagerHUB.GetManager.PlayerEntity.transform.position;

            Instantiate(pillarGo, playerPos, pillarGo.transform.rotation);
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onAstarothThirdPhase -= StartPillarSpawn;
            ManagerHUB.GetManager.GameEventsHandler.onAstarothDeath -= StopPillarSpawn;
        }
    }
}