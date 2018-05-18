using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data")]
public class PlayerData : DataScriptable
{
    [SerializeField] private int _lifeGems;
    [SerializeField] private int _gemFragments; 

    public int LifeGems
    {
        get { return _lifeGems; }
        set
        {
            _lifeGems = value;
            DataChanged.Raise(this);
        }
    }

    public int GemFragments
    {
        get { return _gemFragments; }
        set
        {
            _gemFragments = value;
            DataChanged.Raise(this);
        }
    }
}