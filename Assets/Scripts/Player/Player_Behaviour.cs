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
    public int clickNum;
    // Use this for initialization
    void Start()
    {
        currentComboState = ComboState.NONE;
        startPos = transform.position;
        clickNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            clickNum += 1;
            attacked = true;
            switch (clickNum)
            {
                case 1:
                    currentComboState = ComboState.LIGHT;
                    comboTimer = 2;
                    break;
                case 2:
                    currentComboState = ComboState.MEDIUM;
                    comboTimer = 2;
                    break;
                case 3:
                    currentComboState = ComboState.HEAVY;
                    clickNum = 0;
                    break;
                default:
                    currentComboState = ComboState.NONE;
                    comboTimer = 2;
                    clickNum = 0;
                    break;
            }
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
            comboTimer = 2;
            clickNum = 0;
            currentComboState = ComboState.NONE;
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
