using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Animation anim;
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
                anim.Play("HeavyAttack");
                anim.Rewind("HeavyAttack");
                lightReady = false;
            }
        }

        if (!lightReady)
        {
            lightTimer += Time.deltaTime;
        }

        if (lightTimer >= anim["HeavyAttack"].length)
        {
            
            //lightAttack.Rewind();         
            lightReady = true;
            lightTimer = 0;
        }


        if (heavyReady)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                anim.Play("HeavyAttack");
                anim.Rewind("HeavyAttack");
                heavyReady = false;
            }
        }

        if (!heavyReady)
        {
            heavyTimer += Time.deltaTime;
        }

        if (heavyTimer >= anim["HeavyAttack"].length + 2)
        {
            heavyTimer = 0;
            heavyReady = true;
        }
    }
}
