using System;
using System.Collections;
using System.Collections.Generic;
using Events.InGameEvents;
using InGame.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyInstantiate : MonoBehaviour
{
    [SerializeField] private EnemyPatrol m_enemyPatrol;
    private Animator m_animator;
    private Vector2 m_doorPos;
    private DoorDetector _doorDetector;

    private void Awake()
    {
        _doorDetector = GetComponent<DoorDetector>();
    }

    private void OnEnable()
 {
     BombEvents.TriggerDoor += OnDoorExploded;
     
 }
 
 public void Init(Vector2 doorPos)
 {
     m_doorPos = doorPos;
 }
    
 private void OnDoorExploded()
 {

     if (!_doorDetector.canInteract)
     {
         _doorDetector.canInteract = true; 
     }
     else
     {
         StartCoroutine(WaitUntil());
     }
    
 }

 private IEnumerator WaitUntil()
 {
     yield return new WaitForSeconds(1.5f);
     for (int i = 0; i < 5; i++)
     {
         var tmpEnemy = Instantiate(m_enemyPatrol, m_doorPos, Quaternion.identity);
         tmpEnemy.Init(true);
     }
 }
 
 private void OnDisable()
 {
     BombEvents.TriggerDoor -= OnDoorExploded;
 }
 
}
