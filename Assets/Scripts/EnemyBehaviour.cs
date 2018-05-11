using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public GameEventArgs m_OnPlayerDamaged;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        m_OnPlayerDamaged.Raise(this, other.gameObject);
    }
}
