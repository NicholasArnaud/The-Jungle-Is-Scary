using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public Item item;
    public GameEventArgs ItemPickedUp;
    public PlayerData playerData;

    public void OnItemPickedUp(UnityEngine.Object[] args)
    {
        var fullGem = new Gem();
        var fragGem = new Gem();
        fullGem.value = 4;
        fragGem.value = 1;
        var sender = args[0] as GameObject;
        var other = args[1] as GameObject;
        
        if (sender == null ||  other == null || sender != gameObject)
            return;
        if (item.value == fullGem.value)
            playerData.LifeGems += 1;
        if (item.value == fragGem.value)
            playerData.GemFragments += 1;
        ItemPickedUp.Raise(gameObject, item);
    }
    
    public void Destory()
    {
        Destroy(gameObject);
    }
}
