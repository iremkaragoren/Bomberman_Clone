using Events.LevelEvent;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TimerCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_timerText;
        private int m_timer;
        public int Timer => m_timer;

        private void OnEnable()
        {
            GameStateEvents.TimeIsDecrease += OnLevelTimeWorking;
        }

        private void OnLevelTimeWorking(int _currenttime)
        {
            m_timer = _currenttime;
            m_timerText.text = m_timer.ToString();
        }


        private void OnDisable()
        {
            GameStateEvents.TimeIsDecrease -= OnLevelTimeWorking;
        }
    }
}
