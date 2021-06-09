using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace IFEN
{

    public class ApiReceiver : MonoBehaviour
    {
        [Serializable]
        public enum InputField
        {
            In1, In2, In3, In4, In5, In6, In7, In8, Inkey
        }
        
        [Serializable]
        public enum ConditionField
        {
            GT, LT, EQ, NEQ, GTE, LTE
        }

        [SerializeField] private BrainAvatarAPI brainAvatarAPI;
        [SerializeField] private GameObject networkErrorUi;
        
        
        public InputField inputField = InputField.In5;
        public ConditionField conditionField;
        public float threshold;
        
        // ScoreManager scoreManager;

        private float inputValue;
        public bool isRelaxTime = false;
        public static ApiReceiver Instance;
        //public bool isReceivingSignal = false;

        private void Awake()
        {
            Instance = this;

            int conditionIndex = PlayerPrefs.GetInt("ConditionIndex", SettingsVariables.DEFAULT_CONDITION_INDEX);
            int inputIndex = PlayerPrefs.GetInt("InputIndex", SettingsVariables.DEFAULT_INPUT_INDEX);
            threshold = PlayerPrefs.GetFloat("ThresholdValue", SettingsVariables.DEFAULT_THRESHOLD);
            
            conditionField = GameUtils.ParseEnum<ConditionField>(SettingsVariables.conditions[conditionIndex]);
            inputField = GameUtils.ParseEnum<InputField>(SettingsVariables.inputs[inputIndex]);
        }


        void Start()
        {
            // scoreManager = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<ScoreManager>();
        }

        void Update()
        {
            if (brainAvatarAPI.fetchApi)
            { 
                UpdateInputValues();
            }

            UpdateNetworkErrorUi();
            // UpdateContinuousScore();
        }

        /*private void UpdateContinuousScore()
        {
            scoreManager.UpdateTextUi(IsCriteriaMet());

            if(IsCriteriaMet())
            {
                scoreManager.Score += (Time.deltaTime * 6);
            }
        }*/

        private void UpdateNetworkErrorUi()
        {
            if (networkErrorUi == null) return;
            if (!brainAvatarAPI.fetchApi)
            {
                networkErrorUi.SetActive(false);
                return;
            }

            if (IsNetworkError())
            {
                Debug.Log("Network Error");

                if (!networkErrorUi.activeSelf)
                {
                    networkErrorUi.SetActive(true);
                }
            }
            else
            {
                if (networkErrorUi.activeSelf)
                {
                    networkErrorUi.SetActive(false);
                }
            }
        }

        public void StopReceiving()
        {
            brainAvatarAPI.fetchApi = false;
        }

        public void StartReceiving()
        {
            brainAvatarAPI.fetchApi = true;
        }

        private void UpdateInputValues()
        {
            // Debug.Log("Input -> " + brainAvatarAPI.input.Text(" | "));

            switch (inputField)
            {
                case InputField.In1:
                    inputValue = brainAvatarAPI.input.In1;
                    break;

                case InputField.In2:
                    inputValue = brainAvatarAPI.input.In2;
                    break;

                case InputField.In3:
                    inputValue = brainAvatarAPI.input.In3;
                    break;

                case InputField.In4:
                    inputValue = brainAvatarAPI.input.In4;
                    break;

                case InputField.In5:
                    inputValue = brainAvatarAPI.input.In5;
                    break;

                case InputField.In6:
                    inputValue = brainAvatarAPI.input.In6;
                    break;

                case InputField.In7:
                    inputValue = brainAvatarAPI.input.In7;
                    break;

                case InputField.In8:
                    inputValue = brainAvatarAPI.input.In8;
                    break;

                case InputField.Inkey:
                    inputValue = brainAvatarAPI.input.Inkey;
                    break;

            }
        }


        public bool IsCriteriaMet()
        {
            var result = false;
            switch (conditionField)
            {
                case ConditionField.EQ:
                    result = inputValue == threshold;
                    break;
                case ConditionField.NEQ:
                    result = inputValue != threshold;
                    break;
                case ConditionField.GT:
                    result = inputValue > threshold;
                    break;
                case ConditionField.LT:
                    result = inputValue < threshold;
                    break;;
                case ConditionField.GTE:
                    result = inputValue >= threshold;
                    break;
                case ConditionField.LTE:
                    result = inputValue <= threshold;
                    break;
            }
            result = result && !IsNetworkError() && !isRelaxTime;
            return result;
        }

        public bool IsNetworkError()
        {
            return brainAvatarAPI.isNetworkError;
        }
    }
}
