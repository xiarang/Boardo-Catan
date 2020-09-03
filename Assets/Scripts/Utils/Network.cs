﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Utils
{
    public static class Network
    {
        public static IEnumerator GetRequest(string uri, Action<string> onResponse,Dictionary<string, string> headers = null, Action onError = null)
        {
            if (headers == null)
            {
                headers = new Dictionary<string, string>();
            }
            using (var webRequest = UnityWebRequest.Get(uri))
            {
                foreach (var keyValuePair in headers)
                {
                    webRequest.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
                }
                yield return webRequest.SendWebRequest();

                var pages = uri.Split('/');
                var page = pages.Length - 1;

                if (webRequest.isNetworkError)
                {
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
                    onError?.Invoke();
                }
                else
                {
                    var response = webRequest.downloadHandler.text;
                    onResponse(response);
                }
            }
        }

        public static IEnumerator PostRequest(string uri, string body, Action<string> onResponse, Dictionary<string, string> headers = null, Action onError = null)
        {
            if (headers == null)
            {
                headers = new Dictionary<string, string>();
            }
            using (var webRequest = UnityWebRequest.Post(uri, body))
            {
                foreach (var keyValuePair in headers)
                {
                    webRequest.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
                }
                yield return webRequest.SendWebRequest();
                var pages = uri.Split('/');
                var page = pages.Length - 1;

                if (webRequest.isNetworkError)
                {
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
                    onError?.Invoke();
                }
                else
                {
                    var response = webRequest.downloadHandler.text;
                    onResponse(response);
                }
            }
        }
    }
}