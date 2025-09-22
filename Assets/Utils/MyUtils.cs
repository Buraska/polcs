using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UnityEngine;

namespace Utils
{
    public static class MyUtils
    {
        
        public static void Log(string message)
        {
            Debug.Log(message);
        }
        
        public static void LogWTF([CanBeNull] string message)
        {
            if (message == null)
            {
                message = "null";
            }
            Debug.Log("WTF: "+ message);
        }
        
         public static string CamelToSnake(string s)
        {
            s = Regex.Replace(s, "(.)([A-Z][a-z]+)", "$1_$2");
            s = Regex.Replace(s, "([a-z0-9])([A-Z])", "$1_$2");
            return s.Replace("__", "_").ToLower();
        }
    }
}