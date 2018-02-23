using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleBehaviour : MonoBehaviour {

    
    public GameObject branch;
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

        switch (Input.GetButtonDown("Fire1"))
        {
            case true:
                if (currentState == SwingState.SWINGING)
                    currentState = SwingState.IDLE;
                else
                    currentState = SwingState.SWINGING;
                break;
        }
        if (currentState == SwingState.SWINGING)
            Swing();
        
	} 

    void Swing()
    {
        transform.LookAt(branch.transform.position);
        Vector3.MoveTowards(transform.position, branch.transform.position, 5);
    }



}