using UnityEngine;
using UnityEngine.Events;

namespace Events.InGameEvents
{
    public static class BombEvents
    {
        
        public static UnityAction TriggerEnemy;

        public static UnityAction<GameObject> TriggerDestroyableWall;

        public static UnityAction BombExploded;
        
        public static UnityAction TriggerDoor;
        





    }
}
