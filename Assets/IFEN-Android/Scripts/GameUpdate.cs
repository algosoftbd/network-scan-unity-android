using System;

namespace IFEN
{
    [Serializable]
    public class GameUpdate
    {
        public string gameid;
        public float version;
        public string url;
        public string updateText;
        public string upToDateText;
    }
}