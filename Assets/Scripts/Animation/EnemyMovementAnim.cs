using System;
using Events.InGameEvents;
using InGame.Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Animation
{
    public class EnemyMovementAnim:MonoBehaviour
    {

        private string m_currentAnim;
        private Animator m_animator;
        private EnemyPatrol enemyMovement;
        

        private const string LEFTorDOWN_MOVE = "RedEnemy_Left&Down";
        private const string RIGHTorUP_MOVE = "RedEnemy_Right&Up";

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            m_animator = GetComponent<Animator>();
            enemyMovement = GetComponent<EnemyPatrol>();
        }
        
        private void Update()
        {
            UpdateAnimation(enemyMovement.CurrentDirection);
        }
        

        private void UpdateAnimation(Vector2 direction)
        {
            if (direction == Vector2.down || direction == Vector2.left)
            {
                ChangeAnimationState(LEFTorDOWN_MOVE);
            }
            else if (direction == Vector2.up || direction == Vector2.right)
            {
                ChangeAnimationState(RIGHTorUP_MOVE);
            }
            
            
        }
        

        private void ChangeAnimationState(string newAnimation)
        {
            if (m_currentAnim == newAnimation) return;
            
            m_animator.Play(newAnimation);
            m_currentAnim = newAnimation;
        }
    }
}