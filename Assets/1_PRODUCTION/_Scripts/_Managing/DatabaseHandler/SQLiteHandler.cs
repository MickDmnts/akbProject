using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

namespace AKB.Core.Database
{
    [DefaultExecutionOrder(0)]
    public class SQLiteHandler : MonoBehaviour
    {
        private string dbPath = "";

        private void Awake()
        {
            if (dbPath == "")
            {
                dbPath = "URI=file:" + Application.dataPath + "/Resources/akbPlayerDb.db";

#if UNITY_STANDALONE && !UNITY_EDITOR
            dbPath = "URI=file:" + Application.dataPath + "/akbPlayerDb.db";
#endif
            }

            //Sinner souls setup
            CreateSoulsTables();
            SetupSinnerSoulsDB();

            //Advancement Types
            CreateAdvancementsTypesTable();
            SetupAdvancementsTypesDB();

            //Persistend advancements table
            CreateAdvancementsTable();
            SetupAdvancementsDB();
        }

        #region SINNER_SOULS
        void CreateSoulsTables()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS SINNER_SOULS (
                                                SoulsValue INTEGER PRIMARY KEY
                                                                 NOT NULL
                                            );";

                    int result = command.ExecuteNonQuery();
                    Debug.LogFormat("Sinner Souls table creation result: {0}", result);
                }
            }
        }

        void SetupSinnerSoulsDB()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"INSERT OR IGNORE INTO SINNER_SOULS (SoulsValue)
                                                VALUES
                                                    (0);";

                    int result = command.ExecuteNonQuery();
                    Debug.LogFormat("Sinner Souls table setup: {0}", result);
                }
            }
        }
        #endregion

        #region PERSISTENT_ADVANCEMENT_TYPES
        void CreateAdvancementsTypesTable()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS PERS_ADV_TYPES (
                                                Type ADVANCEMENT (1) PRIMARY KEY
                                                                   UNIQUE
                                                                   NOT NULL
                                            );";

                    int result = command.ExecuteNonQuery();
                    Debug.LogFormat("Persistent Types table creation result: {0}", result);
                }
            }
        }

        void SetupAdvancementsTypesDB()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"INSERT OR IGNORE INTO PERS_ADV_TYPES (Type)
                                            VALUES  ('DEVIL_RAGE'),
                                                    ('DODGE'),
                                                    ('HEALTH'),
                                                    ('MELEE'),
                                                    ('SPEAR_THROW'),
                                                    ('TP_CHARGE');";

                    int result = command.ExecuteNonQuery();
                    Debug.LogFormat("Persistent Types table setup: {0}", result);
                }
            }
        }
        #endregion

        #region PERSISTENT_ADVANCEMENTS
        void CreateAdvancementsTable()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS PERS_ADVANCEMENTS (
                                                Advancement ADVANCEMENT (1) PRIMARY KEY
                                                                            UNIQUE
                                                                            REFERENCES PERS_ADV_TYPES (Type) 
                                                                            NOT NULL,
                                                Tier        INTEGER (1)     NOT NULL
                                                                            DEFAULT (0),
                                                IsUnlocked  INTEGER (1)     NOT NULL
                                                                            DEFAULT (0) 
                                            );";

                    int result = command.ExecuteNonQuery();
                    Debug.LogFormat("Advancements table creation result: {0}", result);
                }
            }
        }

        void SetupAdvancementsDB()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"INSERT OR IGNORE INTO PERS_ADVANCEMENTS ( IsUnlocked, Tier, Advancement)
                                            VALUES  (0, 0, 'DEVIL_RAGE'),
                                                    (0, 0, 'DODGE'),
                                                    (0, 0, 'HEALTH'),
                                                    (0, 0, 'MELEE'),
                                                    (0, 0, 'SPEAR_THROW'),
                                                    (0, 0, 'TP_CHARGE');";

                    int result = command.ExecuteNonQuery();
                    Debug.LogFormat("Advancements table setup: {0}", result);
                }
            }
        }
        #endregion
    }
}