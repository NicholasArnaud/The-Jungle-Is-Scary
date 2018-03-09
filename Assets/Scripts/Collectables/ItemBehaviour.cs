using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public GameEvent ReceiveHealth;
    public GameEventArgs ReceiveGem;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        if(gameObject.name.Contains("Health"))
        {
            ReceiveHealth.Raise();
            Destroy(gameObject);
        }

        if (!gameObject.name.Contains("Gem")) return;
        ReceiveGem.Raise(gameObject);
        Destroy(gameObject);
    }
}
