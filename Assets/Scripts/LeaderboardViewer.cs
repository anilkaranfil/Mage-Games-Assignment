using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardViewer : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Leaderboard leaderboard;
    public RectTransform Container;
    public Mask Mask;
    public RectTransform Prefab;
    public int lengt = 10;
    public float Spacing;

    private RectTransform maskRT;
    private int numVisible;
    private int numBuffer = 2;
    private float containerHalfSize;
    private float prefabSize;

    private Dictionary<int, int[]> itemDict = new Dictionary<int, int[]>();
    private List<RectTransform> listItemRect = new List<RectTransform>();
    private List<LeaderboardItemUI> listItems = new List<LeaderboardItemUI>();
    private int numItems = 0;
    private Vector3 startPos;

    private void Awake()
    {
        leaderboard.leaderboardDataLoadingEvent += LeaderboardDataLoading;
    }

    void Start()
    {
        if (leaderboard.GetCurrentPage() == null)
        {
            leaderboard.GetPage(GenerateLeaderboardUIElements);
            return;
        }
        GenerateLeaderboardUIElements();
    }

    private void GenerateLeaderboardUIElements()
    {
        Container.anchoredPosition3D = new Vector3(0, 0, 0);

        maskRT = Mask.GetComponent<RectTransform>();

        Vector2 prefabScale = Prefab.rect.size;
        prefabSize = (prefabScale.y) + Spacing;

        Container.sizeDelta = (new Vector2(0, prefabSize * lengt));
        containerHalfSize = (Container.rect.size.y * 0.5f);

        numVisible = Mathf.CeilToInt((maskRT.rect.size.y) / prefabSize);

        startPos = Container.anchoredPosition3D - (Vector3.down * containerHalfSize) + (Vector3.down * ((prefabScale.y) * 0.5f));
        numItems = Mathf.Min(lengt, numVisible + numBuffer);
        for (int i = 0; i < numItems; i++)
        {
            GameObject obj = Instantiate(Prefab.gameObject, Container.transform);
            RectTransform t = obj.GetComponent<RectTransform>();
            t.anchoredPosition3D = startPos + (Vector3.down * i * prefabSize);
            listItemRect.Add(t);
            itemDict.Add(t.GetInstanceID(), new int[] { i, i });
            obj.SetActive(true);
            if (leaderboard.GetDataByIndex(i) == null)
            {
                continue;
            }
            LeaderboardItemUI li = obj.GetComponentInChildren<LeaderboardItemUI>();
            listItems.Add(li);
            Data data = leaderboard.GetCurrentPage().data[i];
            listItems[i].UpdateData(data.nickname, data.rank);
        }
        Prefab.gameObject.SetActive(false);
        Container.anchoredPosition3D += Vector3.down * (containerHalfSize - ((maskRT.rect.size.y) * 0.5f));
    }

    public void ReorderItemsByPos(float normPos)
    {
        normPos = 1f - normPos;
        int numOutOfView = Mathf.CeilToInt(normPos * (lengt - numVisible));   //number of elements beyond the left boundary (or top)
        int firstIndex = Mathf.Max(0, numOutOfView - numBuffer);   //index of first element beyond the left boundary (or top)
        int originalIndex = firstIndex % numItems;

        int newIndex = firstIndex;
        for (int i = originalIndex; i < numItems; i++)
        {
            Data data = leaderboard.GetDataByIndex(newIndex);
            if (data == null)
            {
                return;
            }
            MoveItemByIndex(listItemRect[i], newIndex);
            listItems[i].UpdateData(data.nickname,data.rank);
            newIndex++;
        }
        for (int i = 0; i < originalIndex; i++)
        {
            Data data = leaderboard.GetDataByIndex(newIndex);
            if (data == null)
            {
                return;
            }
            MoveItemByIndex(listItemRect[i], newIndex);
            listItems[i].UpdateData(data.nickname,data.rank);
            newIndex++;
        }
    }

    private void MoveItemByIndex(RectTransform item, int index)
    {
        int id = item.GetInstanceID();
        itemDict[id][0] = index;
        item.anchoredPosition3D = startPos + (Vector3.down * index * prefabSize);
    }

    public void LeaderboardDataLoading(object sender,bool isLoading)
    {
        if (isLoading)
        {
            scrollRect.StopMovement();
            scrollRect.vertical = false;
        }
        else
        {
            scrollRect.vertical = true;
        }
    }
}