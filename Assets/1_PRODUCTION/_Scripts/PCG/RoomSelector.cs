using UnityEngine;

namespace akb.Core.Managing.PCG
{
    public class RoomSelector : MonoBehaviour
    {
        const int ENTRY_LEVEL = 0;
        const int BATTLE_MIN = 1;
        const int BATTLE_MAX = 6;
        const int HEAL_LEVEL = 7;

        [Header("Coins needed for store room")]
        [SerializeField] int coinsNeeded = 50;

        //Player values
        private int playerCurrentHealth = 0;
        private int playerMaxHealth = 0;

        int accumulatedCoins = 0;

        int currentLevel = 0;

        RoomDataContainer roomDataContainer;
        GameObject previousRoom;

        private void Start()
        {
            roomDataContainer = FindObjectOfType<RoomDataContainer>();
            ManagerHUB.GetManager.SetRoomSelector(this);

            ManagerHUB.GetManager.GameEventsHandler.onPlayerHubEntry += ResetPCG;
        }

        void ResetPCG()
        {
            currentLevel = 0;
            playerCurrentHealth = 0;
            playerMaxHealth = 0;
        }

        #region PCG
        ///<summary>Call to place the next generated room in the game world and get the rooms entry point in return.</summary>
        public Vector3 PlaceNextRoom(RoomWorld roomWorld)
        {
            if (previousRoom != null)
                Destroy(previousRoom);

            RoomData nextRoom = SelectNextRoom(roomWorld);

            GameObject roomGO = Instantiate(nextRoom.GetRoomPrefab(), new Vector3(currentLevel * 100, 0f, 0f), Quaternion.identity);
            roomGO.gameObject.SetActive(true);
            Vector3 spawnPos = roomGO.GetComponent<RoomData>().GetRoomEntryPoint().position;

            previousRoom = roomGO;

            return spawnPos;
        }

        /// <summary>
        /// Determines the next room.
        /// </summary>
        RoomData SelectNextRoom(RoomWorld roomWorld)
        {
            RoomData nextRoom;

            UpdateHealthValues();
            UpdateCoinValues();

            //Entry room
            if (currentLevel == ENTRY_LEVEL)
            {
                Debug.Log("Entered entry sequence");

                currentLevel++;

                nextRoom = roomDataContainer.GetRoomData(roomWorld, RoomType.Entry);
            }
            //Battle - store - Heal rooms
            else if (currentLevel >= BATTLE_MIN && currentLevel <= BATTLE_MAX)
            {
                Debug.Log("Entered Battle sequence");

                if (IsHealthAboveFiftyPercent(playerCurrentHealth, playerMaxHealth))
                {
                    if (HasEnoughMoney(accumulatedCoins, coinsNeeded))
                    {
                        currentLevel++;

                        nextRoom = GetRoomBasedOnPercentages(roomWorld, 25, 20, 65);
                    }
                    else
                    {
                        currentLevel++;

                        nextRoom = GetRoomBasedOnPercentages(roomWorld, 75, 25, 0);
                    }
                }
                else
                {
                    currentLevel++;

                    nextRoom = GetRoomBasedOnPercentages(roomWorld, 15, 55, 30);
                }
            }
            //Quaranted heal before boss
            else if (currentLevel == HEAL_LEVEL)
            {
                Debug.Log("Entered heal sequence");

                currentLevel++;

                if (playerCurrentHealth == playerMaxHealth)
                {
                    nextRoom = GetRoomBasedOnPercentages(roomWorld, 0, 0, 100);
                }
                else
                {
                    nextRoom = GetRoomBasedOnPercentages(roomWorld, 0, 100, 0);
                }
            }
            //Boss room final level
            else
            {
                Debug.Log("Entered Boss sequence");

                currentLevel = 0;
                nextRoom = roomDataContainer.GetRoomData(roomWorld, RoomType.Boss);

                roomDataContainer.ResetWorlds();
            }

            return nextRoom;
        }

        /// <summary>
        /// Checks if the passed value is above 50%
        /// </summary>
        /// <returns></returns>
        bool IsHealthAboveFiftyPercent(float value, float maxValue)
        {
            if (value > 0.5 * maxValue)
            {
                return true;
            }
            return false;
        }

        bool HasEnoughMoney(int accumulatedGold, int moneyNeeded)
        {
            return accumulatedGold >= moneyNeeded ? true : false;
        }


        RoomData GetRoomBasedOnPercentages(RoomWorld roomWorld, int battleRoomChance, int healRoomChance, int storeRoomChace)
        {
            int randomInt = Random.Range(1, 101);

            if (randomInt <= battleRoomChance)
            {
                return roomDataContainer.GetRoomData(roomWorld, RoomType.Battle);
            }
            else if (randomInt > battleRoomChance && randomInt <= (healRoomChance + battleRoomChance))
            {
                return roomDataContainer.GetRoomData(roomWorld, RoomType.Heal);
            }
            else
            {
                return roomDataContainer.GetRoomData(roomWorld, RoomType.Store);
            }
        }

        /// <summary>
        /// Updates health values
        /// </summary>
        void UpdateHealthValues()
        {
            playerCurrentHealth = (int)ManagerHUB.GetManager.PlayerEntity.GetPlayerHealth();
            playerMaxHealth = (int)ManagerHUB.GetManager.PlayerEntity.GetPlayerMaxHealth();
        }

        void UpdateCoinValues()
        {
            //This should feed the values from the currency system during the run
            accumulatedCoins = ManagerHUB.GetManager.CurrencyHandler.GetHellCoins;
        }

        public RoomWorld GetActiveWorld => roomDataContainer.ActiveWorld;

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onPlayerHubEntry -= ResetPCG;
        }
        #endregion
    }
}