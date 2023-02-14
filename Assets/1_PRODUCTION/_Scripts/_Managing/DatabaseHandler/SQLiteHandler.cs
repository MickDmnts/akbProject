using System.Data;
using System.Text;
using System;
using System.IO;
using Mono.Data.Sqlite;

using UnityEngine;

namespace akb.Core.Database
{
    public class SQLiteHandler
    {
        private static string dbPath = "";

        int lastUsedIdentifier = 0;

        public SQLiteHandler()
        {
            if (dbPath == "")
            {
#if UNITY_EDITOR
                dbPath = "URI=file:" + Application.dataPath + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "akbPlayerDb.db";
#endif

#if UNITY_STANDALONE && !UNITY_EDITOR
                            dbPath = "URI=file:" + Application.dataPath +  Path.DirectorySeparatorChar + "akbPlayerDb.db";
#endif
            }

            ConstructDB();

            OnCostruction();
        }

        void ConstructDB()
        {
            string trimmedPath = dbPath.Substring(9);

            if (!File.Exists(trimmedPath))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(trimmedPath)) { }
            }
        }

        #region DATABASE_CREATION
        private void OnCostruction()
        {
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

            //Create a save file cache table
            CreateLastPlayedSaveFile();
            SetupLastPlayedSaveFile();

            //Monster entries
            CreateMonsterEntriesTable();
            SetupMonsterEntriesDB();
        }

        ///<summary>Creates a table used for logging the database queries.</summary>
        void CreateDBLogger()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS DB_LOGGER(
                                                LogFile VARCHAR(255) 
                                            );";

                    command.ExecuteNonQuery();
                }
            }
        }

        ///<summary>Call to add the passed string to the logger table of the database.</summary>
        public void AddLoggerEntry(string log)
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

                    _ = command.ExecuteNonQuery();
                }

                connection.Close();
                connection.Dispose();
            }
        }

        ///<summary>Creates a table to hold the save files of the game.</summary>
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

        ///<summary>Inserts the save flles entries to the database.</summary>
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
        ///<summary>Creates SINNER_SOULS table.</summary>
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

        ///<summary>Inserts initial entries to SINNER_SOULS table</summary>
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
        ///<summary>Creates EXISTS PERS_ADV_TYPES table.</summary>
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

        ///<summary>Inserts initial entries to PERS_ADV_TYPES table.</summary>
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
        ///<summary>Creates PERS_ADVANCEMENTS table</summary>
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

        ///<summary>Inserts initial entries at PERS_ADVANCEMENTS table.</summary>
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
        ///<summary>Creates SAVE_FILE_RUN_INFO table.</summary>
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
                                                InRunAdvancementData VARCHAR (255),
                                                UnusedRoomIDs      VARCHAR (255),
                                                CoinsInRun INTEGER DEFAULT ( -1)
                                            )";

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Save files INFO table creation result: {result.ToString()}");
#endif
                }
            }
        }

        ///<summary>Inserts initial entries to SAVE_FILE_RUN_INFO table.</summary>
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
                                                InRunAdvancementData,
                                                UnusedRoomIDs,
                                                CoinsInRun
                                            )
                                            VALUES
                                                (0,0,NULL,NULL,NULL,NULL,NULL),
                                                (1,0,NULL,NULL,NULL,NULL,NULL),
                                                (2,0,NULL,NULL,NULL,NULL,NULL),
                                                (3,0,NULL,NULL,NULL,NULL,NULL);";

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Save file INFO table setup: {result.ToString()}");
#endif
                }
            }
        }
        #endregion

        #region LAST_USED_SAVE_FILE
        ///<summary>Creates table EXISTS LAST_USED_SAVE_FILE at DB.</summary>
        void CreateLastPlayedSaveFile()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS LAST_USED_SAVE_FILE
                                            (
                                                Identifier INTEGER PRIMARY KEY DEFAULT (0),
                                                LastUsedSaveID INTEGER DEFAULT (-1)
                                            )";

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Last played save file table setup: {result.ToString()}");
#endif
                }
            }
        }

        ///<summary>Creates initial LastUsedSaveID entry at LAST_USED_SAVE_FILE table.</summary>
        void SetupLastPlayedSaveFile()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"INSERT OR IGNORE INTO LAST_USED_SAVE_FILE
                                            (
                                                Identifier,
                                                LastUsedSaveID
                                            )
                                            VALUES
                                                (
                                                    @identifier,
                                                    -1
                                                );";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "identifier",
                        Value = lastUsedIdentifier
                    });

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Last played save file table setup: {result.ToString()}");
#endif
                }
            }
        }
        #endregion

        #region MONSTER_GRIMOIRE_ENTRIES
        ///<summary>Creates table MONSTER_ENTRIES at DB.</summary>
        void CreateMonsterEntriesTable()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"CREATE TABLE IF NOT EXISTS MONSTER_ENTRIES
                                            (
                                                SaveFileID  INTEGER REFERENCES SAVE_FILE (SaveFileID),
                                                MonsterID INTEGER,
                                                MonsterDescription VARCHAR(255),
                                                IsFound INTEGER DEFAULT (0),

                                                PRIMARY KEY (SaveFileID, MonsterID)
                                            );";

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Monster entries table creation: {result.ToString()}");
#endif
                }
            }
        }

        ///<summary>Inserts initial entries to MONSTER_ENTRIES table.</summary>
        void SetupMonsterEntriesDB()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"INSERT OR IGNORE INTO MONSTER_ENTRIES
                                            (
                                                SaveFileID,
                                                MonsterID,
                                                MonsterDescription,
                                                IsFound
                                            )
                                            VALUES
                                                (0, 0, 'The main workforce of Hell, these mindless creatures dig new pits and tunes for hell every day, while getting a taste of the daily soul coming too close. They will approach on sight to get a tasty bite but are quite easy to fend off.', 0),
                                                (1, 0, 'The main workforce of Hell, these mindless creatures dig new pits and tunes for hell every day, while getting a taste of the daily soul coming too close. They will approach on sight to get a tasty bite but are quite easy to fend off.', 0),
                                                (2, 0, 'The main workforce of Hell, these mindless creatures dig new pits and tunes for hell every day, while getting a taste of the daily soul coming too close. They will approach on sight to get a tasty bite but are quite easy to fend off.', 0),
                                                (3, 0, 'The main workforce of Hell, these mindless creatures dig new pits and tunes for hell every day, while getting a taste of the daily soul coming too close. They will approach on sight to get a tasty bite but are quite easy to fend off.', 0),

                                                (0, 1, 'Every place has its pests and Hell is no exception. Attracted to heat, these giant bugs will gnaw and bite you to get their sweet sweet taste. You need to be careful around those things because if you swat them too hard they will explode on your face.', 0),
                                                (1, 1, 'Every place has its pests and Hell is no exception. Attracted to heat, these giant bugs will gnaw and bite you to get their sweet sweet taste. You need to be careful around those things because if you swat them too hard they will explode on your face.', 0),
                                                (2, 1, 'Every place has its pests and Hell is no exception. Attracted to heat, these giant bugs will gnaw and bite you to get their sweet sweet taste. You need to be careful around those things because if you swat them too hard they will explode on your face.', 0),
                                                (3, 1, 'Every place has its pests and Hell is no exception. Attracted to heat, these giant bugs will gnaw and bite you to get their sweet sweet taste. You need to be careful around those things because if you swat them too hard they will explode on your face.', 0),
                                                
                                                (0, 2, 'These wraiths were created by Beelzebub himself after his short trip to Stygia. At least I think that is where he got the idea. They serve as guards for the greater demons and see threat everywhere, charging on you and slaming their giant swords, ignoring any collateral damage.', 0),
                                                (1, 2, 'These wraiths were created by Beelzebub himself after his short trip to Stygia. At least I think that is where he got the idea. They serve as guards for the greater demons and see threat everywhere, charging on you and slaming their giant swords, ignoring any collateral damage.', 0),
                                                (2, 2, 'These wraiths were created by Beelzebub himself after his short trip to Stygia. At least I think that is where he got the idea. They serve as guards for the greater demons and see threat everywhere, charging on you and slaming their giant swords, ignoring any collateral damage.', 0),
                                                (3, 2, 'These wraiths were created by Beelzebub himself after his short trip to Stygia. At least I think that is where he got the idea. They serve as guards for the greater demons and see threat everywhere, charging on you and slaming their giant swords, ignoring any collateral damage.', 0),
                                                
                                                (0, 3, 'Maybe the most ambitious of the lesser demons, they are overzealous with creating the most chaos and havoc. They need to be the best and they will, litterally, stop at nothing. Maybe too literally since , when they start charging at you, only a rock solid wall will stop them.', 0),
                                                (1, 3, 'Maybe the most ambitious of the lesser demons, they are overzealous with creating the most chaos and havoc. They need to be the best and they will, litterally, stop at nothing. Maybe too literally since , when they start charging at you, only a rock solid wall will stop them.', 0),
                                                (2, 3, 'Maybe the most ambitious of the lesser demons, they are overzealous with creating the most chaos and havoc. They need to be the best and they will, litterally, stop at nothing. Maybe too literally since , when they start charging at you, only a rock solid wall will stop them.', 0),
                                                (3, 3, 'Maybe the most ambitious of the lesser demons, they are overzealous with creating the most chaos and havoc. They need to be the best and they will, litterally, stop at nothing. Maybe too literally since , when they start charging at you, only a rock solid wall will stop them.', 0),
                                                
                                                (0, 4, 'Vile creatures made by the condensed sins of our guests. They are devious with foul intentions, hating everything that exists in the realm. But they get the job done so I keep them around. If they see you from afar they will spit on you, and its going to hurt like HELL! 
Warning: Do not try to pet them!', 0),
                                                (1, 4, 'Vile creatures made by the condensed sins of our guests. They are devious with foul intentions, hating everything that exists in the realm. But they get the job done so I keep them around. If they see you from afar they will spit on you, and its going to hurt like HELL! 
Warning: Do not try to pet them!', 0),
                                                (2, 4, 'Vile creatures made by the condensed sins of our guests. They are devious with foul intentions, hating everything that exists in the realm. But they get the job done so I keep them around. If they see you from afar they will spit on you, and its going to hurt like HELL! 
Warning: Do not try to pet them!', 0),
                                                (3, 4, 'Vile creatures made by the condensed sins of our guests. They are devious with foul intentions, hating everything that exists in the realm. But they get the job done so I keep them around. If they see you from afar they will spit on you, and its going to hurt like HELL! 
Warning: Do not try to pet them!', 0),
                                                
                                                (0, 5, 'The thing some demons do not understand is, we are not here to have fun. We are here for work!
These guys find it funny to play pranks on other demons as they do with the souls of humans. They throw around these little rocks that jolt you through and through.', 0),
                                                (1, 5, 'The thing some demons do not understand is, we are not here to have fun. We are here for work!
These guys find it funny to play pranks on other demons as they do with the souls of humans. They throw around these little rocks that jolt you through and through.', 0),
                                                (2, 5, 'The thing some demons do not understand is, we are not here to have fun. We are here for work!
These guys find it funny to play pranks on other demons as they do with the souls of humans. They throw around these little rocks that jolt you through and through.', 0),
                                                (3, 5, 'The thing some demons do not understand is, we are not here to have fun. We are here for work!
These guys find it funny to play pranks on other demons as they do with the souls of humans. They throw around these little rocks that jolt you through and through.', 0),
                                                
                                                (0, 6, 'These demons seem elegant but they are the greatest tricksters in the kingdom. Beautiful, elegant and most certainly deadly. Their song will make you lose your mind, calling you ever closer. Do not be fooled. Their charms are only a means to a deadly end.', 0),
                                                (1, 6, 'These demons seem elegant but they are the greatest tricksters in the kingdom. Beautiful, elegant and most certainly deadly. Their song will make you lose your mind, calling you ever closer. Do not be fooled. Their charms are only a means to a deadly end.', 0),
                                                (2, 6, 'These demons seem elegant but they are the greatest tricksters in the kingdom. Beautiful, elegant and most certainly deadly. Their song will make you lose your mind, calling you ever closer. Do not be fooled. Their charms are only a means to a deadly end.', 0),
                                                (3, 6, 'These demons seem elegant but they are the greatest tricksters in the kingdom. Beautiful, elegant and most certainly deadly. Their song will make you lose your mind, calling you ever closer. Do not be fooled. Their charms are only a means to a deadly end.', 0),
                                                
                                                (0, 7, 'These creature’s origin is unknown even to me. No one remembers when or how they came to be here, because they own the ability to shape and erase memories. If you stare in their eyes too long, you will most certainly forget even your basic body functions.', 0),
                                                (1, 7, 'These creature’s origin is unknown even to me. No one remembers when or how they came to be here, because they own the ability to shape and erase memories. If you stare in their eyes too long, you will most certainly forget even your basic body functions.', 0),
                                                (2, 7, 'These creature’s origin is unknown even to me. No one remembers when or how they came to be here, because they own the ability to shape and erase memories. If you stare in their eyes too long, you will most certainly forget even your basic body functions.', 0),
                                                (3, 7, 'These creature’s origin is unknown even to me. No one remembers when or how they came to be here, because they own the ability to shape and erase memories. If you stare in their eyes too long, you will most certainly forget even your basic body functions.', 0),
                                                
                                                (0, 8, 'Boss Astaroth Desc', 0),
                                                (1, 8, 'Boss Astaroth Desc', 0),
                                                (2, 8, 'Boss Astaroth Desc', 0),
                                                (3, 8, 'Boss Astaroth Desc', 0),
                                                
                                                (0, 9, 'Boss Beelzebub Desc', 0),
                                                (1, 9, 'Boss Beelzebub Desc', 0),
                                                (2, 9, 'Boss Beelzebub Desc', 0),
                                                (3, 9, 'Boss Beelzebub Desc', 0);";

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Monster entries table setup: {result.ToString()}");
#endif
                }
            }
        }
        #endregion
        #endregion

        #region EXTERNAL_HANDLERS
        ///<summary>Sets the passed save file ID cells back to default.</summary>
        public void EraseDataFromFile(int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"UPDATE SAVE_FILE_RUN_INFO
                                            SET
                                                TotalRuns = 0,
                                                LastRoom = NULL,
                                                PlayerHealth = NULL,
                                                InRunAdvancementData = NULL,
                                                UnusedRoomIDs = NULL,
                                                CoinsInRun = NULL
                                            WHERE 
                                                SaveFileID = @fileID;";


                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Data erased at {saveFileID}, result: {result.ToString()}");
#endif
                }
            }
        }

        ///<summary>Writes the json string to the corresponding save file ID cell in the DB.</summary>
        public void UpdateInRunAdvancementDataCell(string jsonString, int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"UPDATE SAVE_FILE_RUN_INFO
                                            SET
                                                InRunAdvancementData = @jsonStr
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
                    AddLoggerEntry($"In Run AdvancementData update result: {result.ToString()}");
#endif
                }
            }
        }

        ///<summary>Returns the json string saved in the InRunAdvancementData cell of the DB.</summary>
        public string GetInRunAdvancementData(int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"SELECT InRunAdvancementData FROM SAVE_FILE_RUN_INFO
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

        ///<summary>Returns true or false based on LastRoom value of the passed save file ID.</summary>
        ///<returns>Also, False when the value is NULL</returns>
        public bool GetHasActiveRun(int saveFileID)
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
                            string readStr = reader.GetInt32(0).ToString();

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

        ///<summary>Writes the passed room ID to the corresponding save file ID cell.</summary>
        public void UpdateLastRoom(int roomID, int saveFileID)
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

        ///<summary>Writes the passed value to the corresponding save file ID cell.</summary>
        public void UpdatePlayerHealthValue(int playerHealth, int saveFileID)
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

        ///<summary>Call to get the player health stored in the database.</summary>
        /// <returns>-1 in case there is not a player health value stored.</returns>
        public int GetPlayerHealthValue(int saveFileID)
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

        ///<summary>Returns the room ID saved in the save file ID corresponding cell.</summary>
        ///<return>-1 in case the cell is NULL.</return>
        public int GetLastRoom(int saveFileID)
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
                    return -2;
                }
            }
        }

        ///<summary>Writes the passed JSON string to the corresponding save file ID cell.</summary>
        public void UpdateUnusedRooms(string jsonStr, int saveFileID)
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

        ///<summary>Returns the JSON string saved in the corresponding save file ID unused rooms cell.</summary>
        ///<return>An empty string in case there is no value stored.</return>
        public string GetUnusedRooms(int saveFileID)
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

        ///<summary>Writes the passed save file ID to the universal DB table, primarily used to store the last used save file ID.</summary>
        public void SetLastUsedFileID(int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"UPDATE OR IGNORE LAST_USED_SAVE_FILE
                                            SET
                                                LastUsedSaveID = @saveFileID
                                            WHERE 
                                                Identifier = @identifier;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "identifier",
                        Value = lastUsedIdentifier
                    });

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "saveFileID",
                        Value = saveFileID
                    });

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Update last used room to {saveFileID}, result: {result.ToString()}");
#endif
                }
            }
        }

        ///<summary>Returns the save file ID stored in the universal DB table.</summary>
        ///<return>-1 in case there is not ID saved</return>
        public int GetLastUsedFileID()
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"SELECT LastUsedSaveID FROM LAST_USED_SAVE_FILE
                                            WHERE Identifier = @identifier;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "identifier",
                        Value = lastUsedIdentifier
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
                        AddLoggerEntry($"Get last saved ID result : {result.ToString()}");
