using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUpdater : MonoBehaviour
{
    public EnemyDataScriptable Data;
    public Animator anim;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        Data.Alive = true;
    }
    
    public void ReduceHealth(Object[] args)
    {
        var obj = args[1] as GameObject;
        
        if (obj == this.gameObject)
            Data.Health-= 1;
        anim.SetTrigger("Damaged");
        Data.Alive = (Data.Health > 0);
        if (Data.Health < 1 && anim != null)
            anim.SetTrigger("dead");
    }
}
