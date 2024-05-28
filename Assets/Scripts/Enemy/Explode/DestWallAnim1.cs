using System.Collections;
using System.Collections.Generic;
using Events.InGameEvents;
using UnityEngine;

public class DestWallAnim1 : MonoBehaviour,IWall

{
    private Animator m_animator;

    private void OnEnable()
    {
        BombEvents.TriggerDestroyableWall += PlayDestroyableWallAnimation;
    }

    private void PlayDestroyableWallAnimation(GameObject wallObject)
    {
        m_animator = wallObject.GetComponent<Animator>();

        if (m_animator != null)
        {
            m_animator.enabled = true;
            m_animator.SetBool("DestroyableWall", true);
            StartCoroutine(DelayedDestWallDestroy(wallObject));
        }
     
    }

    IEnumerator DelayedDestWallDestroy(GameObject wallObject)
    {
        var clipInfo = m_animator.GetCurrentAnimatorClipInfo(0);
        float waitTime = clipInfo.Length;

        yield return new WaitForSeconds(waitTime);
        
        Destroy(wallObject);
    }

    private void OnDisable()
    {
        BombEvents.TriggerDestroyableWall -= PlayDestroyableWallAnimation;
    }
}


