using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Datas;
using TMPro;
using Utils; 

namespace UI.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button m_starterButton;
        [SerializeField] private Button m_continueButton;
        
        
        
        
        private void Awake()
        {
            m_starterButton.onClick.AddListener(OnStarterButtonClicked);
            m_continueButton.onClick.AddListener(OnContinueButtonClicked);
            
            
        }

        private void OnStarterButtonClicked()
        {
            PlayerData.Instance.InitializeNewGame();
            StartCoroutine(DelayedSceneLoad());

        }

        private void OnContinueButtonClicked()
        {
            PlayerData.Instance.ContinueGame(); 
            StartCoroutine(DelayedSceneLoad());
           
        }

        IEnumerator DelayedSceneLoad()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(EnvironmentVariables.MainSceneName);
            
        }
    }
}