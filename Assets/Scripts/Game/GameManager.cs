using UnityEngine;

// A class to manage the game and its state / events such as on player death or game win

public class GameManager : MonoBehaviour
{
    public void OnPlayerDeath()
    {
#if UNITY_EDITOR
        Debug.Log("GAME_MANAGER: Player has died. Game Over.");
#endif
        // Implement game over logic here
    }

    public void OnGameWin()
    {
#if UNITY_EDITOR
        Debug.Log("GAME_MANAGER: Player has won the game!");
#endif
        // Implement game win logic here
    }
}
