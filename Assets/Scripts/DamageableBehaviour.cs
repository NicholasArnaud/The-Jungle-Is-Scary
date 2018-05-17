using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageableBehaviour : MonoBehaviour, IDamageable
{
    public GameEventArgs PlayerDamagedEvent;
    public GameEventArgs PlayerDiedEvent;
    public Player_Data PlayerData;
    private float immunityTimer = 0;

    void Update()
    {
        immunityTimer -= Time.deltaTime;
        immunityTimer = Mathf.Clamp(immunityTimer, 0, 1);
    }

    public void TakeDamage(int damage)
    {
        if (immunityTimer > 0) return;

        if (PlayerData.Hp <= 0)
            return;

        var newhp = PlayerData.Hp - damage;
        PlayerData.Hp = Mathf.Clamp(newhp, 0, 4);//assign new HP    
        PlayerDamagedEvent.Raise(PlayerData);

        if (PlayerData.Hp < 1)
            PlayerDiedEvent.Raise(PlayerData);

        immunityTimer = 1;
    }
}