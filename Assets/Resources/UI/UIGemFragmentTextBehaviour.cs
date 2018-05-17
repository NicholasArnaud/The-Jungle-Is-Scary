using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IPlayerDataChangeHandler
{
    void OnPlayerDataChanged(Object[] args);

}

public class UIGemFragmentTextBehaviour : MonoBehaviour,IPlayerDataChangeHandler
{
    public void OnPlayerDataChanged(Object[] args)
    {
        var sender = args[0] as Player_Data;
        if (sender == null)
            return;
        GetComponent<Text>().text = sender.GemFragments.ToString();
    }
}
