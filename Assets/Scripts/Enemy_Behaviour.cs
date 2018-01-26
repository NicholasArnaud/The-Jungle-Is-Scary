using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour, IDamager, IDamageable
{

    GameObject target;
    Vector3 startPos;
    public Enemy_Data data;

    // Use this for initialization
    void Start()
    {
        data = ScriptableObject.CreateInstance<Enemy_Data>();
        startPos = transform.position;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, .5f, target.transform.position.z), 1 * Time.deltaTime);
        transform.LookAt(new Vector3(target.transform.position.x, .5f, target.transform.position.z));
        if (data.health <= 0)
        {
            transform.position = startPos;
            data.health = 20;
        }
            
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
            DoDamage(other.gameObject.GetComponent<Player_Behaviour>());

    }

    public void DoDamage(IDamageable defender)
    {
        defender.TakeDamage(data.damage);
    }

    public void TakeDamage(int f)
    {
        data.health -= f;
    }
}



