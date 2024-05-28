using Datas;
using Events.InGameEvents;
using Events.LevelEvent;
using ScriptableObjects.Variable;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreTextAssigner : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_scoreText;
    
        private int m_score;
        public int Score => m_score;

        
        private void OnEnable()
        {
            BombEvents.TriggerEnemy += ScoreIncrease;
            GameStateEvents.LevelStart += OnLevelStart;
        }

        private void OnLevelStart(int arg0)
        {
            ScoreIncrease();
        }

        private void ScoreIncrease()
        {
            m_scoreText.text = PlayerData.Instance.Score.ToString();
        }

        private void OnDisable()
        {
            BombEvents.TriggerEnemy -= ScoreIncrease;
            GameStateEvents.LevelStart -= OnLevelStart;
        }
    }
}
