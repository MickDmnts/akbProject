using UnityEngine;
using akb.Core.Managing;

namespace akb.Core.Database
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

            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                GameManager.GetManager.Database.EraseDataFromFile(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                GameManager.GetManager.Database.SetLastUsedFileID(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                Debug.Log(GameManager.GetManager.Database.GetLastUsedFileID());
            }

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                GameManager.GetManager.Database.UpdateSoulsValue(5, 0);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log(GameManager.GetManager.Database.GetSoulsValue(0));
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                GameManager.GetManager.Database.UpdateIsMonsterFound(0, 5, false);
                GameManager.GetManager.Database.UpdateIsMonsterFound(0, 2, true);
                GameManager.GetManager.Database.UpdateIsMonsterFound(0, 3, false);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log(GameManager.GetManager.Database.GetIsMonsterFoundValue(0, 5));
                Debug.Log(GameManager.GetManager.Database.GetIsMonsterFoundValue(0, 2));
                Debug.Log(GameManager.GetManager.Database.GetIsMonsterFoundValue(0, 3));
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(GameManager.GetManager.Database.GetMonsterDescription(0, 5));
                Debug.Log(GameManager.GetManager.Database.GetMonsterDescription(0, 2));
                Debug.Log(GameManager.GetManager.Database.GetMonsterDescription(0, 3));
            }
        }
    }
}