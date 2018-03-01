using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSwingTestBehaviour : MonoBehaviour
{
    public bool swinging = false;    
    public void AttachAndSwing(Rigidbody rb)
    {
        
        if (swinging)
            GetComponent<FixedJoint>().connectedBody = null;
        else
            GetComponent<FixedJoint>().connectedBody = rb;
        swinging = !swinging;
    }
	
}
