using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBehaviour : MonoBehaviour, IDamager
{

    public void DoDamage(float f)
    {
        throw new NotImplementedException();
    }

    internal Rigidbody RBody;
    internal void Start()
    {
        this.RBody = this.gameObject.GetComponent<Rigidbody>();
        this.RBody.isKinematic = true;
        int childCount = this.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {

            Transform t = this.transform.GetChild(i);

            HingeJoint hinge = t.gameObject.GetComponent<HingeJoint>();
            hinge.connectedBody = i == 0 ? this.RBody : this.transform.GetChild(i - 1).GetComponent<Rigidbody>();

            hinge.useSpring = true;
            //hinge.enableCollision = true;
        }
    }


    void Update()
    {
        if (Input.GetButton("Fire1"))
            transform.Rotate(new Vector3(0,0,1), 10);

    }
}
