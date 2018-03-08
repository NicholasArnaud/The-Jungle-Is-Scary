using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public GameEvent ReceiveHealth;
    public GameEvent ReceiveGem;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        if(gameObject.name == "HealthPickUp")
        {
            ReceiveHealth.Raise();
            Destroy(gameObject);
        }
        if(gameObject.name[0] == 'G')
        {
            ReceiveGem.Raise();
            Destroy(gameObject);
        }
    }
}
