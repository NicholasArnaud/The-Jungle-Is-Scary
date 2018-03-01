using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrappleBehaviour : MonoBehaviour
{

    public GameObject branch;
    public enum SwingState
    {
        IDLE,
        THROWING,
        SWINGING,
    }
    public bool canThrow;
    public bool attached;
    public SwingState currentState;
    public float DetectionRadius;
    public List<Collider> objects;
    AnimationCurve ac;
    // Use this for initialization
    void Start()
    {
        currentState = SwingState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        var col = Physics.OverlapSphere(transform.position, DetectionRadius);
        objects = col.ToList();
        if (objects.Contains(GameObject.FindGameObjectWithTag("Branch").GetComponent<Collider>()))
            canThrow = true;
        else
        {
            canThrow = false;
            currentState = SwingState.IDLE;
        }


        switch (Input.GetButtonDown("Fire1"))
        {
            case true:
                if (currentState == SwingState.SWINGING)
                    currentState = SwingState.IDLE;
                else if (canThrow)
                    currentState = SwingState.SWINGING;
                else
                    currentState = SwingState.IDLE;
                break;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, .5f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);
    }

    void Swing()
    {

    }
    
    void OnTriggerEnter(Collider other)
    {

    }

}