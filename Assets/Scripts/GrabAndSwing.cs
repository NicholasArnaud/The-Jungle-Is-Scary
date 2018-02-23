using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndSwing : MonoBehaviour {

    public GameObject player;
    public AnimationCurve ac;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            player.transform.SetParent(null);
            player.GetComponent<Rigidbody>().useGravity = true;
            player.GetComponent<Player_Movement>().enabled = true;
        }


    }	
	
    void OnTriggerEnter(Collider other)
    {
        player.transform.SetParent(other.transform);
        player.GetComponent<Rigidbody>().useGravity = false;  
        player.GetComponent<Player_Movement>().enabled = false;
    }
}
