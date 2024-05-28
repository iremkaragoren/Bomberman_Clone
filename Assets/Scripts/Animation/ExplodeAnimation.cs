using System;
using System.Collections;
using System.Collections.Generic;
using Events.InGameEvents;
using UnityEngine;

public class ExplodeAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] explodeSprites;
    [SerializeField] private float m_AnimationSpeed = 0.2f;
    private SpriteRenderer m_spriteRenderer;
    private Coroutine m_animCoroutine;
    

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        OnExplodeAnim();
    }
   
    private void OnExplodeAnim()
    {
        StartAnimCoroutine();
    }
    
    private void StartAnimCoroutine()
    {
       m_animCoroutine= StartCoroutine(ExplodeSprite(explodeSprites));
    }

    private IEnumerator ExplodeSprite (Sprite[] sprites)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            m_spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(m_AnimationSpeed);
        }
        
        Destroy(gameObject.transform.parent.gameObject);
        
    }

    private void StopAnimCoroutine()
    {
        if (m_animCoroutine != null)
        {
            StopCoroutine(m_animCoroutine);
            m_animCoroutine = null;
        }
    }


    private void OnDisable()
    {
        StopAnimCoroutine();
        BombEvents.BombExploded -= OnExplodeAnim;
    }
}