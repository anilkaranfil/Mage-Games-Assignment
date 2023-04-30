using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject leaderboard;
    public void OpenGameplayScene()
    {
        SceneLoader.OpenGameplay();
    }
    public void OpenLeaderboard(bool isActive)
    {
        leaderboard.SetActive(isActive);
    }

}