using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public GameEventArgs m_OnEnemyDamaged;

    public void OnCollisionEnter(Collision other)
    {
        m_OnEnemyDamaged.Raise(this, other.gameObject);
    }
}
