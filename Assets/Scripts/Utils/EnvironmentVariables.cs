using UnityEngine;

namespace Utils
{
    public static class EnvironmentVariables
    {
        public const string LevelsDataPath = "Predefined/Levels/LevelList";
        public const string PlayerPredefinedPath = "Predefined/Player/PlayerPredefined";

        public const string StarterSceneName = "Starter";
        public const string MainSceneName = "Main";
        public const string LoginSceneName = "Login";
        public static string PlayerSavePath => Application.persistentDataPath + "/" + "Player.sav";
    }
}