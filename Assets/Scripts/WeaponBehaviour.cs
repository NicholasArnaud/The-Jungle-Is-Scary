using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBehaviour : MonoBehaviour, IDamager
{

    bool hitMax, hitMin;

    public void DoDamage(float f)
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {
        hitMax = false;
        hitMin = true;
    }

    void Update()
    {
        var rot = transform.localRotation;
        if (rot.x >= 0)
        {
            hitMax = true;
            hitMin = false;
        }
        if (rot.x <= -.80f)
        {
            hitMin = true;
            hitMax = false;
        }
        if (hitMin == true)
            transform.RotateAround(new Vector3(.5f, 1, 0), new Vector3(1, 0, 0), 5);
        if (hitMax == true)
            transform.RotateAround(new Vector3(.5f, 1, 0), new Vector3(1, 0, 0), -5);   
    }
}
