using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerBehaviour : MonoBehaviour, IDamager
{
    [Range(0, 10)]
    public int DamageAmount = 1;
    public void DoDamage(IDamageable defender)
    {
        if (defender == GetComponent<IDamageable>())
            return;
        defender.TakeDamage(DamageAmount);
    }
 
}
