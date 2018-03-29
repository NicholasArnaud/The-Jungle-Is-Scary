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
    public GameEvent giveHealth;
    public GameEvent playerDied;

    public ComboState currentComboState;
    public float comboTimer;
    bool attacked;
    public int clickNum;

    private Transform startPos;
    public Transform checkpoint;
    public float immunityTimer = 1;
    public bool canTakeDamage;

    // Use this for initialization
    public void Start()
    {
        currentComboState = ComboState.NONE;
        startPos = transform;
        checkpoint = startPos;
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

        if (canTakeDamage == true)
            if (Input.GetKeyDown(KeyCode.X))
                TakeDamage(1);

        if (canTakeDamage == false)
            immunityTimer -= .03f;

        if (immunityTimer <= 0)
        {
            canTakeDamage = true;
        }

        if (Data.hp <= 0)
        {
            Data.lifeGems -= 1;
            Data.hp = 4;
            playerDied.Raise();
        }

        if (Data.lifeGems <= 0)
        {
            transform.position = startPos.position;
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
            transform.position = transform.position + Vector3.back * 50 * Time.deltaTime;
        }    
}

    public void OnPlayerDied()
    {
        transform.position = new Vector3(checkpoint.position.x, 2.5f, checkpoint.position.z); ;
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
