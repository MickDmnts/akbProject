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
                SQLiteHandler.UpdatePlayerHealthValue(120, 0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("Getting health value");
                Debug.Log(SQLiteHandler.GetPlayerHealthValue(0));
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Updating last room value");
                SQLiteHandler.UpdateLastRoom(2, 0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("Getting last room value");
                Debug.Log(SQLiteHandler.GetLastRoom(0));
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                string jsonStr = "TODO";
                SQLiteHandler.UpdateUnusedRooms(jsonStr, 0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Debug.Log(SQLiteHandler.GetUnusedRooms(0));
            }
        }
    }
}