using UnityEngine;
using UnityEngine.UI;

namespace IFEN
{

    public class IpChildItem : RecyclingListViewItem {
        public Text ipText;
        // public Text rightText1;
        // public Text rightText2;
        public Button btnConnect;

        private IpData ipData;
        public IpData IpData {
            get { return ipData; }
            set {
                ipData = value;
                ipText.text = ipData.Ip;
                btnConnect.onClick.AddListener(OnClickConnectButton);
            }
        }

        private void OnClickConnectButton()
        {
            Debug.Log("Tapped IP: " + ipData.Ip);
            IpDiscoverManager.Instance.UpdateHost(ipData.Ip);
        }
    }

}