#endif
                    }

                    return result;
                }
            }
        }

        ///<summary>Writes the passed value to the corresponding save file ID cell.</summary>
        public void UpdateInRunCoinValue(int value, int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"UPDATE SAVE_FILE_RUN_INFO
                                            SET
                                                CoinsInRun = @coins
                                            WHERE 
                                                SaveFileID = @fileID;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "coins",
                        Value = value
                    });

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Update in run coin value at save file {saveFileID}, result: {result.ToString()}");
#endif
                }
            }
        }

        ///<summary>Returns the coin value stored in the corresponding save file ID cell.</summary>
        ///<return>-1 in case there is no value saved</return>
        public int GetInRunCoinValue(int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"SELECT CoinsInRun FROM SAVE_FILE_RUN_INFO
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
                        AddLoggerEntry($"Get in run coin value at save file {saveFileID}, result : {result.ToString()}");
#endif
                        return result;
                    }

                    connection.Clone();
                    connection.Dispose();
                    AddLoggerEntry($"No coin value saved at {saveFileID}");
                    return -1;
                }
            }
        }

        ///<summary>Writes the passed value to the corresponding save file ID cell.</summary>
        public void UpdateTotalRunsValue(int value, int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"UPDATE SAVE_FILE_RUN_INFO
                                            SET
                                                TotalRuns = @value
                                            WHERE 
                                                SaveFileID = @fileID;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "value",
                        Value = value
                    });

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Update total runs value at save file {saveFileID}, result: {result.ToString()}");
#endif
                }
            }
        }

        ///<summary>Returns the total runs value stored in the corresponding save file ID cell.</summary>
        ///<return>-1 in case there is no value saved</return>
        public int GetTotalRunsValue(int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"SELECT TotalRuns FROM SAVE_FILE_RUN_INFO
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
                        AddLoggerEntry($"Get total run value at save file {saveFileID}, result : {result.ToString()}");
#endif
                        return result;
                    }

                    connection.Clone();
                    connection.Dispose();
                    AddLoggerEntry($"No total run value saved at {saveFileID}");
                    return -1;
                }
            }
        }

        ///<summary>Writes the passed value to the corresponding save file ID cell.</summary>
        public void UpdateSoulsValue(int value, int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"UPDATE SINNER_SOULS
                                            SET
                                                SoulsValue = @value
                                            WHERE 
                                                SaveFileID = @fileID;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "value",
                        Value = value
                    });

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Update souls value at save file {saveFileID}, result: {result.ToString()}");
#endif
                }
            }
        }

        ///<summary>Returns the souls value stored in the corresponding save file ID cell.</summary>
        ///<return>-1 in case there is no value saved</return>
        public int GetSoulsValue(int saveFileID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"SELECT SoulsValue FROM SINNER_SOULS
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
                        AddLoggerEntry($"Get souls value at save file {saveFileID}, result : {result.ToString()}");
#endif
                        return result;
                    }

                    connection.Clone();
                    connection.Dispose();
                    AddLoggerEntry($"No souls value saved at {saveFileID}");
                    return -1;
                }
            }
        }

        ///<summary>Writes the passed value to the corresponding save file ID - monster ID pair cell.</summary>
        public void UpdateIsMonsterFound(int saveFileID, int monsterID, bool isFound)
        {
            int isFoundParsed = isFound ? 1 : 0;

            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"UPDATE MONSTER_ENTRIES
                                            SET
                                                IsFound = @isFound
                                            WHERE 
                                                SaveFileID = @fileID
                                                AND
                                                MonsterID = @monsterID;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "isFound",
                        Value = isFoundParsed
                    });

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "monsterID",
                        Value = monsterID.ToString()
                    });

                    int result = command.ExecuteNonQuery();
