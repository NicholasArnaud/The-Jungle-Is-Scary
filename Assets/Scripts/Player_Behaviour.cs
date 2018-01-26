using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behaviour : MonoBehaviour,IDamageable {

    public Player_Data data;
    Vector3 startPos;
    public GameEvent giveHealth;
    // Use this for initialization
    void Start () {
        startPos = transform.position;
        data = ScriptableObject.CreateInstance<Player_Data>();
	}
	
	// Update is called once per frame
	void Update () {
        if (data.lives <= 0)
        {
            transform.position = startPos;
            giveHealth.Raise();
            data.lives = 4;
        }         
    }

    public void TakeDamage(int f)
    {
        data.lives -= f;
    } 
}
