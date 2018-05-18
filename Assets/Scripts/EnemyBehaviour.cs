using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagerBehaviour))]
public class EnemyBehaviour : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        var damager = GetComponent<IDamager>();
        var defender = other.GetComponent<IDamageable>();
        damager.DoDamage(defender);
    }
}
