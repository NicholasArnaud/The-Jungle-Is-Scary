using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behaviour : MonoBehaviour,IDamageable {

    public Player_Data data;
    Vector3 startPos;

    // Use this for initialization
    void Start () {
        startPos = transform.position;
        data = ScriptableObject.CreateInstance<Player_Data>();
	}
	
	// Update is called once per frame
	void Update () {
        if (data.health <= 0)
        {
            transform.position = startPos;
            data.health = 100;
        }
            
    }

    public void TakeDamage(int f)
    {
        data.health -= f;
    }

    
}
