using UnityEngine;

namespace ScriptableObjects.Predefined.Player
{
    
    
    [CreateAssetMenu(fileName = "PlayerPredefined", menuName = "ThisGame/Predefined/PlayerPredefined", order = 1)]
    public class PlayerPredefined : ScriptableObject
    {
        public int IntialHealth = 2;
        

    }
}