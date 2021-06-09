using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IFEN
{

    public class IPAddressManager : MonoBehaviour
    {
        public BrainAvatarAPI brainAvatarAPI;
        public InputField ipAddressInput;

        public void UpdateIPAddress()
        {
            string ip = ipAddressInput.text;
            Debug.Log("New IP: " + ip);
            brainAvatarAPI.SetIpAddress(ip);
        }

    }
}
