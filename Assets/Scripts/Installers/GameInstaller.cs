using Datas;
using Events.LevelEvent;
using ScriptableObjects.Predefined.Level;
using ScriptableObjects.Predefined.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Installers
{
    public class GameInstaller
    {
        public static GameInstaller Instance { get; private set; }
        public LevelListSo LevelListSo { get; private set; }
        public PlayerPredefined PlayerPredefined { get; private set; }
        public PlayerData PlayerData { get; private set; }


      [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
      
        static void OnBeforeSplashScene()
        {
            Instance = new GameInstaller();
            Instance.Construct();
        }
        public void Construct()
        {
            RegisterEvents();
            InstallData();
            LoadScene(EnvironmentVariables.StarterSceneName);
        } 

        private void InstallData()
        {
            LevelListSo = Resources.Load<LevelListSo>(EnvironmentVariables.LevelsDataPath);
            PlayerPredefined = Resources.Load<PlayerPredefined>(EnvironmentVariables.PlayerPredefinedPath);
            PlayerData = PlayerData.Instance;
            
        }

        private void LoadScene(string sceneName)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadSceneAsync(sceneName);
        }

        private void RegisterEvents()
        {
            GameStateEvents.LevelReload += OnLevelLoad;
            GameStateEvents.GameSuccess += OnLevelSuccess;
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == EnvironmentVariables.MainSceneName)
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }
           
            
        }

        private void OnLevelLoad()
        {
            LoadScene(EnvironmentVariables.MainSceneName);
            Debug.Log("1");
            
        }

        private void OnLevelSuccess()
        {
            PlayerData.LevelUp();
            LoadScene(EnvironmentVariables.MainSceneName);
            Debug.Log("2");
        }
    }
}