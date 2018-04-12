using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine : MonoBehaviour
{
    private static bool _created;
    private static bool _loaded;
    public string StartUpSceneName;
    public string LoadingSceneName;
    public string InGameSceneName;
    public GameContext Gamecontext;

    private void Awake()
    {
        if (_created) return;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(StartUpSceneName, LoadSceneMode.Single);
        _created = true;
        Debug.Log("Awake: "+ gameObject);
    }

    public void EnterGameScene()
    {
        if (_loaded) return;
        _loaded = true;
        SceneManager.LoadScene(LoadingSceneName);
        StartCoroutine(LoadNewScene());
    }

    public void ExitGameScene()
    {
        if (SceneManager.GetActiveScene().name == InGameSceneName)
        {
            SceneManager.LoadScene(StartUpSceneName, LoadSceneMode.Single);
        }
    }

    public IEnumerator LoadNewScene()
    {
        var async = SceneManager.LoadSceneAsync(InGameSceneName);
        while (!async.isDone)
        {
            yield return null;
        }
    }
}
