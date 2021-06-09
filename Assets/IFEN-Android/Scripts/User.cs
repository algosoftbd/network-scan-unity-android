using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IFEN
{
    [System.Serializable]
    public class User
    {
        public string name;
        public List<string> items;
        public string email;
        public bool passwordChanged;


        public string GetString()
        {
            return name + ", " + email + ", " + items.ToString() + ", " + passwordChanged;
        }
    }

}