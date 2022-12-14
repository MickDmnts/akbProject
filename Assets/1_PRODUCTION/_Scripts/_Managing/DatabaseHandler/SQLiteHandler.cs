using System.Data;
using System.Text;
using System;
using UnityEngine;
using Mono.Data.Sqlite;

namespace AKB.Core.Database
{
    public class SQLiteHandler
    {
        private static string dbPath = "";

        public SQLiteHandler()
        {
            OnCostruction();
        }

        private void OnCostruction()
        {
            if (dbPath == "")
            {
                dbPath = "URI=file:" + Application.dataPath + "/Resources/akbPlayerDb.db";

#if UNITY_STANDALONE && !UNITY_EDITOR
            dbPath = "URI=file:" + Application.dataPath + "/akbPlayerDb.db";
#endif
            }

            //Create db Logger
            CreateDBLogger();

            //Save file
            CreateSaveFiles();
            SetupSaveFilesDB();

            //Sinner souls setup
            CreateSoulsTables();
            SetupSinnerSoulsDB();

            //Advancement Types
            CreateAdvancementsTypesTable();
            SetupAdvancementsTypesDB();

            //Persistend advancements table
            CreateAdvancementsTable();
            SetupAdvancementsDB();

            //SaveFileInfo
            CreateSaveFileInfoTable();
            SetupSaveFileInfoDB();
        }

#if UNITY_EDITOR
        /// <summary>
        /// Creates a table used for logging the database queries.
        /// </summary>
        void CreateDBLogger()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS DB_LOGGER(
                                                LogFile VARCHAR(255) PRIMARY KEY
                                            );";

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Call to add the passed string to the logger table of the database.
        /// </summary>
        /// <param name="log"></param>
        public static void AddLoggerEntry(string log)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"INSERT INTO DB_LOGGER 
                                                (LogFile)
                                            VALUES
                                                (@log)";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "log",
                        Value = $"{DateTime.Now.ToString()} {log}"
                    });

                    command.ExecuteNonQuery();
                }
            }
        }
#endif

        /// <summary>
        /// Creates a table to hold the save files of the game.
        /// </summary>
        void CreateSaveFiles()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS SAVE_FILE 
                                            (
                                                SaveFileID INTEGER PRIMARY KEY
                                                                 UNIQUE
                                                                 NOT NULL
                                            );";

                    int result = command.ExecuteNonQuery();

#if UNITY_EDITOR
                    AddLoggerEntry($"Save files table creation result: {result.ToString()}");
#endif
                }
            }
        }

        /// <summary>
        /// Inserts the save flles entries to the database.
        /// </summary>
        void SetupSaveFilesDB()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"INSERT OR IGNORE INTO SAVE_FILE 
                                                (SaveFileID)
                                            VALUES
                                                (0),
                                                (1),
                                                (2),
                                                (3);";

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Save file table setup: {result.ToString()}");
#endif
                }
            }
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

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS SINNER_SOULS 
                                            (
                                                SaveFileID INTEGER REFERENCES SAVE_FILE (SaveFileID) 
                                                                   UNIQUE,
                                                SoulsValue INTEGER NOT NULL,
                                                
                                                PRIMARY KEY 
                                                (SaveFileID, SoulsValue)
                                            );";

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Sinner Souls table creation result: {result.ToString()}");
#endif
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

                    command.CommandText = @"INSERT OR IGNORE INTO SINNER_SOULS 
                                            (
                                                SaveFileID,
                                                SoulsValue
                                            )
                                            VALUES 
                                                (0,0),
                                                (1,0),
                                                (2,0),
                                                (3,0);";

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Sinner Souls table setup: {result.ToString()}");
#endif
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
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS PERS_ADV_TYPES 
                                            (
                                                Type ADVANCEMENT (1) PRIMARY KEY UNIQUE NOT NULL
                                            );";

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Persistent Types table creation result: {result.ToString()}");
#endif
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
#if UNITY_EDITOR
                    AddLoggerEntry($"Persistent Types table setup: {result.ToString()}");
