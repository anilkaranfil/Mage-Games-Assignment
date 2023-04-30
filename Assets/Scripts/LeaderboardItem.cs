using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardItem : MonoBehaviour
{
    public TextMeshProUGUI TextMeshProUGUI;
    
    public void UpdateData(int rank,int score,string username)
    {
        TextMeshProUGUI.text = rank.ToString() + " " + username + " " + score.ToString();
    }
}