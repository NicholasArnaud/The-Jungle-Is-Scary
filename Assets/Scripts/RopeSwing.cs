using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour
{

    HingeJoint hinge;

    // Use this for initialization
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        hinge.connectedBody = other.gameObject.GetComponent<Rigidbody>();
        other.gameObject.GetComponentInParent<Rigidbody>().useGravity = false;
    }
}
