using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBehaviour : MonoBehaviour, IDamager
{
    public Transform Player;
    public void Update()
    {
        transform.position = Player.transform.position + new Vector3(1, 0, 0);
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
} 
