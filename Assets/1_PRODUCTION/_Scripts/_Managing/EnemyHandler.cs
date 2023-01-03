using UnityEngine;

using akb.Core.Managing;

namespace akb.Entities.AI
{
    public class EnemyHandler : MonoBehaviour
    {
        private void Start()
        {
            ManagerHUB.GetManager.GameEventsHandler.onEnemyEntryUpdate += UpdateEnemyEntry;
        }

        ///<summary>Updates the database entry of the passed enemy ID to "found".
        /// <para>Raised through onEnemyEntryUpdate event.</para>
        /// </summary>
        void UpdateEnemyEntry(int enemyID)
        {
            if (GameManager.GetManager.Database.GetIsMonsterFoundValue(GameManager.GetManager.ActiveFileID, enemyID) != 0)
            {
                GameManager.GetManager.Database.UpdateIsMonsterFound(GameManager.GetManager.ActiveFileID, enemyID, true);
                Debug.Log($"Updated entry for monster id {enemyID} to is found");
            }
            else
            {
                Debug.Log($"Monster with ID {enemyID} is already found");
            }
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onEnemyEntryUpdate -= UpdateEnemyEntry;
        }

        //TODO
        //Spawn enemies based on room index
        //with the help of the room data enemy spawn pairs
    }
}