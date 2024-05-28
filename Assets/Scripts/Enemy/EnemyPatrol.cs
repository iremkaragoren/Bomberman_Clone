using Events.LevelEvent;
using UnityEngine;
using UnityEngine.Serialization;

namespace InGame.Enemy
{
    public class EnemyPatrol : MonoBehaviour
    {
        [SerializeField] private float m_walkSpeed;
        [SerializeField] private LayerMask m_obstacleLayer;

        private Rigidbody2D m_rb;
        private Vector2 m_currentDirection;
        private Vector2 m_nextGridPoint;

        public Vector2 CurrentDirection => m_currentDirection;

        private const int m_maxAttempts = 20;
        private int currentAttempts = 0;

        private bool m_canMove = false;

        private void Awake()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_nextGridPoint = transform.position;
            ChooseNewDirection();
            GameStateEvents.LevelStart += (index) => { m_canMove = true; };
        }

        public void Init(bool movementActiveness = false)
        {
            m_canMove = movementActiveness;
        }

        private void FixedUpdate()
        {
            if (!m_canMove)
                return;

            if ((Vector2)transform.position != m_nextGridPoint)
            {
                MoveToNextGridPoint();
            }
            else
            {
                ChooseNewDirection();
            }
        }

        private void MoveToNextGridPoint()
        {
            Vector2 newPosition =
                Vector2.MoveTowards(m_rb.position, m_nextGridPoint, m_walkSpeed * Time.fixedDeltaTime);
            m_rb.MovePosition(newPosition);
        }

        private void ChooseNewDirection()
        {
            Vector2[] possibleDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
            currentAttempts = 0;

            do
            {
                m_currentDirection = possibleDirections[Random.Range(0, possibleDirections.Length)];
                m_nextGridPoint = (Vector2)transform.position + m_currentDirection;
                currentAttempts++;
            } while (IsPathBlocked(m_nextGridPoint) && currentAttempts < m_maxAttempts);
        }

        private bool IsPathBlocked(Vector2 targetPosition)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, targetPosition, m_obstacleLayer);

#if UNITY_EDITOR
            // Debug.DrawRay(transform.position, targetPosition, hit.collider != null ? Color.red : Color.green);
#endif
            return hit.collider != null;
        }
    }
}