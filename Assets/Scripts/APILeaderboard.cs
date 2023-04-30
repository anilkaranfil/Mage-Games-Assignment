using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APILeaderboard : MonoBehaviour
{
    public Page[] leaderboard;
    public string[] leaderboardPages;
    
    private void Start()
    {
        //test
        leaderboard = new Page[leaderboardPages.Length];
        for (int i = 0; i < leaderboardPages.Length; i++)
        {
            StartCoroutine(GetRequest(leaderboardPages[i]));
        }
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(String.Format("Someting went wrong :{0}", webRequest.error));
                    break;
                case UnityWebRequest.Result.Success:
                    Page leaderboard = JsonConvert.DeserializeObject<Page>(webRequest.downloadHandler.text);
                    this.leaderboard[leaderboard.page] = leaderboard;
                    break;
            }
        }
    }
}
