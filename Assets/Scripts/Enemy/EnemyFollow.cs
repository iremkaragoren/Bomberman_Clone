using InGame.Enemy;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float m_followSpeed;
    [SerializeField] private GameObject m_playerGO;
    [SerializeField] private LayerMask playerLayer;

    private EnemyPatrol m_enemyPatrol;
    public float dist; 
    private const int m_maxAttempts = 10;
    private int currentAttempts = 0;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_enemyPatrol = GetComponent<EnemyPatrol>();
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        currentAttempts = 0;

        do
        {
            Vector2 distanceVector = transform.position - m_playerGO.transform.position;
            float distanceLength = distanceVector.magnitude;

            if (distanceLength < 3)
            {
                transform.position = Vector2.MoveTowards(transform.position, m_playerGO.transform.position,
                    m_followSpeed * Time.deltaTime);
                currentAttempts++;
            }
            else
            {
                break;
            }
        } while (true && currentAttempts < m_maxAttempts);
    }

    bool CanSeePlayer()
    {
        Vector2 currentDirection = m_enemyPatrol.CurrentDirection;

        return CheckDirectionForPlayer(currentDirection) || CheckDirectionForPlayer(-currentDirection);
    }

    private bool CheckDirectionForPlayer(Vector2 direction)
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, dist, playerLayer);

#if UNITY_EDITOR
        Debug.DrawRay(transform.position, direction, hit.collider != null ? Color.red : Color.green);
#endif

        return hit.collider != null && hit.collider.gameObject == m_playerGO;
    }
}