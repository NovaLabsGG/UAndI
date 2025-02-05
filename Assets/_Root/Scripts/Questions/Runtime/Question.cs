using System;
using UnityEngine;

namespace _Root.Scripts.Questions.Runtime
{
    [Serializable]
    public class Question
    {
        public string text;
        public string[] options;
    }
    
    public static class JsonHelper
    {
        
        public static T[] FromJson<T>(string json)
        {
            string newJson = "{\"array\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] array;
        }
    }
}