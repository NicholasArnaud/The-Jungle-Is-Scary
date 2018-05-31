using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayerBehaviour : MonoBehaviour
{
    public StringVariable playertag;
    public GameObject PlayerRef;
    public Transform checkpoint;
    public GameEvent PlayerRespawnEvent;

    void OnEnable()
    {
        PlayerRef = GameObject.FindGameObjectWithTag(playertag.Value);
    }
    public void TeleportPlayer()
    {
        if (((PlayerData) PlayerRef.GetComponent<DamageableBehaviour>().Data).LifeGems <= 0) return;
        PlayerRef.transform.position = checkpoint.position;
        PlayerRespawnEvent.Raise();
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
