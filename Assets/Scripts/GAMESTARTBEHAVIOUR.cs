using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GAMESTARTBEHAVIOUR : MonoBehaviour
{

    public GameEventArgsResponse Response;

    void Start()
    {
        Response.Invoke(null);
    }

}
