using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleBehaviour : MonoBehaviour {

    public enum SwingState
    {
        IDLE,
        SWINGING,
    }

    public SwingState currentState;

	// Use this for initialization
	void Start () {
        currentState = SwingState.IDLE;
	}
	
	// Update is called once per frame
	void Update () {

        switch (Input.GetMouseButtonDown(0))
        {
            case true:
                if (currentState == SwingState.SWINGING)
                    currentState = SwingState.IDLE;
                else
                    currentState = SwingState.SWINGING;
                break;
        }
	}
}