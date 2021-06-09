using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace IFEN
{
    public class TrialManagement : MonoBehaviour
    {
        private const int TOTAL_TRIAL = 10;
        private const string KEY_TRIAL_REMAINS = "trialRemains";
        private int trialRemains;

        public Button buttonBuyNow;
        public Button buttonStartTrial;
        public TextMeshProUGUI trialRemainsText;

        public GameObject panelTrialStart;
        public GameObject panelTrialFinished;

        void Start()
        {
            buttonBuyNow.onClick.AddListener(HandleBuyNow);
            // PlayerPrefs.SetInt(KEY_TRIAL_REMAINS, TOTAL_TRIAL);
            trialRemains = PlayerPrefs.GetInt(KEY_TRIAL_REMAINS, -1);
            if (trialRemains == -1)
            {
                trialRemains = TOTAL_TRIAL;
                PlayerPrefs.SetInt(KEY_TRIAL_REMAINS, trialRemains);
            }

            if (trialRemains > 0)
            {
                panelTrialStart.SetActive(true);
                panelTrialFinished.SetActive(false);
                buttonStartTrial.onClick.AddListener(HandleStartTrial);
                UpdateTrialRemainText();
            }
            else
            {
                panelTrialStart.SetActive(false);
                panelTrialFinished.SetActive(true);
            }
        }


        void UpdateTrialRemainText()
        {
            if (trialRemains > 1)
            {
                trialRemainsText.text = "( " + trialRemains + " Remains )";
            }
            else
            {
                trialRemainsText.text = "( " + trialRemains + " Remain )";
            }
        }

        void HandleStartTrial()
        {
            trialRemains--;
            PlayerPrefs.SetInt(KEY_TRIAL_REMAINS, trialRemains);
            SceneManager.LoadScene(GameUtils.SCENE_GAME_TRIAL);
        }

        void HandleBuyNow()
        {
            Application.OpenURL(GameUtils.BUY_URL);
        }
    }
    
}
