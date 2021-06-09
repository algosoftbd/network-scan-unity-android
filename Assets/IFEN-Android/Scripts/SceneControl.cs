using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace IFEN
{

    public class SceneControl : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI welcomeText;
        [SerializeField]
        TextMeshProUGUI versionText;

        [SerializeField] private GameObject logoutButton;

        void Start()
        {
            string userEmail = "N/A";
            if (welcomeText != null)
            {
                string email = PlayerPrefs.GetString("Email");
                if (!string.IsNullOrEmpty(email))
                    welcomeText.text = "Welcome,\n" + email;
                else
                {
                    welcomeText.text = "Trial Version";
                    logoutButton.SetActive(false);
                }
            }
            if (versionText != null)
            {
                versionText.text = "Version: " + IFEN.GameUtils.GAME_VERSION;
            }
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene(GameUtils.SCENE_GAME);
        }
        public void LoadMenuScene()
        {
            SceneManager.LoadScene(GameUtils.SCENE_MENU);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Logout()
        {
            // logout code here
            PlayerPrefs.SetInt("Status", -1);
            PlayerPrefs.SetString("Email", null);
            SceneManager.LoadScene(GameUtils.SCENE_LOADER);
        }

        public void BuyNow()
        {
            // logout code here
            Application.OpenURL(GameUtils.BUY_URL);
        }
    }
}
