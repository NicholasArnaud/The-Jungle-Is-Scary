using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "GLOBALGAMEMANAGER")]
public class GLOBALGAMEMANAGER : ScriptableObject
{
    public void LoadScene(int num)
    {
        SceneManager.LoadScene(num, LoadSceneMode.Single);
    }

    public static void SetSendersToInstantiatedClone(GameObject go, DataScriptable data)
    {
        foreach (var gameEventArgsListener in go.GetComponents<GameEventArgsListener>())
        {
            if (gameEventArgsListener.Sender.GetType() == typeof(EnemyDataScriptable) || gameEventArgsListener.Sender.GetType() == typeof(DataScriptable))
                gameEventArgsListener.Sender = data;
        }
    }
    public void Print(string value)
    {
        Debug.Log(value);
    }
    public void LoadSceneAdditive(int num)
    {
        SceneManager.LoadScene(num, LoadSceneMode.Additive);
    }
}
