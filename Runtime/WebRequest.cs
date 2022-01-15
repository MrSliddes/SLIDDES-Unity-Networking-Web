using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

namespace SLIDDES.Networking.Web
{
    /// <summary>
    /// For handling web requests
    /// </summary>
    public static class WebRequest
    {
        /// <summary>
        /// Debug.logs the data fetched from the url
        /// </summary>
        /// <param name="url">The website url to get data from</param>
        /// <returns>Debug.Log result</returns>
        /// <remarks>This does not return json data, for that use GetJson</remarks>
        /// <seealso cref="GetJson(string, System.Action{string})"/>
        public static IEnumerator Get(string url)
        {
            using(UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = url.Split('/');
                int page = pages.Length - 1;

#if UNITY_2020_3_OR_NEWER
                switch(webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        Debug.LogError("[WebRequest] Connection Error: " + pages[page] + ": Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError("[WebRequest] Data Processing Error: " + pages[page] + ": Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError("[WebRequest] Protocol Error: " + pages[page] + ": HTTP Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.Success:
                        Debug.Log("[WebRequest] Success: " + pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                        break;
                }
#elif UNITY_2019_4_OR_NEWER
                if(webRequest.isNetworkError)
                {
                    Debug.Log("[WebRequest] Network Error: " + pages[page] + ": Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log("[WebRequest] Success: " + pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                }
#endif
            }
            yield break;
        }

        /// <summary>
        /// Get json result from web address
        /// </summary>
        /// <param name="url">The website url to get data from</param>
        /// <param name="result">This calls back an action with a json ready string</param>
        /// <returns>Json formated string. In case of error = Connection Error: 1000; DataProcessingError: 1001; ProtocolError: 1002;</returns>
        /// <example>
        /// string result = "";
        /// yield return StartCoroutine(WebRequest.GetJson(url, x => result = x));
        /// </example>
        public static IEnumerator GetJson(string url, System.Action<string> result)
        {
            using(UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = url.Split('/');
                int page = pages.Length - 1;

#if UNITY_2020_3_OR_NEWER
                switch(webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        Debug.LogError("[WebRequest] Connection Error: " + pages[page] + ": Error: " + webRequest.error);
                        result("{\"result\":\"1000\"}"); 
                        yield break;
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError("[WebRequest] Data Processing Error: " + pages[page] + ": Error: " + webRequest.error);
                        result("{\"result\":\"1001\"}"); 
                        yield break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError("[WebRequest] Protocol Error: " + pages[page] + ": HTTP Error: " + webRequest.error);
                        result("{\"result\":\"1002\"}"); 
                        yield break;
                    case UnityWebRequest.Result.Success:
                        string s = webRequest.downloadHandler.text;
                        result(s); 
                        yield break;
                }
#elif UNITY_2019_4_OR_NEWER
                if (webRequest.isNetworkError)
                {
                    Debug.Log("[WebRequest] Network Error: " + pages[page] + ": Error: " + webRequest.error);
                }
                else
                {
                    string s = webRequest.downloadHandler.text;
                    result(s); 
                    yield break;
                }
#endif
            }
            yield break;
        }
    }
}