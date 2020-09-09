using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Utils
{
    public static class Network
    {
        public static IEnumerator GetRequest(string uri, Action<string> onResponse,
            Dictionary<string, string> headers = null, Action onError = null)
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
                    if (keyValuePair.Value.Trim() == "Token")
                    {
                        webRequest.SetRequestHeader(keyValuePair.Key, $"Token {URL.Token}");
                        // MainScreen.showDialog("token", URL.Token);
                    }
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

        public static IEnumerator PostRequest(string uri, string body, Action<string> onResponse,
            Dictionary<string, string> headers = null, bool json = false, Action onError = null)
        {
            if (headers == null)
            {
                headers = new Dictionary<string, string>();
            }

            using (var webRequest = json ? new UnityWebRequest(uri, "post") : UnityWebRequest.Post(uri, body))
            {
                foreach (var keyValuePair in headers)
                {
                    webRequest.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
                    if (keyValuePair.Value.Trim() == "Token")
                    {
                        webRequest.SetRequestHeader(keyValuePair.Key, $"Token {URL.Token}");
                        // MainScreen.showDialog("token", URL.Token);
                    }
                }
        
                if (json)
                {
                    webRequest.SetRequestHeader("Content-Type", "application/json");
                    var bodyRaw = Encoding.UTF8.GetBytes(body);
                    webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
                    webRequest.downloadHandler = new DownloadHandlerBuffer();
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

        public static IEnumerator GetTexture(string resource, Action<Texture> func,
            Dictionary<string, string> headers = null)
        {
            if (headers == null)
            {
                headers = new Dictionary<string, string>();
            }

            var request = UnityWebRequestTexture.GetTexture(resource);
            foreach (var keyValuePair in headers)
            {
                request.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
            }

            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Texture myTexture = DownloadHandlerTexture.GetContent(request);
                func(myTexture);
            }
        }

        public static IEnumerator PutRequest(string uri, string body, Action<string> onResponse,
            Dictionary<string, string> headers = null, bool json = false, Action onError = null)
        {
            if (headers == null)
            {
                headers = new Dictionary<string, string>();
            }

            using (var webRequest = new UnityWebRequest(uri, "put"))
            {
                foreach (var keyValuePair in headers)
                {
                    webRequest.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
                }

                if (json)
                {
                    webRequest.SetRequestHeader("Content-Type", "application/json");
                }

                var bodyRaw = Encoding.UTF8.GetBytes(body);
                webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
                webRequest.downloadHandler = new DownloadHandlerBuffer();

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