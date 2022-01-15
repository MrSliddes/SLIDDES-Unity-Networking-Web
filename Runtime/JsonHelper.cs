using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SLIDDES.Networking.Web
{
    /// <summary>
    /// Helper class to process Json data string arrays to unity
    /// </summary>
    /// <credit>https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity</credit>
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        /// <summary>
        /// If this is a Json array from the server and you did not create it by hand
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Json formatted string</returns>
        public static string FixJson(string value)
        {
            value = "{\"Items\":" + value + "}";
            return value;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}