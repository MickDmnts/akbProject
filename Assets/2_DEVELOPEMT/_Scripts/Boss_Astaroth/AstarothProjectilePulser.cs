using System.Collections;
using UnityEngine;
using akb.Core.Sounds;
using akb.Core.Managing;


namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothProjectilePulser : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] Transform core;
        [SerializeField] GameObject projectileGo;
        [SerializeField] float sphereRadius;
        [SerializeField] float spawnInterval = 5f;

        [SerializeField, HideInInspector] BoundPos[] boundPositions = new BoundPos[36]; // 360/10 = 36;

        bool canSpawn = false;
        float currentInterval;

        struct BoundPos
        {
            public Vector3 position;
            public Quaternion rotation;
        }

        private void Start()
        {
            ManagerHUB.GetManager.GameEventsHandler.onAstarothThirdPhase += EnableSpawning;
            ManagerHUB.GetManager.GameEventsHandler.onAstarothDeath += DisableSpawning;

            Transform core = this.core;
            GameObject hint = new GameObject("Hint");
            hint.transform.SetParent(core);
            hint.transform.position = core.position;
            hint.transform.position += new Vector3(sphereRadius, 0f, 0f);

            for (int i = 0; i < boundPositions.Length; i++)
            {
                core.Rotate(Vector3.up, i * 10);
                boundPositions[i].position = hint.transform.position;
                boundPositions[i].rotation = core.rotation;
            }
        }

        void EnableSpawning()
        {
            canSpawn = true;
        }

        void DisableSpawning()
        {
            canSpawn = false;
        }

        private void Update()
        {
            if (!canSpawn) { return; }

            currentInterval += Time.deltaTime;
            if (currentInterval >= spawnInterval)
            {
                StartCoroutine(SpawnProjectiles());
                currentInterval = 0f;
            }
        }

        IEnumerator SpawnProjectiles()
        {
            foreach (BoundPos projPos in boundPositions)
            {
                Instantiate(projectileGo, projPos.position, projPos.rotation);
                ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.BossFireOrbs);

                yield return new WaitForSeconds(0.001f);
            }

            yield return null;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            foreach (BoundPos pos in boundPositions)
            {
                Gizmos.DrawWireSphere(pos.position, 0.5f);
            }
        }
#endif

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onAstarothThirdPhase -= EnableSpawning;
            ManagerHUB.GetManager.GameEventsHandler.onAstarothDeath -= DisableSpawning;
        }
    }
}