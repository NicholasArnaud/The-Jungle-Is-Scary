using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBehaviour : MonoBehaviour, IDamager
{
    Transform _base;
    void Start()
    {
        _base = GetComponentInParent<Transform>();
    }
    public void DoDamage(IDamageable defender)
    {
        defender.TakeDamage(GetComponentInParent<Player_Behaviour>().data.lightDamage);
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "enemy")
            DoDamage(other.gameObject.GetComponent<Enemy_Behaviour>());
    }

    void Update()
    {
        //transform.RotateAround(o.transform.position, new Vector3(0, 1, 0), 5);
        if (Input.GetButton("Fire1"))
            _base.transform.Rotate(new Vector3(0, 0, 1), 1);
    }
}
