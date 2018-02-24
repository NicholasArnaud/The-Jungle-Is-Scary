using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behaviour : MonoBehaviour, IDamageable
{

    
    public Player_Data Data;
    Vector3 startPos;
    public GameEvent giveHealth;
    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        Data = ScriptableObject.CreateInstance<Player_Data>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Data.lives <= 0)
        {
            transform.position = startPos;
            giveHealth.Raise();
            Data.lives = 4;
        }
    }

    public void TakeDamage(int f)
    {
        Data.lives -= f;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, .5f, 0, .5f);
        Gizmos.DrawSphere(transform.position, Data.DetectionRadius);
    }
}
 