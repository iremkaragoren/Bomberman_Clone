using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Animation;
using Cinemachine;
using Datas;
using Events.InGameEvents;
using Events.LevelEvent;
using Extensions.System;
using InGame.Enemy;
using InGame.PowerUp;
using Installers;
using ScriptableObjects.Predefined.Level;
using ScriptableObjects.Variable;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InGame.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static event Action OnLevelPreparationFinished; 
        
        [SerializeField] private DataContainer m_dataContainer;

        [SerializeField] private List<PowerUps_SO> _avaiblePowerUp;
        [SerializeField] private int m_destroyableWallCount;
        [SerializeField] private CinemachineVirtualCamera m_virtualCamere;
        [SerializeField] private Transform m_levelHolder;

        private int m_width;
        private int m_height;
        private int[,] m_grid;

        private GameObject m_instantiatedGo;
        private Vector3 m_destroyableWallPos;
        private List<Vector3> m_placeablePositions = new();
        private List<Vector3> m_emptyPosition = new();
        private PolygonCollider2D m_gridColl;
        private LevelSo m_LevelSo;


        private void Awake()
        {
            string jsonFilePath = Path.Combine(Application.persistentDataPath, "dosya_adi.json");
            Debug.Log("JSON Dosya Yolu: " + jsonFilePath);
            Debug.Log("Persistent Data Path: " + Application.persistentDataPath);
            Initialize();
        }

        private void Initialize()
        {
            if (GameInstaller.Instance == null)
            {
                Debug.LogError("GameInstaller.Instance is null. Make sure the GameInstaller is correctly instantiated.");
                return;
            }
            
            if (GameInstaller.Instance.LevelListSo == null)
            {
                Debug.LogError("LevelListSo is null. Make sure it is correctly assigned in the GameInstaller.");
                return;
            }
            
            if (GameInstaller.Instance.PlayerData == null)
            {
                Debug.LogError("PlayerData is null. Make sure it is correctly assigned in the GameInstaller.");
                return;
            }

            m_LevelSo = GameInstaller.Instance.LevelListSo.Levels[GameInstaller.Instance.PlayerData.CurrentLevel];
            
            if (m_LevelSo == null)
            {
                Debug.LogError("Level data (m_LevelSo) is null. Make sure the current level index is valid.");
                return;
            }

            m_width = m_LevelSo.GridWidth;
            m_height = m_LevelSo.GridHeight;
        }

        private void Start()
        {
            if (m_width % 2 == 0) m_width++;

            if (m_height % 2 == 0) m_height++;

            GenerateGridData(m_width, m_height);
            PlayerInstantiatePosition();
            PlaceDestroyableWalls(m_grid);
            InstantiatePowerUps();
            InstantiateEnemy(m_grid);
            InstantiateDoor();
            OnLevelPreparationFinished?.Invoke();
        }

        private void GenerateGridData(int width, int height)
        {
            m_grid = new int [width, height];


            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1 || (x % 2 == 0 && y % 2 == 0))
                    {
                        m_grid[x, y] = 1;
                        InstantiateGridObj(x, y, m_dataContainer.wallPrefab);
                    }

                    else
                    {
                        m_grid[x, y] = 0;
                        InstantiateGridObj(x, y, m_dataContainer.groundPrefab);
                        m_emptyPosition.Add(m_instantiatedGo.transform.position);
                    }
                }
            }
        }

        private void PlayerInstantiatePosition()
        {
            Debug.Log((Vector2)m_LevelSo.PlayerStartPos);
            GameObject player = Instantiate(m_dataContainer.playerPrefab, (Vector2)m_LevelSo.PlayerStartPos,
                Quaternion.identity);
            m_virtualCamere.Follow = player.transform;
            PlayerEvents.PlayerPos?.Invoke(player);
        }

        private List<Vector2> GetEmptyPositions(int[,] grid)
        {
            List<Vector2> emptyPositions = new List<Vector2>();

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 0 && !m_LevelSo.StarterGrid.Contains(new Vector2Int(i, j)))
                        emptyPositions.Add(new Vector2(i, j));
                }
            }

            return emptyPositions;
        }


        private void PlaceDestroyableWalls(int[,] grid)
        {
            var emptyPositionsList = GetEmptyPositions(grid);

            int wallToPlaceCount = Math.Min(m_destroyableWallCount, emptyPositionsList.Count);


            for (int i = 0; i < wallToPlaceCount; i++)
            {
                Vector2 destroyablePosition = emptyPositionsList.PullRandom();
                if (destroyablePosition == m_LevelSo.PlayerStartPos)
                    continue;
                Vector2 _destrWallPos = new(destroyablePosition.x, destroyablePosition.y);
                m_placeablePositions.Add(_destrWallPos);

                GameObject instantiatedGo = Instantiate(m_dataContainer.destroyableWallPrefab, _destrWallPos,
                    Quaternion.identity);
                instantiatedGo.transform.SetParent(m_levelHolder);
            }
        }

        private List<T> SelectRandomElements<T>(List<T> list, int maxElements)
        {
            int numberOfElementsToSelect = Random.Range(0, Math.Min(maxElements, list.Count) + 1);
            var selected = new List<T>();

            for (int i = 0; i < numberOfElementsToSelect; i++)
            {
                int index = Random.Range(0, list.Count);
                selected.Add(list[index]);
                list.RemoveAt(index);
            }

            return selected;
        }


        private void InstantiatePowerUps()
        {
            var selectedPowerUps = SelectRandomElements(_avaiblePowerUp, 2);

            foreach (PowerUps_SO powerUp in selectedPowerUps)
            {
                Vector2 powerUpPos = m_placeablePositions.PullRandom();
                GameObject instantiatedGo = Instantiate(powerUp.PowerUpGO, powerUpPos, Quaternion.identity);
                Debug.Log("xx" + powerUp.name);
                instantiatedGo.GetComponent<PowerUpDetector>().Init(powerUp);
                instantiatedGo.transform.SetParent(m_levelHolder);
            }
        }

        private void InstantiateDoor()
        {
            Vector2 doorPos = m_placeablePositions.PullRandom();
            GameObject _doorGO = Instantiate(m_dataContainer.doorPrefab, doorPos, Quaternion.identity);
            _doorGO.GetComponent<EnemyInstantiate>().Init(doorPos);
        }


        private void InstantiateEnemy(int[,] grid)
        {
            List<Vector2> emptyPositions =
                GetEmptyPositions(grid).Where(pos => !m_placeablePositions.Contains(pos)).ToList();

            foreach (var enemy in m_LevelSo.EnemyTypeList)
            {
                for (int i = 0; i < m_LevelSo.EnemyCount; i++)
                {
                    Vector2 enemyPos = emptyPositions.PullRandom();
                    if (enemyPos == m_LevelSo.PlayerStartPos)
                        continue;
                    GameObject enemyGO = Instantiate(enemy.EnemyPrefab, enemyPos, Quaternion.identity);
                    enemyGO.GetComponent<EnemyFollow>().dist = enemy.AmountRaycast;
                    enemyGO.transform.SetParent(m_levelHolder);
                }
            }
        }


        private void InstantiateGridObj(int x, int y, GameObject prefab)
        {
            Vector2 wallPosition = new(x, y);
            m_instantiatedGo = Instantiate(prefab, wallPosition, Quaternion.identity);
            m_instantiatedGo.transform.SetParent(m_levelHolder);
        }
    }
}