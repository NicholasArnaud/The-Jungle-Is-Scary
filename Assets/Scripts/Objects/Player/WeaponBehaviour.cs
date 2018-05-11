using UnityEditor.Graphs;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public GameEventArgs m_OnEnemyDamaged;
    public float hitTimer = 2;
    public bool hitEnemy = false;
    public void OnTriggerEnter(Collider other)
    {
        if (hitEnemy == false && other.tag == "Enemy")
        {
            hitEnemy = true;
            m_OnEnemyDamaged.Raise(this, other.gameObject);
        }
            
    }

    public void Update()
    {
        if (hitEnemy == true)
        {
            hitTimer -= Time.deltaTime;
        }
        if (hitTimer <= 0)
            hitEnemy = false;

    } 
}
