using System;
using System.Text;
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
        
        public static string BoolArrayToString(bool[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            StringBuilder sb = new StringBuilder();

            sb.Append(rows).Append(",").Append(cols).Append(";");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    sb.Append(array[i, j] ? '1' : '0');
                sb.Append('|');
            }
            return sb.ToString();
        }

        // Convert string back to bool[,]
        public static bool[,] StringToBoolArray(string data)
        {
            string[] parts = data.Split(';');
            string[] dims = parts[0].Split(',');
            int rows = int.Parse(dims[0]);
            int cols = int.Parse(dims[1]);
            bool[,] result = new bool[rows, cols];
            string[] rowStrings = parts[1].Split('|', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                result[i, j] = rowStrings[i][j] == '1';

            return result;
        }
        
         public static string CamelToSnake(string s)
        {
            s = Regex.Replace(s, "(.)([A-Z][a-z]+)", "$1_$2");
            s = Regex.Replace(s, "([a-z0-9])([A-Z])", "$1_$2");
            return s.Replace("__", "_").ToLower();
        }
    }
}