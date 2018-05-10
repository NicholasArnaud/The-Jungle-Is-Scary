﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public List<GameObject> ELONGATEDBOROS;
    public List<GameObject> HandleBoros;

    public void OnAnimationSwingStart()
    {
        ELONGATEDBOROS.ForEach(x => x.SetActive(true));
        HandleBoros.ForEach(x => x.SetActive(false));
    }

    public void OnAnimationSwingEnd()
    {
        ELONGATEDBOROS.ForEach(x => x.SetActive(false));
        HandleBoros.ForEach(x => x.SetActive(true));
    }
    public Animator anim;
    public float lightTimer;
    public float heavyTimer;
    public bool lightReady;
    public bool heavyReady;

    // Update is called once per frame
    void Update()
    {
        if (lightReady)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                anim.SetTrigger("Light Attack");
                lightReady = false;
            }
        }

        if (!lightReady)
        {
            lightTimer += Time.deltaTime;
        }

        if (lightTimer >= 1)
        {        
            lightReady = true;
            lightTimer = 0;
        }


        if (heavyReady)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                anim.SetTrigger("Heavy Attack");
                heavyReady = false;
            }
        }

        if (!heavyReady)
        {
            heavyTimer += Time.deltaTime;
        }

        if (heavyTimer >= 3)
        {
            heavyTimer = 0;
            heavyReady = true;
        }
    }
}