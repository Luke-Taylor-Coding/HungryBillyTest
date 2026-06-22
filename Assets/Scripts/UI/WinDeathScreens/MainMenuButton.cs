using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void MainMenu()
    {
        // Load the first scene in the build index
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
