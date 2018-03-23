using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehaviour : MonoBehaviour, IContainer
{
    public Player_Data PlayerData;
    public List<Item> items = new List<Item>();
    public Item currentItem;
    public int currentItemIndex;
    void Start()
    {
        if (items.Count > 0)
            currentItem = items[currentItemIndex];
    }

    public void Update()
    {

        var scroll = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log(scroll);
        CycleItems(scroll);
        

    }
    public void AddToInventory(Item gem)
    {
        items.Add(gem);
        
    }

    public void RemoveFromInventory(Item gem)
    {
        throw new System.NotImplementedException();
    }

    public void CycleItems(float scroll)
    {
        if (scroll == 0.0 || items.Count <= 0)
            return;
        var nextindex = (scroll > 0.0f) ? currentItemIndex += 1 : currentItemIndex -= 1;
        var lastindex = items.Count - 1;
        if (nextindex > lastindex)
            nextindex = lastindex;
        if (nextindex < 0)
            nextindex = 0;

        currentItemIndex = nextindex;
        currentItem = items[currentItemIndex];


    }
}
