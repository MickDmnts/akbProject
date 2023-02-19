using akb.Core.Sounds;
using akb.Core.Managing;
using UnityEngine;

public class PressurePadScript : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform spawn;
    [SerializeField] float spawnTime = 0.5f;
    [SerializeField] float timeTillSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Spawn();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        timeTillSpawn += Time.deltaTime;
        if (timeTillSpawn >= spawnTime)
        {
            ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.ProjectileShoot);
            Instantiate(projectile, spawn.position, spawn.rotation);
            timeTillSpawn = 0;
        }
    }
}
