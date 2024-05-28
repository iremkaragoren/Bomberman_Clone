using System;
using System.Collections;
using Events.InGameEvents;
using Events.LevelEvent;
using InGame.Enemy;
using InGame.PowerUp;
using ScriptableObjects.Variable;
using UI.Manager;
using Unity.VisualScripting;
using UnityEngine;

namespace InGame.Player
{
    public class PlayerDetector : MonoBehaviour
    {
        public bool isBombTriggered;

        private EnemyInstantiate m_enemyInstantiate;

        private BoxCollider2D m_collider;

        private void Awake()
        {
            m_enemyInstantiate = GetComponent<EnemyInstantiate>();
            m_collider = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PowerUpDetector powerUpDetector)&& powerUpDetector.canInteract )
            {
                PowerUpEvents.CollectPowerUp.Invoke(powerUpDetector._PowerUpType, powerUpDetector.Amount);
                Destroy(powerUpDetector.gameObject);
            }
            
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out EnemyDetector enemyDetector))
            {
                GameStateEvents.LevelFail?.Invoke();
                
            }
        }
        

       
    }
}
