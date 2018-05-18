using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class Player_Data : ScriptableObject
{
    public GameEventArgs PlayerDataChanged;
    [SerializeField] private int _hp;
    [SerializeField] private int _lifeGems;
    [SerializeField] private bool _alive;
    [SerializeField] private int _damage;
    [SerializeField] private int _gemFragments;

    public int Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            PlayerDataChanged.Raise(this);
        }
    }


    public int LifeGems
    {
        get { return _lifeGems; }
        set
        {
            _lifeGems = value;
            PlayerDataChanged.Raise(this);
        }
    }

    public int GemFragments
    {
        get { return _gemFragments; }
        set
        {
            _gemFragments = value;
            PlayerDataChanged.Raise(this);
        }
    }

    public bool Alive
    {
        get { return _alive; }
        set
        {
            _alive = value;
            PlayerDataChanged.Raise(this);
        }
    }

    public int Damage
    {
        get { return _damage; }
        set
        {
            _damage = value;
            PlayerDataChanged.Raise(this);
        }
    }
}