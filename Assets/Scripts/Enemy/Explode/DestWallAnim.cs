using System;
using System.Collections;
using System.Collections.Generic;
using Events.InGameEvents;
using UnityEngine;

public class DestWallAnim : MonoBehaviour
{
   private Animator m_animator;

   private void OnEnable()
   {
      BombEvents.TriggerDestroyableWall += PlayDestroyableWallAnimation;
   }

   private void PlayDestroyableWallAnimation(GameObject wallObject)
   {
      Animator animator = wallObject.GetComponent<Animator>();

      animator.enabled = true;
      animator.SetBool("DestroyableWall", true);
      StartCoroutine(DelayedDestWallDestroy());
     
   }

   IEnumerator DelayedDestWallDestroy()
   {
      var clipInfo = m_animator.GetCurrentAnimatorClipInfo(0);
      float waitTime = clipInfo.Length;

      yield return new WaitForSeconds(waitTime);
      Destroy(m_animator.gameObject);
      
      
   }

   private void OnDisable()
   {
      BombEvents.TriggerDestroyableWall -= PlayDestroyableWallAnimation;
   }
}
