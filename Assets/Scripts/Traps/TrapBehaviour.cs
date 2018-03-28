using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour {

    public GameEvent HitPlayer;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            HitPlayer.Raise();
    }
}
