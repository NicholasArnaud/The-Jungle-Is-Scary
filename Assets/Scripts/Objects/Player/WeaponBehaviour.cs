using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public GameEventArgs m_OnEnemyDamaged;

    public void OnTriggerEnter(Collider other)
    {
        m_OnEnemyDamaged.Raise(this, other.gameObject);
    }
}
