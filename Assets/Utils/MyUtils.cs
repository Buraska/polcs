using UnityEngine;

namespace Utils
{
    public static class MyUtils
    {
        
        public static void Log(string message)
        {
            Debug.Log(message);
        }
        
        public static void LogWTF(string message)
        {
            Debug.Log("WTF: "+ message);
        }
    }
}