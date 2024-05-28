using System.Collections;
using Datas;
using Events.LevelEvent;
using ScriptableObjects.Predefined.Level;
using UnityEngine;

namespace InGame.Managers
{
    public class LevelTimeHandler : MonoBehaviour
    {
        private int m_currentTime;
        private int m_counter;
        private Coroutine m_counterCoroutine;

        private void OnEnable()
        {
            GameStateEvents.LevelStart += OnCounterStart;
            GameStateEvents.LevelFail += OnCounterStop;

        }
        private void OnDisable()
        {
            GameStateEvents.LevelStart -= OnCounterStart;
            GameStateEvents.LevelFail -= OnCounterStop;
        }
        
        IEnumerator Counter()
        {
            m_counter = PlayerData.Instance.GetCurrentLevel().LevelsTime;
            m_currentTime = m_counter;

            while (m_currentTime > 0)
            {
                yield return new WaitForSeconds(1f);
               m_currentTime-= 1;
               
               GameStateEvents.TimeIsDecrease?.Invoke(m_currentTime);
            }
            
            GameStateEvents.LevelFail?.Invoke();
        }
        
        private void OnCounterStart(int _levelStart)
        {
            m_counterCoroutine = StartCoroutine(Counter());
        }
        
        private void OnCounterStop()
        {
            if (m_counterCoroutine != null)
            {
                StopCoroutine(m_counterCoroutine);
                m_counterCoroutine = null;
            }
        }
    }
}