using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Predefined.Level
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "ThisGame/Predefined/LevelSO", order = 1)]
    public class LevelSo : ScriptableObject
    {
        public int LevelsTime => m_levelsTime;
        public int GridWidth => m_gridWidth;
        public int GridHeight => m_gridHeight;

        public int DestroyableWall => m_destroyableWall;
        public List<Enemy_SO> EnemyTypeList => m_enemyTypesList;
        public List<Vector2Int> StarterGrid => m_starterGrid;
        public Vector2Int PlayerStartPos => _playerStartPos;
        public int EnemyCount => m_enemyCount;
    

        [SerializeField] private int m_levelsTime;
        [SerializeField] private int m_gridWidth;
        [SerializeField] private int m_gridHeight;
        [SerializeField] private int m_destroyableWall;
        [SerializeField] private Vector2Int _playerStartPos;
        [SerializeField] private int m_enemyCount;
        [SerializeField] private List<Enemy_SO> m_enemyTypesList;
        [SerializeField] private List<Vector2Int> m_starterGrid;
    }
}
