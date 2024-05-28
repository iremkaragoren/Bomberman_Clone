using System;
using System.Collections;
using System.Collections.Generic;
using Datas;
using Events.LevelEvent;
using InGame.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] private GameObject m_gameOverScene;
    [SerializeField] private GameObject m_stagePanel;
    [SerializeField] private GameObject m_mainPanel;
    [SerializeField] private TextMeshProUGUI m_stageText;

    private void Awake()
    {
        m_stageText.SetText((PlayerData.Instance.CurrentLevel + 1).ToString());
    }

    private void OnEnable()
    {
        GameStateEvents.GameOver += OnGameOver;
        LevelManager.OnLevelPreparationFinished += OnStageReady;
    }

    private void OnStageReady()
    {
        StartCoroutine(DelayedGameOverLoad_2());
        m_mainPanel.SetActive(true);
    }

    private void OnGameOver()
    {
        StartCoroutine(DelayedGameOverLoad());
    }

    private IEnumerator DelayedGameOverLoad()
    {
        yield return new WaitForSeconds(1.5f);
        m_gameOverScene.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(EnvironmentVariables.StarterSceneName);
    }

    private IEnumerator DelayedGameOverLoad_2()
    {
        yield return new WaitForSeconds(1.5f);
        
        GameStateEvents.LevelStart?.Invoke(PlayerData.Instance.CurrentLevel);
        
        m_stagePanel.SetActive(false);
    }

    private void OnDisable()
    {
        GameStateEvents.GameOver -= OnGameOver;
        LevelManager.OnLevelPreparationFinished -= OnStageReady;
    }
}