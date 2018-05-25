using UnityEngine;

public abstract class DataScriptable : ScriptableObject
{
    public GameEventArgs DataChanged;
    [SerializeField] private int _health;
    [SerializeField] private bool _alive;
    [SerializeField] private int _damage;


    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            DataChanged.Raise(this);
        }
    }


    public bool Alive
    {
        get { return _alive; }
        set
        {
            _alive = value;
            DataChanged.Raise(this);
        }
    }

    public int Damage
    {
        get { return _damage; }
        set
        {
            _damage = value;
            DataChanged.Raise(this);
        }
    }
}