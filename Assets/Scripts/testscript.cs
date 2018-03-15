using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    public GameObject Sunny;
    private Animator _animatorController;
    // Use this for initialization
    void Start ()
    {
        _animatorController = Sunny.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Q))
	    {
	        Sunny.GetComponent<BuffFlowerBehaviour>().Data.Health -=1;
            _animatorController.SetTrigger("Hit");
	    }
	}
}
