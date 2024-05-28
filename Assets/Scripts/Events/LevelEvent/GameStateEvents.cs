using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Events.LevelEvent
{
    public static class GameStateEvents
    {
        public static UnityAction<int> LevelStart;
        public static UnityAction LevelFail;
        public static UnityAction GameOver;
        public static UnityAction LevelSuccess;
        public static UnityAction GameSuccess;
        public static UnityAction<int> TimeIsDecrease;
        public static UnityAction LevelReload;
        public static UnityAction  AllEnemiesDied;
        public static UnityAction OpenStageScene;
        public static UnityAction GameStart;

    }
}