using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagerBehaviour),typeof(PhysicsTriggerListener))]
public class EnemyBehaviour : MonoBehaviour
{
    public void OnPhysicsTriggerListenerTriggerEnter(Object[] args)
    {
        var sender = args[0] as GameObject;//sender should be this gameobject
        var other = args[1] as GameObject;//other should be who sender entered
        if (sender == null || other == null)//if either of these isn't what we think they are then drop
            return;

        var damager = sender.GetComponent<IDamager>();//set the attacker
        var defender = other.GetComponent<IDamageable>();//set the defender
        damager.DoDamage(defender);//do the damage
    }
}
