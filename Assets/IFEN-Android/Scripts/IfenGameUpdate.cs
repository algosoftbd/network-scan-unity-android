using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Gui;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

namespace IFEN
{
    public class IfenGameUpdate : MonoBehaviour
    {
        private string url = GameUtils.GAME_UPDATE_URL;

        public GameObject loadingUi;
        public GameObject updateAvailableUi;
        public GameObject versionUpToDateUi;
        
        private TextMeshProUGUI textUpdateAvailable;
        private LeanButton buttonUpdateNow;
        private TextMeshProUGUI textUpToDate;

        private void Awake()
        {
            textUpdateAvailable = updateAvailableUi.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            buttonUpdateNow = updateAvailableUi.transform.GetChild(1).GetComponent<LeanButton>();

            textUpToDate = versionUpToDateUi.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        void Start()
        {
            SetLoadingUi(true);
            FetchGameUpdate();
        }

        private void FetchGameUpdate()
        {
            GameUpdate currentUpdate = new GameUpdate();
            currentUpdate.gameid = GameUtils.GAME_ID;
            currentUpdate.version = GameUtils.GAME_VERSION;

            StartCoroutine(POST(JsonUtility.ToJson(currentUpdate)));
        }

        private void SetLoadingUi(bool isLoading)
        {
            loadingUi.SetActive(isLoading);
            updateAvailableUi.SetActive(false);
            versionUpToDateUi.SetActive(false);
        }

        private void SetUpdateAvailableUi(bool show)
        {
            loadingUi.SetActive(false);
            updateAvailableUi.SetActive(show);
            versionUpToDateUi.SetActive(false);
        }

        private void SetVersionUpToDateUi(bool show)
        {
            loadingUi.SetActive(false);
            updateAvailableUi.SetActive(false);
            versionUpToDateUi.SetActive(show);
        }

        public WWW POST(string jsonStr)
        {
            WWW www;
            Hashtable postHeader = new Hashtable();
            postHeader.Add("Content-Type", "application/json");

            // convert json string to byte
            var formData = System.Text.Encoding.UTF8.GetBytes(jsonStr);

            www = new WWW(url, formData, postHeader);
            StartCoroutine(WaitForRequest(www));
            return www;
        }

        IEnumerator WaitForRequest(WWW www)
        {
            yield return www;

            SetLoadingUi(false);
            GameUpdate latestUpdate = null;
            try
            {
                latestUpdate = JsonUtility.FromJson<GameUpdate>(www.text);
            }
            catch (Exception e)
            {
                Debug.Log("GameUpdate Error: " + e);
            }
            
            if (latestUpdate == null || latestUpdate.version == 0)
            {
                Debug.Log("GameUpdate is null");
            }
            else
            {
                if (latestUpdate.version > GameUtils.GAME_VERSION)
                {
                    SetUpdateAvailableUi(true);
                    buttonUpdateNow.OnClick.AddListener(() =>
                    {
                        Debug.Log("Update URL: " + latestUpdate.url);
                        Application.OpenURL(latestUpdate.url);
                    });
                    textUpdateAvailable.text = latestUpdate.updateText;
                }
                else
                {
                    textUpToDate.text = latestUpdate.upToDateText;
                    SetVersionUpToDateUi(true);
                }
            }
        }
    }
}
