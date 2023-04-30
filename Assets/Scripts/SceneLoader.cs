using UnityEngine.SceneManagement;

public class SceneLoader
{
    private static readonly int mainMenu = 0;
    private static readonly int gameplay = 1;

    public static void OpenMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public static void OpenGameplay()
    {
        SceneManager.LoadScene(gameplay);
    }
}