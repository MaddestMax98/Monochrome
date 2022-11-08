using ScripatbleObj;
using System.IO;
using UnityEngine;

namespace Manager
{

    public static class SaveManager
    {
        public static void SaveGame()
        {

        }

        private static void SavePlayerData()
        {

        }
        
        public static void SaveMannequinItemList()
        {
            /*
            string json = JsonUtility.ToJson();
            Debug.Log(json);
            WriteToSaveFile(json);
            */

        }

        private static void WriteToSaveFile(string data)
        {
            File.WriteAllText(Application.dataPath + "/save.json", data);
        }
         
    }
}

