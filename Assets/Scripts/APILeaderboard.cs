using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APILeaderboard
{
    public static IEnumerator GetRequest(string url,Action<Page> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    callback(null);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    callback(null);
                    Debug.LogError(String.Format("Someting went wrong :{0}", webRequest.error));
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("Request Success");
                    Page leaderboard = JsonConvert.DeserializeObject<Page>(webRequest.downloadHandler.text);
                    callback(leaderboard);
                    break;
            }
        }
    }
}