using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEnemyBehaviour : MonoBehaviour,IDamageable
{
    public GameEventArgs EnemyDamagedEvent;
    public GameEventArgs EnemyDiedEvent;
    public EnemyDataScriptable EnemyData;
    private float immunityTimer = 0;
	
	
	void Update ()
	{
	    immunityTimer -= Time.deltaTime;
	    immunityTimer = Mathf.Clamp(immunityTimer, 0, 1);
    }

    public void TakeDamage(int damage)
    {
        if (immunityTimer > 0) return;

        if (EnemyData.Health <= 0)
            return;

        var newhp = EnemyData.Health - damage;
        EnemyData.Health = Mathf.Clamp(newhp, 0, 10);//assign new HP    
        EnemyDamagedEvent.Raise(EnemyData);

        if (EnemyData.Health < 1)
        {
            EnemyData.Alive = false;
            EnemyDiedEvent.Raise(EnemyData);
        }
            

        immunityTimer = 1;
    }
}
