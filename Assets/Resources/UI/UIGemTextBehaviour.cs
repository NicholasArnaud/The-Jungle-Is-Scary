using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGemTextBehaviour : MonoBehaviour ,IPlayerDataChangeHandler
{
    public void OnPlayerDataChanged(Object[] args)
    {
        var sender = args[0] as Player_Data;
        if (sender == null)
            return;
        GetComponent<Text>().text = sender.LifeGems.ToString();
    }
}
