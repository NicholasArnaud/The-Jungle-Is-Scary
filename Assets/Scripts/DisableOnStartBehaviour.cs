using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStartBehaviour : MonoBehaviour
{
    public GameEventArgsResponse Response;
	// Use this for initialization
	void Start ()
	{
        Response.AddListener(delegate {gameObject.SetActive(false);});
	    Response.Invoke(null);
	}
	
 
}
