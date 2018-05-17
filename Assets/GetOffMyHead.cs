using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GetOffMyHead : MonoBehaviour
{
    
    void Start()
    {

    }
    void OnTriggerStay(Collider other)
    {
        var controller = other.GetComponent<CharacterController>();
        if (controller != null)
            controller.SimpleMove(Vector3.forward * 2);
    }

}
