using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behaviour : MonoBehaviour, IDamageable
{

    public enum ComboState
    {
        Light,
        Medium,
        Heavy,
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
        currentComboState = ComboState.Light;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            attacked = true;
            Attack();
            if (comboTimer > 0 && currentComboState == ComboState.Light)
            {
                currentComboState = ComboState.Medium;
                comboTimer = 5;
            }
            if (comboTimer > 0 && currentComboState == ComboState.Medium)
            {
                currentComboState = ComboState.Heavy;
                comboTimer = 5;
            }
        }
        if(comboTimer <= 0)
        {
            attacked = false;
            comboTimer = 100;
            currentComboState = ComboState.Light;
        }

        if (attacked)        
            comboTimer -= .001f;
        

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

    void Attack()
    {
        if (currentComboState == ComboState.Light)
            Debug.Log("Light");
        if (currentComboState == ComboState.Medium)
            Debug.Log("Medium");
        if (currentComboState == ComboState.Heavy)
            Debug.Log("Heavy");
    }
}
 