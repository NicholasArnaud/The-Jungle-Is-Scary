using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    public int speed;
    public float jumpHeight;
    public bool onFloor;

	// Use this for initialization
	void Start () {
        onFloor = true;
	}
	
	// Update is called once per frame
	void Update () {

        //Movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, 0, v);
        transform.localPosition += move * speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<BranchSwingTestBehaviour>().AttachAndSwing(GetComponent<Rigidbody>());
        }
            
        //Jumping
        //checks if player is on the floor to avoid infinite jumping
        //if (onFloor && Input.GetKeyDown(KeyCode.Space))
        //{

        //    //GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpHeight, 0));
        //    onFloor = !onFloor;
        //}
	}

    public void OnTriggerEnter(Collider collider)
    {
        // check if player if on
        if (collider.tag == "Floor")
            onFloor = true;
    }

}
