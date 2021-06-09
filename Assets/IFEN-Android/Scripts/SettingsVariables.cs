using System.Collections.Generic;

namespace IFEN
{
    public class SettingsVariables
    {
        public static int DEFAULT_INPUT_INDEX = 4;
        public static int DEFAULT_CONDITION_INDEX = 0;
        public static float DEFAULT_THRESHOLD = 0.0f;

        public static List<string> inputs = new List<string>()
        {
            "In1", "In2", "In3", "In4", "In5", "In6", "In7", "In8"
        };
        
        public static List<string> conditions = new List<string>()
        {
            "GT", "LT", "GTE", "LTE", "EQ", "NEQ"
        };
    }
}