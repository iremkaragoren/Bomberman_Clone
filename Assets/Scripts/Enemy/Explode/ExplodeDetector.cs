using System;
using Datas;
using DG.Tweening;
using Events.InGameEvents;
using Events.LevelEvent;
using InGame.DestroyableWall;
using InGame.Enemy;
using InGame.Player;
using UI.Manager;
using UnityEngine;

namespace InGame.Bomb
{
    public class ExplodeDetector: MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.TryGetComponent(out PlayerDetector playerDetector) && playerDetector.isBombTriggered==false)
            {
                playerDetector.isBombTriggered = true;
                GameStateEvents.LevelFail?.Invoke();
            }

            if (other.TryGetComponent(out EnemyDetector enemyDetector))
            {
                Destroy(enemyDetector.gameObject);
                BombEvents.TriggerEnemy?.Invoke();
                
                Debug.Log("trigger enemy"+other.gameObject);
            }

            if (other.TryGetComponent(out DestroyableWallDetector destroyableWall))
            {
                BombEvents.TriggerDestroyableWall?.Invoke(destroyableWall.gameObject);
                // Destroy(destroyableWall.gameObject);
            
            }
            
            if (other.gameObject.TryGetComponent(out DoorDetector doorDetector))
            {
                Debug.Log("done"+other.gameObject);
                BombEvents.TriggerDoor?.Invoke();
                
            }
            
        }

    }
}