#if UNITY_EDITOR
                    AddLoggerEntry($"Update is found value at save file {saveFileID} with monster ID {monsterID}, result: {result.ToString()}");
#endif
                }
            }
        }

        ///<summary>Returns the is found value stored in the corresponding save file ID cell.</summary>
        ///<return>-1 in case there is no value saved</return>
        public int GetIsMonsterFoundValue(int saveFileID, int monsterID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"SELECT IsFound FROM MONSTER_ENTRIES
                                            WHERE 
                                                SaveFileID = @fileID
                                                AND
                                                MonsterID = @monsterID;";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "monsterID",
                        Value = monsterID.ToString()
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
                        AddLoggerEntry($"Get is found value at save file {saveFileID} for monster ID {monsterID}, result : {result.ToString()}");
#endif
                        return result;
                    }

                    connection.Clone();
                    connection.Dispose();

                    AddLoggerEntry($"No is found value saved at {saveFileID}, monster ID {monsterID}");
                    return -1;
                }
            }
        }

        ///<summary>Returns the monster description from the passed mosnter ID.</summary>
        public string GetMonsterDescription(int saveFileID, int monsterID)
        {
            using (SqliteConnection connection = new SqliteConnection(dbPath))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;

                    command.CommandText = @"SELECT MonsterDescription FROM MONSTER_ENTRIES
                                            WHERE 
                                                SaveFileID = @fileID 
                                                AND
                                                MonsterID = @monsterID";

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "fileID",
                        Value = saveFileID.ToString()
                    });

                    command.Parameters.Add(new SqliteParameter()
                    {
                        ParameterName = "monsterID",
                        Value = monsterID.ToString()
                    });

                    string result = "";
                    StringBuilder sb = new StringBuilder();
                    SqliteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result = sb.Append(reader.GetString(0)).ToString();
                    }

                    AddLoggerEntry($"Returned: Monster description value at {saveFileID}, monster ID {monsterID}");
                    return result;
                }
            }
        }
        #endregion
    }
}