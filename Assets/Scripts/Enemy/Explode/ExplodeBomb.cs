using System;
using System.Collections;
using System.Collections.Generic;
using Datas;
using Events.InGameEvents;
using ScriptableObjects.Variable;
using UnityEngine;
using UnityEngine.Serialization;

namespace InGame.Bomb
{
    public class ExplodeBomb : MonoBehaviour
    {
        public static event Action<List<Vector2>> OnExplode;
        [SerializeField] private GameObject m_normalBombExplodePrefab;
        private float m_raycastDist = 3f;
        private float m_timer;
        private bool m_bombDropped;
        private readonly float m_timerInterval = 2.5f;

        private List<Vector2> activeWays = new();

        private void OnEnable()
        {
            BombRaycast();
        }

        private void BombRaycast()
        {
            Vector2[] m_directions = { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
            float offsetDistance = 1f; 

            foreach (Vector2 direction in m_directions)
            {
                Vector2 raycastOrigin = (Vector2)transform.position + direction.normalized * offsetDistance;
                RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, direction, m_raycastDist);
                

                if (hit.collider != null && hit.collider.GetComponent<IWall>() != null)
                {
                    activeWays.Add(direction);
                }

                else
                {

                    if (hit.collider.gameObject != null)
                    {
                        Debug.Log(hit.collider.gameObject.name);
                    }
                }
            }
        }


        // public List<Vector2> ActiveWays()
        // {
        //     return activeWays;
        // }
        //
        private void Update()
        {
            m_timer += Time.deltaTime;

            if (m_timer > m_timerInterval)
            {
                Explode();
                OnExplode?.Invoke(activeWays);
                m_timer = 0;
                BombEvents.BombExploded?.Invoke();
                Destroy(gameObject);
            }
        }


        private void Explode()
        {
            Instantiate(m_normalBombExplodePrefab, transform.position, Quaternion.identity);
        }
    }

   
}