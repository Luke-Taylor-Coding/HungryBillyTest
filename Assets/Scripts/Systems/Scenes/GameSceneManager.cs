using System.Collections;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.SceneManagement;

// SceneManager for loading and managing scenes
// Supports loading scenes by name and index as well as async loading 
// NOTE: this class is a singleton class

public class GameSceneManager : MonoBehaviour
{
    private AsyncOperation m_pendingScene;
    public static GameSceneManager s_instance { get; private set; }

    void Awake()
    {
        // Singleton logic
        if (s_instance != null && s_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        s_instance = this;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadNextScene()
    {
        int next = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        LoadScene(next);
    }

    public void ReloadCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Loads a scene asynchronously, with the option to control scene activation and load mode
    /// if sceneActivationOnLoad is set to false, the scene will be loaded but not activated until ActivatePendingScene() is called
    /// </summary>
    public void LoadSceneAsync(string SceeneName, bool sceneActivationOnLoad = true, LoadSceneMode sceneMode = LoadSceneMode.Single) 
    {
        StartCoroutine(LoadSceneAsyncCoroutine(SceeneName, sceneActivationOnLoad, sceneMode));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName, bool sceneActivationOnLoad = true, LoadSceneMode sceneMode = LoadSceneMode.Single)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName, sceneMode);
        if (asyncLoad == null)
        {
            yield break;
        }

        asyncLoad.allowSceneActivation = sceneActivationOnLoad;

        while (!asyncLoad.isDone)
        {
            if (!sceneActivationOnLoad && asyncLoad.progress >= 0.9f)
            {
                m_pendingScene = asyncLoad; // store for later activation
                yield break;
            }

            yield return null;
        }
    }

    public void ActivatePendingScene()
    {
        if (m_pendingScene != null)
        {
            m_pendingScene.allowSceneActivation = true;
            m_pendingScene = null;
        }
    }

    public void LoadMainMenu()
    {
        LoadScene(0);
    }
}
