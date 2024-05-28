using System;
using System.Collections;
using System.Collections.Generic;
using Events.InGameEvents;
using Events.LevelEvent;
using InGame.DestroyableWall;
using InGame.Player;
using Unity.VisualScripting;
using UnityEngine;

public class DoorDetector : MonoBehaviour
{

    public bool isUnlocked = false;
    public bool canInteract = true;
    
    // private BoxCollider2D m_collider;
    private EnemyInstantiate m_enemyInstantiate;

    private void Awake()
    {
        // m_collider = GetComponent<BoxCollider2D>();
        m_enemyInstantiate = GetComponent<EnemyInstantiate>();
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out DestroyableWallDetector detector))
        {
            canInteract = false;
        }
        
        if (other.gameObject.TryGetComponent(out PlayerDetector playerDetector) && isUnlocked )
        {
            GameStateEvents.LevelSuccess?.Invoke();
        }
        
    }
    
   
    

    private void OnEnable()
    {
        GameStateEvents.AllEnemiesDied += OnAllEnemiesDied;
    }
    
    private void OnAllEnemiesDied()
    {
            isUnlocked = true;
        
    }

    private void OnDisable()
    {
        GameStateEvents.AllEnemiesDied -= OnAllEnemiesDied;
    }
}
