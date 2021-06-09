using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IFEN
{

    public class LoaderScript : MonoBehaviour
    {

        void Start()
        {
            StartCoroutine(SimpleDelay());
        }

        void Update()
        {

        }


        IEnumerator SimpleDelay()
        {
            Debug.Log("start");
            yield return new WaitForSeconds(3);

            Debug.Log("finish");
            //Debug.Log(auth.CurrentUser);

            if (PlayerPrefs.GetInt("Status") == 1)
            {
                SceneManager.LoadScene(GameUtils.SCENE_MENU);
            }
            else
            {
                SceneManager.LoadScene(GameUtils.SCENE_LOGIN);
            }
        }
    }
}
