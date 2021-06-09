using System.Collections;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;
using Debug = UnityEngine.Debug;
using Ping = UnityEngine.Ping;

namespace IFEN
{
    public class IpDiscoverManager: MonoBehaviour
    {
        public static IpDiscoverManager Instance;

        private static int MAX_IP_DISCOVER = 255;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            PlayerPrefs.SetString("host", null);
            DiscoverNow();
        }

        public void UpdateHost(string ip)
        {
            PlayerPrefs.SetString("host", ip);
            SceneManager.LoadScene(GameUtils.SCENE_LOADER);
        }

        private void DiscoverNow()
        {
            string existingHost = PlayerPrefs.GetString("host");
            CheckPing(existingHost);
            
            string baseIP = GetBaseIp();
            Debug.Log("Base IP: " + baseIP);
            // IpDiscoverUi.Instance.CheckingIp("Base IP: " + baseIP);
            for (int i = 1; i <= MAX_IP_DISCOVER; i++)
            {
                string ip = baseIP + i;
                if (ip == existingHost) continue;
                CheckPing(ip);
            }
        }

        private string GetDefaultGatewayAndroid()
        {
            AndroidJavaObject jo = new AndroidJavaObject("com.test1.test");
            AndroidJavaClass act = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            return jo.Call<string>("getHostIp");
        }

        private IPAddress GetDefaultGateway()
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
                .Select(g => g?.Address)
                // .Where(a => a.AddressFamily == AddressFamily.InterNetwork)
                // .Where(a => Array.FindIndex(a.GetAddressBytes(), b => b != 0) >= 0)
                .FirstOrDefault(a => a != null);
        }
        
        private string GetBaseIp()
        {
            string gateway = GetDefaultGatewayAndroid();
            // IpDiscoverUi.Instance.CheckingIp("Gateway: " + gateway);
            string[] ipParts = gateway.Split('.');
            string ip = "";

            for (int i = 0; i < ipParts.Length - 1; i++)
            {
                ip += ipParts[i] + ".";
            }

            return ip;
        }
        
        private void CheckPing(string ip)
        {
            StartCoroutine(StartPing(ip));
        }
 
        private IEnumerator StartPing(string ip)
        {
            WaitForSeconds f = new WaitForSeconds(0.01f);
            Ping p = new Ping(ip);
            while (p.isDone == false)
            {
                yield return f;
            }

            // ipDiscoverCount++;
            PingFinished(p);
        }


        private void PingFinished(Ping p)
        {
            string host = p.ip;
            UpdateIpText(host);
            string fullAddress = GameUtils.API_PROTOCOL + "://" + host + ":" + GameUtils.API_PORT;
            WebSocket ws = new WebSocket(fullAddress);
            ws.OnOpen += (sender, args) =>
            {
                MainThreadWorker.Instance.AddAction(()=>
                {
                    IpDiscoverUi.Instance.AddNewIp(host);
                    Debug.Log("Found: " + host);    
                });
                ws.Close();
            };
            
            ws.OnClose += (sender, args) =>
            {
                Debug.Log("Close: " + host);
            };
            
            ws.OnError += (sender, args) =>
            {
                ws.Close();
            };
            
            ws.ConnectAsync();
        }

        private void UpdateIpText(string host)
        {
            // if (ipDiscoverCount >= MAX_IP_DISCOVER) return;
            string text = "Checking: " + host;
            IpDiscoverUi.Instance.UpdateIpCheckingText(text);
        }

    }
}
