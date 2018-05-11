using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public GameEventArgs m_OnPlayerDamaged;

    public void OnCollisionEnter(Collision other)
    {
        m_OnPlayerDamaged.Raise(this, other.gameObject);
    }
}