#endif
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

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS PERS_ADVANCEMENTS 
                                            (
                                                SaveFileID  INTEGER         REFERENCES SAVE_FILE (SaveFileID),
                                                Advancement ADVANCEMENT (1) REFERENCES PERS_ADV_TYPES (Type) 
                                                                            NOT NULL,
                                                Tier        INTEGER (1)     NOT NULL
                                                                            DEFAULT (0),
                                                IsUnlocked  INTEGER (1)     NOT NULL
                                                                            DEFAULT (0),
                                                PRIMARY KEY 
                                                (SaveFileID, Advancement)
                                            );";

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Advancements table creation result: {result.ToString()}");
#endif
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

                    command.CommandText = @"INSERT OR IGNORE INTO PERS_ADVANCEMENTS 
                                            (
                                                SaveFileID,
                                                Advancement,
                                                Tier,
                                                IsUnlocked
                                            )
                                            VALUES 
                                                (0,'DEVIL_RAGE',0,0),
                                                (1,'DEVIL_RAGE',0,0),
                                                (2,'DEVIL_RAGE',0,0),
                                                (3,'DEVIL_RAGE',0,0),

                                                (0,'DODGE',0,0),
                                                (1,'DODGE',0,0),
                                                (2,'DODGE',0,0),
                                                (3,'DODGE',0,0),

                                                (0,'HEALTH',0,0),
                                                (1,'HEALTH',0,0),
                                                (2,'HEALTH',0,0),
                                                (3,'HEALTH',0,0),

                                                (0,'MELEE',0,0),
                                                (1,'MELEE',0,0),
                                                (2,'MELEE',0,0),
                                                (3,'MELEE',0,0),

                                                (0,'SPEAR_THROW',0,0),
                                                (1,'SPEAR_THROW',0,0),
                                                (2,'SPEAR_THROW',0,0),
                                                (3,'SPEAR_THROW',0,0),

                                                (0,'TP_CHARGE',0,0),
                                                (1,'TP_CHARGE',0,0),
                                                (2,'TP_CHARGE',0,0),
                                                (3,'TP_CHARGE',0,0);";

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Advancements table setup: {result.ToString()}");
#endif
                }
            }
        }
        #endregion

        #region SAVE_FILE_INFO
        void CreateSaveFileInfoTable()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS SAVE_FILE_RUN_INFO 
                                            (
                                                SaveFileID         INTEGER PRIMARY KEY UNIQUE  REFERENCES SAVE_FILE (SaveFileID) DEFAULT NULL,
                                                TotalRuns          INTEGER DEFAULT ( 0),      
                                                LastRoom           INTEGER DEFAULT ( -1),
                                                PlayerHealth       INTEGER DEFAULT ( -1),                                                                         
                                                UnusedAdvancements VARCHAR (255),
                                                UnusedRoomIDs      VARCHAR (255)
                                            )";

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Save files INFO table creation result: {result.ToString()}");
#endif
                }
            }
        }

        void SetupSaveFileInfoDB()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"INSERT OR IGNORE INTO SAVE_FILE_RUN_INFO 
                                            (
                                                SaveFileID,
                                                TotalRuns,
                                                LastRoom,
                                                PlayerHealth,
                                                UnusedAdvancements,
                                                UnusedRoomIDs
                                            )
                                            VALUES
                                                (0,0,NULL,NULL,NULL,NULL),
                                                (1,0,NULL,NULL,NULL,NULL),
                                                (2,0,NULL,NULL,NULL,NULL),
                                                (3,0,NULL,NULL,NULL,NULL);";

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Save file INFO table setup: {result.ToString()}");
#endif
                }
            }
        }
        #endregion

        public static void UpdateUnusedAdvancementsCell(string jsonString, int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"UPDATE SAVE_FILE_RUN_INFO
                                            SET
                                                UnusedAdvancements = @jsonStr
                                            WHERE 
                                                SaveFileID = @fileID;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "jsonStr",
                        Value = jsonString
                    });

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Unused advancement update result: {result.ToString()}");
#endif
                }
            }
        }

        public static string GetUnusedAdvancements(int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"SELECT UnusedAdvancements FROM SAVE_FILE_RUN_INFO
                                            WHERE SaveFileID = @fileID;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    string result = "";
                    StringBuilder sb = new StringBuilder();
                    SqliteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result = sb.Append(reader.GetString(0)).ToString();
                    }

#if UNITY_EDITOR
                    AddLoggerEntry($"JSON retrieval for save file {saveFileID}, result: {result}");
