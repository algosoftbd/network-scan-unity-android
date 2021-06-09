using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IFEN
{

    public class SettingsMenu : MonoBehaviour
    {
        public TMP_Dropdown qualityDropdown;
        public TMP_Dropdown resolutionDropdown;
        public TMP_Dropdown inputDropdown;
        public TMP_Dropdown conditionDropdown;
        public TMP_InputField thresholdInput;
        public Toggle fullscreenToggle;

        Resolution[] resolutions;

        private int inputIndex;
        private int conditionIndex;
        private float threshold;

        private void Awake()
        {
            inputIndex = PlayerPrefs.GetInt("InputIndex", SettingsVariables.DEFAULT_INPUT_INDEX);
            conditionIndex = PlayerPrefs.GetInt("ConditionIndex", SettingsVariables.DEFAULT_CONDITION_INDEX);
            threshold = PlayerPrefs.GetFloat("ThresholdValue", SettingsVariables.DEFAULT_THRESHOLD);
        }

        void Start()
        {
            inputDropdown.AddOptions(SettingsVariables.inputs);
            conditionDropdown.AddOptions(SettingsVariables.conditions);
            inputDropdown.value = inputIndex;
            conditionDropdown.value = conditionIndex;
            thresholdInput.text = threshold.ToString();
            
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.RefreshShownValue();

            fullscreenToggle.isOn = Screen.fullScreen;

            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            int currentResolutionIndex = 0;
            List<string> options = new List<string>();
            for (int i = 0; i < resolutions.Length; i++)
            {
                Resolution r = resolutions[i];
                options.Add(r.width + " x " + r.height);

                if (r.width == Screen.width &&
                    r.height == Screen.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            

            // On Value Changed Listeners
            qualityDropdown.onValueChanged.AddListener(SetQuality);
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
            conditionDropdown.onValueChanged.AddListener(OnConditionChanged);
            inputDropdown.onValueChanged.AddListener(OnInputChanged);
            //
        }

        public void ResetButton()
        {
            inputIndex = SettingsVariables.DEFAULT_INPUT_INDEX;
            conditionIndex = SettingsVariables.DEFAULT_CONDITION_INDEX;
            threshold = SettingsVariables.DEFAULT_THRESHOLD;

            inputDropdown.value = inputIndex;
            conditionDropdown.value = conditionIndex;
            thresholdInput.text = threshold.ToString();
        }

        public void SaveButton()
        {
            SaveAndUpdateApiReceiver();
        }

        private void SaveAndUpdateApiReceiver()
        {
            try
            {
                threshold = float.Parse(thresholdInput.text);
                PlayerPrefs.SetInt("InputIndex", inputIndex);
                PlayerPrefs.SetInt("ConditionIndex", conditionIndex);
                PlayerPrefs.SetFloat("ThresholdValue", threshold);
            
                UpdateApiReceiver();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateApiReceiver()
        {
            if (ApiReceiver.Instance != null)
            {
                ApiReceiver.Instance.conditionField = GameUtils.ParseEnum<ApiReceiver.ConditionField>(SettingsVariables.conditions[conditionIndex]);
                ApiReceiver.Instance.inputField = GameUtils.ParseEnum<ApiReceiver.InputField>(SettingsVariables.inputs[inputIndex]);
                ApiReceiver.Instance.threshold = threshold;
            }
        }

        private void OnConditionChanged(int index)
        {
            conditionIndex = index;
        }

        private void OnInputChanged(int index)
        {
            inputIndex = index;
        }
        

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
            Debug.Log("Quality: " + qualityIndex);
        }

        public void SetFullscreen(bool isFullScreen)
        {
            Screen.fullScreen = isFullScreen;
            Debug.Log("SetFullscreen: " + isFullScreen);
        }
    }
}
