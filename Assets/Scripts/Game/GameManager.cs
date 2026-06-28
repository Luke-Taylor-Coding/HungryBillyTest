using UnityEngine;
using UnityEngine.Events;

// A class to manage the game and its state / events such as on player death or game win

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent m_onPlayerDeath;
    [SerializeField] private UnityEvent m_onGameWin;

    public void OnPlayerDeath()
    {
#if UNITY_EDITOR
        Debug.Log("GAME_MANAGER: Player has died. Game Over.");
#endif
        m_onPlayerDeath.Invoke();
    }

    public void OnGameWin()
    {
#if UNITY_EDITOR
        Debug.Log("GAME_MANAGER: Player has won the game!");
#endif
        // Implement game win logic here
        m_onGameWin.Invoke();
    }
}
