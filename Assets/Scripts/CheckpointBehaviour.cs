using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehaviour : MonoBehaviour
{

    public GameObject wallPrefab;
    public Transform spawnTransform;
    public void Start()
    {
        wallPrefab.SetActive(false);
    }
    public void OnCheckpointCrossed(Object[]args)
    {
        
        var sender = args[0] as GameObject;
        if(sender != gameObject)
            return;
        var other = args[1] as GameObject;
        other.GetComponent<Player_Behaviour>().checkpoint = spawnTransform;
        CreateWall();
        Debug.Log("Checkpoint Crossed");
    }

    public void CreateWall()
    {
        wallPrefab.SetActive(true);
    }
    public void RemoveWall()
    {
        wallPrefab.SetActive(false);
    }
}
