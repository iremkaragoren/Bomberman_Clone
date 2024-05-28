using System.Collections;
using UnityEngine;
using Datas;
using Events.InGameEvents;
using ScriptableObjects.Variable; // Ensure you are using the correct namespace for PlayerData

namespace InGame.Player
{
    public class PlayerDropBomb : MonoBehaviour
    {
        [SerializeField] private GameObject m_bombPrefab;
        private GameObject m_BombGO;

        private Rigidbody2D _rb;
        private Vector2 m_currentPlayerPosition;
        private int m_droppedBombCount;
        
        private Coroutine _coroutine;

        private void Update()
        {
            DropBombInput();
        }

        private void OnEnable()
        {
            BombEvents.BombExploded += OnBombExploded;
        }

        private void OnDisable()
        {
            BombEvents.BombExploded -= OnBombExploded;
        }

        private void OnBombExploded()
        {
            m_droppedBombCount = Mathf.Max(0, m_droppedBombCount - 1);
        }

        private void DropBombInput()
        {
            if (!Input.GetKeyDown(KeyCode.X)) return;
            if (!IsBombAvailable()) return;

            DropBomb();
            BombCoroutine();
            StopBombCoroutine();

            PlayerEvents.DropBomb?.Invoke();
            m_droppedBombCount++;
        }

        private void BombCoroutine()
        {
            _coroutine = StartCoroutine(IsTriggerActive());
        }

        private void StopBombCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        IEnumerator IsTriggerActive()
        {
            _rb = m_BombGO.GetComponent<Rigidbody2D>();
            _rb.isKinematic = false;
            yield return new WaitForSeconds(0.01f);
        }

        private void DropBomb()
        {
            m_currentPlayerPosition = transform.position;
            Vector2 gridPosition = RoundToGridPivotPoint(m_currentPlayerPosition);
            m_BombGO = Instantiate(m_bombPrefab, gridPosition, Quaternion.identity);
        }
        

        private Vector2 RoundToGridPivotPoint(Vector2 position)
        {
            float _cellSize = 1.0f;  
            float x = Mathf.Round(position.x / _cellSize) * _cellSize;
            float y = Mathf.Round(position.y / _cellSize) * _cellSize;
            return new Vector2(x, y);
        }

        private bool IsBombAvailable()
        {
            int maxBombCount = PlayerData.Instance.GetAmountOfPowerUp(PowerUpTypes.BombUp) ;
            return m_droppedBombCount < maxBombCount;
        }
    }
}
