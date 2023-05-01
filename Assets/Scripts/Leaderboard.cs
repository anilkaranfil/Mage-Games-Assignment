using System;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public const string apiTarget = "https://magegamessite.web.app/case1/leaderboard_page_";
    public const string apiExtention = ".json";

    private Dictionary<int, Page> pages = new Dictionary<int, Page>();
    [SerializeField] private int currentPageIndex;
    public EventHandler<bool> leaderboardDataLoadingEvent;

    public void TryRequest(string url, Action onLoad)
    {
        StartCoroutine(APILeaderboard.GetRequest(url, page =>
        {
            if (page == null)
            {
                Debug.LogError("Data empty or Null");
                return;
            }
            pages.Add(page.page, page);
            leaderboardDataLoadingEvent.Invoke(this, false);
            onLoad?.Invoke();
            Debug.Log("Data Loaded");
        }));
    }

    public void GetPage(Action callback)
    {
        TryRequest(apiTarget + currentPageIndex.ToString() + apiExtention, callback);
    }
    public Page GetCurrentPage()
    {
        return pages.ContainsKey(currentPageIndex) ? pages[currentPageIndex] : null;
    }
    public Data GetDataByIndex(int index)
    {
        int targetPage = Mathf.FloorToInt(index / 10);
        if (pages.ContainsKey(targetPage))
        {
            return pages[targetPage].data[index % 10];
        }

        if (!pages[pages.Count - 1].is_last)
        {
            leaderboardDataLoadingEvent.Invoke(this, true);
            TryRequest(apiTarget + targetPage.ToString() + apiExtention, null);
            return null;
        }
        return null;
    }
    public int GetLoadedLeaderboardLenght()
    {
        int lenght = 0;
        for (int i = 0; i < pages.Count; i++)
        {
            lenght += pages[i].data.Length;
        }
        return lenght;
    }
}
