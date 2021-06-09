using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IFEN
{

    public class LoginPauseMenu : MonoBehaviour
    {
        //public static bool GameIsPaused = false;

        public TextMeshProUGUI infoText;

        void Update()
        {

            /*if (GameIsPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }*/
        }

        public void Resume()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            //GameIsPaused = false;
        }

        public void Pause(string info)
        {
            infoText.text = info;
            gameObject.SetActive(true);
            Time.timeScale = 0f;
            //GameIsPaused = true;
        }
    }
}