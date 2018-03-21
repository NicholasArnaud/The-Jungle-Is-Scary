using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSwingTestBehaviour : MonoBehaviour
{
    public bool swinging = false;
    SpringJoint sj;
    void Start()
    {
        sj = GetComponent<SpringJoint>();
    }

    public void AttachAndSwing(Rigidbody rb)
    {
        if (swinging)
        {
            sj.connectedBody = null;
            rb.useGravity = false;
            rb.gameObject.GetComponent<PlayerController>().enabled = true;
            rb.gameObject.GetComponent<Player_Behaviour>().enabled = true;
        }

        else
        {
            sj.connectedBody = rb;
            rb.useGravity = true;
            rb.gameObject.GetComponent<PlayerController>().enabled = false;
            rb.gameObject.GetComponent<Player_Behaviour>().enabled = false;
        }

        swinging = !swinging;
    }
}
