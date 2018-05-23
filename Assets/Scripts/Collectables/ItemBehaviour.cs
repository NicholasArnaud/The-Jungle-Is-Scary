﻿using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public string ItemIdentifier;
    public Item item;
    public GameEventArgs ItemPickedUp;

    public void OnItemPickedUp(UnityEngine.Object[] args)
    {
        var sender = args[0] as GameObject;
        var other = args[1] as GameObject;
        
        if (sender == null ||  other == null || sender != gameObject)
            return;
        
        ItemPickedUp.Raise(gameObject, item);
    }
    
    public void Destory()
    {
        Destroy(gameObject);
    }
}
