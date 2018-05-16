using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player_Behaviour : MonoBehaviour, IDamageable
{

    public Player_Data Data;
    public GameEvent giveHealth;
    public GameEvent playerDied;
    public Animation anim;
    private Transform startPos;
    public Transform checkpoint;
    public float immunityTimer = 1;
    public bool canTakeDamage;

    // Use this for initialization
    public void Start()
    {
        startPos = new GameObject().transform;
        checkpoint = startPos;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (canTakeDamage == false)
            immunityTimer -= .03f;

        if (immunityTimer <= 0)      
            canTakeDamage = true;
        
        if (Data.hp <= 0)
        {
            Data.lifeGems -= 1;
            Data.hp = 4;
            playerDied.Raise();
        }

        if (Data.lifeGems <= 0)
        {
            SceneManager.LoadScene("13.MainMenuScene");
            Data.lifeGems = 3;
        }
    }

    public void TakeDamage(int d)
    {
        if (canTakeDamage)
        {
            Data.hp -= d;
            immunityTimer = 1;
            canTakeDamage = false;
            Knockback();
        }    
    }

    public void Knockback()
    {
        anim.Play("DamageGround");
        transform.position = transform.position + Vector3.back * 50 * Time.deltaTime;
    }

    public void OnPlayerDied()
    {
        transform.position = new Vector3(checkpoint.position.x, 2.5f, checkpoint.position.z); ;
    }
}
