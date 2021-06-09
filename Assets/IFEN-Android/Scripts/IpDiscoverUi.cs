using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace IFEN
{
    public struct IpData
    {
        public string Ip;

        public IpData(string ip)
        {
            Ip = ip;
        }
    }

    public class IpDiscoverUi: MonoBehaviour
    {
        public RecyclingListView theList;
        public GameObject theLoading;
        public TextMeshProUGUI ipFoundText;
        public TextMeshProUGUI ipCheckingText1;
        public TextMeshProUGUI ipCheckingText2;
        
        public static IpDiscoverUi Instance;
        
        private List<IpData> data = new List<IpData>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start() {
            theLoading.SetActive(true);
            theList.ItemCallback = PopulateItem;
            // RetrieveData();
            //
            // // This will resize the list and cause callbacks to PopulateItem for
            // // items that are needed for the view
            // theList.RowCount = data.Count;
        }

        public void UpdateIpCheckingText(string text)
        {
            ipCheckingText1.text = text;
            ipCheckingText2.text = "Looking for more PCs";
        }

        public void AddNewIp(string ip)
        {
            theLoading.SetActive(false);
            data.Add(new IpData(ip));
            theList.RowCount = data.Count;
            ipFoundText.text = data.Count + " PC Found";
        }

        private void PopulateItem(RecyclingListViewItem item, int rowIndex) {
            var child = item as IpChildItem;
            if (child != null)
            {
                child.IpData = data[rowIndex];
            }
        }
    }
}
