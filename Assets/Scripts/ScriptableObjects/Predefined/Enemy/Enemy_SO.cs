using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_SO", menuName = "ThisGame/Predefined/EnemySO", order = 5)]
public class Enemy_SO : ScriptableObject
{
    public GameObject EnemyPrefab => enemyPrefab;
    public float AmountRaycast => m_amountRaycast;
    public EnemyType EnemyType => m_enemyType;
    
    [SerializeField] private float m_amountRaycast;
    [SerializeField] private EnemyType m_enemyType;
    [SerializeField] private GameObject enemyPrefab;
}
[Serializable]
public enum EnemyType
{
    blueEnemy,
    redEnemy,
    purpleEnemy
}
