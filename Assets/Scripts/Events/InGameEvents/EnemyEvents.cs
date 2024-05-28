using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;

namespace Events.InGameEvents
{
    public class EnemyEvents : MonoBehaviour
    {
        public static UnityAction EnemyDeath;

        public static UnityAction LeftOrDownMove;

        public static UnityAction RightOrUpMove;

        public static UnityAction<GameObject> EnemyInstantiate;





    }
}
