using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Network
{
    public static IEnumerator GetRequest(string uri, Action<string> func)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            //todo: get token from browser
            webRequest.SetRequestHeader("Authorization", "Token 58998a8632efec6b3810f7a2833dc300fe2a937f");
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                string response = webRequest.downloadHandler.text;
                func(response);
            }
        }
    }
}