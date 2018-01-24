using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour {

    GameObject target;
	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 1);
	}
}
