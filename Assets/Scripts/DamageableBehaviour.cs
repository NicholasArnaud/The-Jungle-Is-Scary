using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageableBehaviour : MonoBehaviour, IDamageable
{
    public GameEventArgs DamagedEvent;
    public GameEventArgs DeathEvent;
    public DataScriptable Data;
    private float immunityTimer = 0;

    void Update()
    {
        immunityTimer -= Time.deltaTime;
        immunityTimer = Mathf.Clamp(immunityTimer, 0, 1);
    }

    public void TakeDamage(int damage)
    {
        if (immunityTimer > 0) return;

        if (Data.Health <= 0)
            return;

        var newhp = Data.Health - damage;
        Data.Health = Mathf.Clamp(newhp, 0, 4);//assign new HP    
        DamagedEvent.Raise(Data);

        if (Data.Health < 1)
            DeathEvent.Raise(Data);

        immunityTimer = 1;
    }
}