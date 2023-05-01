using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardItemUI : MonoBehaviour
{
    public TextMeshProUGUI TextMeshProUGUI;
    
    public void UpdateData(string rank,int rankk)
    {
        TextMeshProUGUI.text = rank + " " + rankk.ToString();
    }
}