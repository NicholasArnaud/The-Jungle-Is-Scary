using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{

    GameObject target;
    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, .5f, target.transform.position.z), 1 * Time.deltaTime);
        transform.LookAt(new Vector3(target.transform.position.x, .5f, target.transform.position.z));
    }
}
