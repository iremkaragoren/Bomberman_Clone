using System;
using UnityEngine;

namespace ScriptableObjects.Variable
{
    [CreateAssetMenu(fileName = "PowerUps_SO", menuName = "ThisGame/Predefined/PowerUpsSO", order = 1)]
    public class PowerUps_SO : ScriptableObject
    {
        public GameObject PowerUpGO => m_powerUpGO;
        public float AmountToIncreaseToIncrease => amountToIncrease;
        public PowerUpTypes PowerUpType => _powerUpTypes;
        
        [SerializeField] private GameObject m_powerUpGO;
        [SerializeField] private PowerUpTypes _powerUpTypes;
        [SerializeField] private float amountToIncrease;
    }
    
    [Serializable]
    public enum PowerUpTypes
    {
        FullFire,
        BombUp,
        SpeedUp,
        ExtraHealth
    }
}