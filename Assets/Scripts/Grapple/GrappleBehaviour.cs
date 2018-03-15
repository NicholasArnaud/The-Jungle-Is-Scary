using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrappleBehaviour : MonoBehaviour
{

    public bool canThrow;
    public bool attached;
    public float DetectionRadius;
    public List<Collider> objects;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var col = Physics.OverlapSphere(transform.position, DetectionRadius);
        objects = col.ToList();
        if (objects.Contains(GameObject.FindGameObjectWithTag("Branch").GetComponent<Collider>()))
            canThrow = true;          
        else
            canThrow = false;
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (attached)
            {
                FindObjectOfType<BranchSwingTestBehaviour>().AttachAndSwing(rb);
                rb.AddForce(0, 0, 200);
                attached = false;
            }
            else if (canThrow)
            {
                FindObjectOfType<BranchSwingTestBehaviour>().AttachAndSwing(rb);
                attached = true;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, .5f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);
    }

}