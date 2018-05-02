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
    }

    public void ReduceHealth()
    {
        Data.Health--;
    }
}
