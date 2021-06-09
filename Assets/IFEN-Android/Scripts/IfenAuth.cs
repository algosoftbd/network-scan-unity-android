using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

namespace IFEN
{

    public class IfenAuth : MonoBehaviour
    {
        private string _gameId = GameUtils.GAME_ID;
        private string url = GameUtils.GAME_LOGIN_URL;

        public TMP_InputField inputEmail;
        public TMP_InputField inputPassword;
        public Button buttonGo;
        public TextMeshProUGUI textPleaseWait;
        public LoginPauseMenu loginPauseMenu;

        bool IsVerifiedAndPurchased = false;
        bool IsError = false;
        string errorText = "";

        void Start()
        {
            loginPauseMenu.Resume();
            UpdateLoadingUi(false);
        }

        void Update()
        {
            if (IsError)
            {
                if (errorText.ToLower().Contains("no user"))
                {
                    errorText = "You're not registered yet";
                }
                loginPauseMenu.Pause(errorText);
                IsError = false;
            }

            if (IsVerifiedAndPurchased)
            {
                SceneManager.LoadScene(GameUtils.SCENE_GAME);
            }
        }

        public void HandleLogin()
        {
            string email = inputEmail.text;
            string password = inputPassword.text;
            string pcid = SystemInfo.deviceUniqueIdentifier;
            Debug.Log(email + ", " + password + ", pcid: " + pcid);

            LoginUser loginUser = new LoginUser();
            loginUser.email = email;
            loginUser.password = password;
            loginUser.pcid = pcid;
            loginUser.gameid = _gameId;

            StartCoroutine(POST(JsonUtility.ToJson(loginUser)));
        }

        public void UpdateLoadingUi(bool isLoading)
        {
            inputEmail.interactable = !isLoading;
            inputPassword.interactable = !isLoading;
            buttonGo.interactable = !isLoading;
            textPleaseWait.gameObject.SetActive(isLoading);
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

            Response response = JsonUtility.FromJson<Response>(www.text);
            Debug.Log(response.info);

            if (response.status == 200)
            {
                //CheckUser(response.user);
                IsVerifiedAndPurchased = true;
                PlayerPrefs.SetInt("Status", 1);
                PlayerPrefs.SetString("Email", response.user.email);
                Debug.Log("Verified And Purchased");
            }
            else
            {
                errorText = response.info;
                IsError = true;
            }
        }

        /*void CheckUser(User user)
        {
            if (user.items == null)
            {
                errorText = "Please purchase this game";
                IsError = true;
                return;
            }

            if (user.items.Contains(GAME_ID))
            {
                IsVerifiedAndPurchased = true;
                PlayerPrefs.SetInt("Status", 1);
                PlayerPrefs.SetString("Email", user.email);
                Debug.Log("Verified And Purchased");
            }
            else
            {
                errorText = "Please purchase this game";
                IsError = true;
            }
        }*/

        class LoginUser
        {
            public string email;
            public string password;
            public string pcid;
            public string gameid;
        }
    }
}
