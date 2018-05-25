using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayerBehaviour : MonoBehaviour
{

    public Transform checkpoint;

    public void TeleportPlayer()
    {
        transform.position = checkpoint.position;
    }

    /// <summary>
    /// listen for when a checkpoint is crossed and update our current checkpoint
    /// </summary>
    /// <param name="args">first argument should be the checkpoint you crossed</param>

    public void OnCheckpointCrossed(Object[] args)
    {
        checkpoint = (args[0] as GameObject).transform;
    }

}
