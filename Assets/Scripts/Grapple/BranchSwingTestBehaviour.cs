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
        }
            
        else
        {
            sj.connectedBody = rb;

        }
            
        swinging = !swinging;
    }
}
