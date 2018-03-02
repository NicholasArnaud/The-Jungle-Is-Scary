using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrow : MonoBehaviour {

    GameObject heldEnemy;
    public float throwForce;
    int heldAmt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {	
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            heldEnemy.transform.SetParent(null);
            heldEnemy.GetComponent<Rigidbody>().AddForce(Vector3.forward * throwForce);
            heldEnemy = null;
            heldAmt = 0;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (heldAmt != 0)
                return;
            other.transform.SetParent(transform);
            heldEnemy = other.gameObject;
            heldAmt = 1;
        }
    }
}
