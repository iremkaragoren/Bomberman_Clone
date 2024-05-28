using System;
using System.Collections;
using System.Collections.Generic;
using Datas;
using Events.InGameEvents;
using Events.LevelEvent;
using ScriptableObjects.Predefined.Level;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{

    private int m_currentEnemyCount;
    


    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_currentEnemyCount = PlayerData.Instance.GetCurrentLevel().EnemyCount;
        
    }

    private void OnEnable()
    {
        BombEvents.TriggerEnemy += OnEnemyDeath;
    }

    private void OnEnemyDeath()
    {
        m_currentEnemyCount--;
        if (m_currentEnemyCount <= 0)
        {
            GameStateEvents.AllEnemiesDied?.Invoke();
        }
    }

    private void OnDisable()
    {
        
        BombEvents.TriggerEnemy -= OnEnemyDeath;
    }
}
