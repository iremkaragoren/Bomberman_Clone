using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Events.InGameEvents;
using Events.LevelEvent;
using ScriptableObjects.Predefined.Level;
using ScriptableObjects.Predefined.Player;
using ScriptableObjects.Variable;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utils;

namespace Datas
{
    [Serializable]
    public class PlayerData
    {
        private static PlayerData instance;

        public static PlayerData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerData();
                }

                return instance;
            }
        }

        private List<PowerUpTypes> _powerUps;
        private Dictionary<PowerUpTypes, int> _powerUpCount;
        public LevelListSo m_levelList;

        public int CurrentLevel;
        private bool m_levelCompleted;

        public int CurrentHealth;
        public int Score;
        private bool m_hasFailedLevel;

        public PlayerData()
        {
            m_levelList = Resources.Load<LevelListSo>(EnvironmentVariables.LevelsDataPath);
            _powerUpCount = new Dictionary<PowerUpTypes, int>();
            Construct();
        }

        public void Construct()
        {
            InitializeInitialPowerups();
            RegisterEvents();

            if (TryLoadPlayer()) return;

            CreateDefaultPlayer();
        }

        public void InitializeNewGame()
        {
            CreateDefaultPlayer();
            SavePlayerData();
        }

        public void ContinueGame()
        {
            if (File.Exists(EnvironmentVariables.PlayerSavePath))
            {
                LoadPlayerData();
            }
            else
            {
                InitializeNewGame();
            }
        }

        public int GetAmountOfPowerUp(PowerUpTypes powerUpType)
        {
            if (_powerUpCount.TryGetValue(powerUpType, out int up))
            {
                return up;
            }

            return 0;
        }


        public LevelSo GetCurrentLevel()
        {
            return m_levelList.Levels[CurrentLevel];
        }

        private void RegisterEvents()
        {
            PowerUpEvents.CollectPowerUp += OnPowerUpTriggered;
            BombEvents.TriggerEnemy += OnBombTriggerEnemy;
            GameStateEvents.LevelFail += OnLevelFail;
            GameStateEvents.LevelSuccess += OnLevelComplete;
        }


        public void OnLevelComplete()
        {
            if (!m_hasFailedLevel)
            {
                CurrentHealth++;
            }

            m_levelCompleted = true;
            GameStateEvents.GameSuccess.Invoke();
            SavePlayerData();


            m_hasFailedLevel = false;
        }

        public void OnLevelFail()
        {
            CurrentHealth--;

            if (CurrentHealth < 0)
            {
                DeletePlayerData();
                CreateDefaultPlayer();
                GameStateEvents.GameOver?.Invoke();
            }
            else
            {
                SavePlayerData();
                GameStateEvents.LevelReload.Invoke();
            }
        }

        private void OnBombTriggerEnemy()
        {
            AddScore(100);
        }

        private void OnPowerUpTriggered(PowerUpTypes types, int amount)
        {
            AddPowerUp(types);
        }

        private void InitializeInitialPowerups()
        {
            AddPowerUp(PowerUpTypes.SpeedUp);
            AddPowerUp(PowerUpTypes.BombUp);
            
        }

        private void AddPowerUp(PowerUpTypes powerUpTypes)
        {
            if (_powerUpCount.ContainsKey(powerUpTypes))
            {
                _powerUpCount[powerUpTypes]++;
            }
            else
            {
                _powerUpCount.Add(powerUpTypes, 1);
            }
        }

        private void RemovePowerUp(PowerUpTypes powerUpTypes)
        {
            _powerUps.Remove(powerUpTypes);
        }

        private void CreateDefaultPlayer()
        {
            CurrentLevel = 0;
            Score = 0;
            CurrentHealth = Resources.Load<PlayerPredefined>(EnvironmentVariables.PlayerPredefinedPath)?.IntialHealth ?? 0;

            m_levelList = Resources.Load<LevelListSo>(EnvironmentVariables.LevelsDataPath);
            if (m_levelList == null)
            {
                Debug.LogError("Level list couldn't be loaded.");
                return;
            }

            _powerUps = new List<PowerUpTypes>();
            _powerUpCount = new Dictionary<PowerUpTypes, int>();
            
            InitializeInitialPowerups();
            SavePlayerData();
        }

        public void AddScore(int amount)
        {
            Score += amount;
        }

        private bool TryLoadPlayer()
        {
            if (!File.Exists(EnvironmentVariables.PlayerSavePath)) return false;

            LoadPlayerData();
            return true;
        }

        private void SavePlayerData()
        {
            string jsonData = JsonUtility.ToJson(this);
            File.WriteAllText(EnvironmentVariables.PlayerSavePath, jsonData);
        }

        private void LoadPlayerData()
        {
            string jsonData = File.ReadAllText(EnvironmentVariables.PlayerSavePath);
            JsonUtility.FromJsonOverwrite(jsonData, this);
        }

        private void DeletePlayerData()
        {
            if (File.Exists(EnvironmentVariables.PlayerSavePath))
            {
                File.Delete(EnvironmentVariables.PlayerSavePath);
            }
        }

        public void LevelUp()
        {
            CurrentLevel++;
        }
    }
}