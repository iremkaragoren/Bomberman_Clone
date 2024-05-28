using System;
using Datas;
using Events.LevelEvent;
using ScriptableObjects.Predefined.Player;
using TMPro;
using UnityEngine;

namespace UI.Manager
{
    public class HealthTextAssigner:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_healthText;

        private void OnEnable()
        {
            AssignText();
        }

        private void AssignText()
        {
            m_healthText.text = PlayerData.Instance.CurrentHealth.ToString();
        }

    }
}