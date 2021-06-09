using System;
using UnityEngine;

namespace IFEN
{
    public static class GameUtils
    {
        public static T ParseEnum<T>(string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }

        public static int API_PORT = 40510;
        public static string API_PROTOCOL = "ws";
        public static string API_HOST = "localhost";
        
        public static string GAME_ID = "ifen-yogamaster-vr";
        public static string GAME_ROOT_URL = "https://ifen-game-verification.appspot.com";
        // public static string GAME_ROOT_URL = "http://localhost:8080";
        public static string GAME_LOGIN_URL = GAME_ROOT_URL + "/login";
        public static string GAME_UPDATE_URL = GAME_ROOT_URL + "/update";
        public static float GAME_VERSION = 1.2f;
        public static string BUY_URL = "https://www.neurofeedback-partner.de/product_info.php?info=p364_yogamaster-neurofeedback-spiel.html";

        public static string SCENE_GAME = "Menu";
        public static string SCENE_GAME_TRIAL = "Menu";
        public static string SCENE_MENU = "Menu";
        public static string SCENE_LOADER = "Loader";
        public static string SCENE_IP_DISCOVER = "IP Discover";
        public static string SCENE_LOGIN = "Login";
    }
}