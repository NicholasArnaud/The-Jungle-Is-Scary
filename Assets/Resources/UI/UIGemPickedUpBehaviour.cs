using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIGemPickedUpBehaviour : MonoBehaviour
{
    public Item Gem;
    public int Count;
    public void OnGemPickedUp(UnityEngine.Object[] args)
    {
        var sender = args[0] as GameObject;
        var item = args[1] as Gem;
        if(item == null || Gem != item)
            return;
        Count++;
        GetComponent<Text>().text = Count.ToString();
    }
	
}
