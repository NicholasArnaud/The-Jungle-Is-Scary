using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDisable : MonoBehaviour
{
    public GameEvent eventSystem;
	void OnDisable()
    {
        eventSystem.Raise();
    }
}
