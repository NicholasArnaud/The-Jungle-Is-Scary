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
            rb.gameObject.GetComponent<PlayerController>().Speed.Value = 0;
            rb.gameObject.GetComponent<Player_Behaviour>().enabled = false;
        }

        else
        {
            sj.connectedBody = rb;
            rb.gameObject.GetComponent<PlayerController>().Speed.Value = 10;
            rb.gameObject.GetComponent<Player_Behaviour>().enabled = true;
        }

        swinging = !swinging;
    }
}
