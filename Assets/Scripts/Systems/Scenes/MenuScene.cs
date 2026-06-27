using UnityEngine;

public class MenuScene : MonoBehaviour
{
    private void Start()
    {
        // pre-load the game scene on start
        GameSceneManager.s_instance.LoadSceneAsync("GameScene", false);
    }
}
