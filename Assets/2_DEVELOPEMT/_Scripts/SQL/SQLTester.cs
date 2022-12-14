using AKB.Core.Managing;
using UnityEngine;

namespace AKB.Core.Database
{
    public class SQLTester : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("Updating health value");
                GameManager.GetManager.Database.UpdatePlayerHealthValue(120, 0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("Getting health value");
                Debug.Log(GameManager.GetManager.Database.GetPlayerHealthValue(0));
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Updating last room value");
                GameManager.GetManager.Database.UpdateLastRoom(2, 0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("Getting last room value");
                Debug.Log(GameManager.GetManager.Database.GetLastRoom(0));
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                string jsonStr = "TODO";
                GameManager.GetManager.Database.UpdateUnusedRooms(jsonStr, 0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Debug.Log(GameManager.GetManager.Database.GetUnusedRooms(0));
            }
        }
    }
}