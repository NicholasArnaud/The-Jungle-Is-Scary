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
        SWINGING,
    }
    public bool canSwing;
    public SwingState currentState;
    public float DetectionRadius;
    public List<Collider> objects;
    // Use this for initialization
    void Start()
    {
        currentState = SwingState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        var col = Physics.OverlapSphere(transform.position, 5);
        objects = col.ToList();
        if (objects.Contains(GameObject.FindGameObjectWithTag("Branch").GetComponent<Collider>()))
            canSwing = true;
        else
            canSwing = false;

        switch (Input.GetButtonDown("Fire1"))
        {
            case true:
                if (currentState == SwingState.SWINGING)
                    currentState = SwingState.IDLE;
                else if(canSwing)
                    currentState = SwingState.SWINGING;
                break;
        }
     //   if (currentState == SwingState.SWINGING)
            //Swing();

    }

    void Swing()
    {
        transform.LookAt(branch.transform.position);
        Vector3.MoveTowards(transform.position, branch.transform.position, 5);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, .5f, 0, .5f);
        Gizmos.DrawSphere(transform.position, DetectionRadius);
    }

}