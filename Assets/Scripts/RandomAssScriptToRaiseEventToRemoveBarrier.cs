using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAssScriptToRaiseEventToRemoveBarrier : MonoBehaviour
{

    public GameEvent OnEnemiesDead;
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.R))
		    OnEnemiesDead.Raise();
	}
}