#endif
                    return result;
                }
            }
        }

        public static bool GetHasActiveRun(int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"SELECT LastRoom FROM SAVE_FILE_RUN_INFO
                                            WHERE SaveFileID = @fileID;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    string result = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    SqliteDataReader reader = command.ExecuteReader();

                    if (reader[0].GetType() != typeof(DBNull))
                    {
                        while (reader.Read())
                        {
                            string readStr = reader.GetString(0).ToString();

                            result = sb.Append(readStr).ToString();
                        }

                        bool outcome = (!result.Equals(string.Empty) || result.Equals("0"));

#if UNITY_EDITOR
                        AddLoggerEntry($"Get has active run for save file {saveFileID} result: {result} - outcome: {outcome}");
#endif
                        return outcome;
                    }
                }
                connection.Clone();
                connection.Dispose();

#if UNITY_EDITOR
                AddLoggerEntry("Not currently in run");
#endif
                return false;
            }
        }

        public static void UpdatePlayerHealthValue(int playerHealth, int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"UPDATE SAVE_FILE_RUN_INFO
                                            SET
                                                PlayerHealth = @healthValue
                                            WHERE 
                                                SaveFileID = @fileID;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "healthValue",
                        Value = playerHealth
                    });

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Player health update at save file {saveFileID}, result : {result.ToString()}");
#endif
                }
            }
        }

        /// <summary>
        /// Call to get the player health stored in the database.
        /// </summary>
        /// <returns>-1 in case there is not a player health value stored.</returns>
        public static int GetPlayerHealthValue(int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"SELECT PlayerHealth FROM SAVE_FILE_RUN_INFO
                                            WHERE SaveFileID = @fileID;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    int result = -1;
                    SqliteDataReader reader = command.ExecuteReader();

                    if (reader[0].GetType() != typeof(DBNull))
                    {
                        while (reader.Read())
                        {
                            result = reader.GetInt32(0);
                        }

#if UNITY_EDITOR
                        AddLoggerEntry($"Get Player health at save file {saveFileID}, result : {result.ToString()}");
#endif
                        return result;
                    }

                    connection.Clone();
                    connection.Dispose();
                    AddLoggerEntry($"No player health entry at {saveFileID}");
                    return -1;
                }
            }
        }

        public static void UpdateLastRoom(int roomID, int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"UPDATE SAVE_FILE_RUN_INFO
                                            SET
                                                LastRoom = @roomId
                                            WHERE 
                                                SaveFileID = @fileID;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "roomId",
                        Value = roomID
                    });

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Last room update at save file {saveFileID}, result: {result.ToString()}");
#endif
                }
            }
        }

        public static int GetLastRoom(int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"SELECT LastRoom FROM SAVE_FILE_RUN_INFO
                                            WHERE SaveFileID = @fileID;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    int result = -1;
                    SqliteDataReader reader = command.ExecuteReader();

                    if (reader[0].GetType() != typeof(DBNull))
                    {
                        while (reader.Read())
                        {
                            result = reader.GetInt32(0);
                        }

#if UNITY_EDITOR
                        AddLoggerEntry($"Get last room at save file {saveFileID}, result : {result.ToString()}");
#endif
                        return result;
                    }

                    connection.Clone();
                    connection.Dispose();
                    AddLoggerEntry($"No last room entry at {saveFileID}");
                    return -1;
                }
            }
        }

        public static void UpdateUnusedRooms(string jsonStr, int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"UPDATE SAVE_FILE_RUN_INFO
                                            SET
                                                UnusedRoomIDs = @str
                                            WHERE 
                                                SaveFileID = @fileID;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "str",
                        Value = jsonStr
                    });

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Update unused rooms at save file {saveFileID}, result: {result.ToString()}");
#endif
                }
            }
        }

        public static string GetUnusedRooms(int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"SELECT UnusedRoomIDs FROM SAVE_FILE_RUN_INFO
                                            WHERE SaveFileID = @fileID;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    string result = "";
                    StringBuilder sb = new StringBuilder();
                    SqliteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result = sb.Append(reader.GetString(0)).ToString();
                    }

#if UNITY_EDITOR
                    AddLoggerEntry($"Get unused rooms JSON for save file {saveFileID}, result: {result}");
#endif
                    return result;
                }
            }
        }
    }
}