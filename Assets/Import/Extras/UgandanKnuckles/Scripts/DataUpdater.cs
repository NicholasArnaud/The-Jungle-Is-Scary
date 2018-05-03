using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUpdater : MonoBehaviour
{
    public EnemyDataScriptable Data;

    void Start()
    {
        Data.Alive = true;
        Data.Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Data.Alive = (Data.Health > 0);
        if(!Data.Alive)
            Destroy(gameObject);
    }

    public void ReduceHealth(Object[] args)
    {
        var obj = args[1] as GameObject;
        if(obj == this.gameObject)
            Data.Health--;
    }
}
