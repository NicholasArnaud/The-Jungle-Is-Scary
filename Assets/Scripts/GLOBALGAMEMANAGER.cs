using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "GLOBALGAMEMANAGER")]
public class GLOBALGAMEMANAGER : ScriptableObject {
    public void LoadScene(int num)
    {
        SceneManager.LoadScene(num, LoadSceneMode.Single);
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
