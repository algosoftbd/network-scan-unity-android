using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp;
using Newtonsoft.Json;
using UnityEngine.Serialization;

namespace IFEN
{
    public class BrainAvatarAPI : MonoBehaviour
    {
        private string host = GameUtils.API_HOST;
        private int port = GameUtils.API_PORT;
        private string protocol = GameUtils.API_PROTOCOL;

        [HideInInspector]
        public bool isNetworkError = false;

        public bool fetchApi = false;

        [HideInInspector]
        public Input input;
        
        WebSocket ws;

        private void OnEnable()
        {
            host = PlayerPrefs.GetString("host", GameUtils.API_HOST);
            InitWebSocket();
        }

        /**
         * DEPRECATED
         * Used for TCP connection.
         */
        IEnumerator GetRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                if (webRequest.isNetworkError)
                {
                    isNetworkError = true;
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
                }
                else
                {
                    isNetworkError = false;
                    var rawData = webRequest.downloadHandler.text;
                    input = JsonUtility.FromJson<Input>(rawData);
                    // Debug.Log(input.Text(", "));
                }
            }
        }

        public void SetIpAddress(string hostAddress)
        {
            host = hostAddress;
            InitWebSocket();
        }

        IEnumerator WebSocketReconnectCoroutine()
        {
            Debug.Log("Reconnecting ...");
            yield return null;
            InitWebSocket();
            Debug.Log("Reconnect Done!");
        }
        
        private void InitWebSocket()
        {
            if (ws != null)
            {
                ws.Close();
                ws = null;
            }

            string fullAddress = GetFullAddress();
            Debug.Log(fullAddress);
            ws = new WebSocket(fullAddress);
            ws.OnOpen += (sender, args) =>
            {
                isNetworkError = false;
                Debug.Log("Message connected");
            };
            ws.OnError += (sender, args) =>
            {
                Debug.Log("message error");
                ws.Close();
            };
            ws.OnClose += (sender, args) =>
            {
                isNetworkError = true;
                Debug.Log("message close");
                ws.ConnectAsync();
            };
            ws.OnMessage += (sender, e) =>
            {
                isNetworkError = false;
                
                if (!fetchApi) return;
                
                Debug.Log("Message Received from "+((WebSocket)sender).Url+", Data : "+e.Data);
                string data = e.Data;

                Dictionary<string,string> dicQueryString = 
                    data.Split('&')
                        .ToDictionary(c => c.Split('=')[0],
                            c => Uri.UnescapeDataString(c.Split('=')[1]));
                string json = JsonConvert.SerializeObject(dicQueryString);
                // Debug.Log(json);
                input = JsonUtility.FromJson<Input>(json);
            };
            ws.ConnectAsync();
        }

        private string GetFullAddress()
        {
            return protocol + "://" + host + ":" + port;
        }
    }
}
