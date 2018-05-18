using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this is the checkpoint behaviour physics listener
/// </summary>
[RequireComponent(typeof (PhysicsTriggerListener))]
public class CheckpointBehaviour : MonoBehaviour
{
    public GameEventArgs OnChekpointCrossed;
    public GameObject wallPrefab;
    public Transform spawnTransform;

    public void OnCheckpointTriggerEnter(Object[]args)
    {
        var sender = args[0] as GameObject;
        if(sender != gameObject)
            return;
        OnChekpointCrossed.Raise(gameObject);
    }
}
