using UnityEngine;
using UnityEngine.Events;

namespace Events.InGameEvents
{
    public static class PlayerEvents
    {
    
        public static UnityAction TriggerEnemy0rBomb;
        public static UnityAction TriggerDoor;
        public static UnityAction DropBomb;
        public static UnityAction<GameObject> PlayerPos;
    }
}
