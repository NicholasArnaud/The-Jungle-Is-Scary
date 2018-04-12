using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehaviour : MonoBehaviour
{


    public Transform spawnTransform;
    public void OnCheckpointCrossed(Object[]args)
    {
        var sender = args[0] as GameObject;
        if(sender != gameObject)
            return;
        var other = args[1] as GameObject;
        other.GetComponent<Player_Behaviour>().checkpoint = spawnTransform;
        Debug.Log("Checkpoint Crossed");
    }

}
