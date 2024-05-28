using System;
using System.Collections;
using Datas;
using Events.InGameEvents;
using Events.LevelEvent;
using ScriptableObjects.Variable;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace InGame.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float m_walkSpeed;
        private float _horizontalInput;
        private float _verticalInput;

        private Rigidbody2D m_rb;
        private Animator m_animator;
        private CapsuleCollider2D m_collider;

        private IEnumerator coroutine;
        
        private bool m_canMove = false;


        private bool isMoving()
        {
            return _horizontalInput != 0 || _verticalInput != 0;
        }

        private void Awake()
        {
            Initialize();
        }

       
        private void Initialize()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_collider = GetComponent<CapsuleCollider2D>();
            m_animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            GameStateEvents.LevelFail += OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            m_animator.SetBool("isDeath", true);

            m_rb.isKinematic = m_collider.isTrigger = true;
             m_walkSpeed = 0;

            StartCoroutine(WaitForDeathAnimation());
        }


        private IEnumerator WaitForDeathAnimation()
        {
            var clipInfo = m_animator.GetCurrentAnimatorClipInfo(0);
            float waitTime = clipInfo.Length;

            yield return new WaitForSeconds(waitTime);

            Destroy(this.gameObject);
        }

        private void Update()
        {
            MovementInput();
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void MovementInput()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
        }

        private void Movement()
        {
            if (isMoving())
            {
                Vector2 movement = new Vector2(_horizontalInput, _verticalInput) *
                                   (PlayerData.Instance.GetAmountOfPowerUp(PowerUpTypes.SpeedUp) * m_walkSpeed) *
                                   Time.fixedDeltaTime;
                m_rb.MovePosition(m_rb.position + movement);
                
                m_animator.SetFloat("horizontalMovement", _horizontalInput);
                m_animator.SetFloat("verticalMovement", _verticalInput);
                m_animator.SetBool("isMoving", true);
            }
            else
            {
                m_animator.SetBool("isMoving", false);
            }
        }

        private void OnDisable()
        {
            GameStateEvents.LevelFail -= OnPlayerDeath;
        }
    }
}