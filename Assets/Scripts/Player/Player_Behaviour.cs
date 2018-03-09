using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behaviour : MonoBehaviour, IDamageable
{

    public enum ComboState
    {
        NONE,
        LIGHT,
        MEDIUM,
        HEAVY,
    }

    public Player_Data Data;
    Vector3 startPos;
    public GameEvent giveHealth;
    public ComboState currentComboState;
    public float comboTimer;
    bool attacked;

    // Use this for initialization
    void Start()
    {
        currentComboState = ComboState.NONE;
        startPos = transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        int i = 0;
        if (Input.GetMouseButtonDown(0))
        {
            attacked = true;
            if (comboTimer < 0)
           
                     
        }

        switch (currentComboState)
        {
            case ComboState.LIGHT:
                LightAttack();
                break;
            case ComboState.MEDIUM:
                MediumAtack();
                break;
            case ComboState.HEAVY:
                HeavyAttack();
                break;
        }

        if (comboTimer <= 0)
        {
            attacked = false;
            comboTimer = 5;
            currentComboState = ComboState.LIGHT;
        }

        if (attacked)
            comboTimer -= .01f;


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

    void LightAttack()
    {
        Debug.Log("Light");
    }
    void MediumAtack()
    {
        Debug.Log("Medium");
    }
    void HeavyAttack()
    {
        Debug.Log("Heavy");
    }
}
