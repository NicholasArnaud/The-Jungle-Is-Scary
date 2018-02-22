using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBehaviour : MonoBehaviour
{
    public enum Enemy
    {
        SUNNY = 1,
        FROGGY = 2,
        EYE = 3,
    }

    public int Health;
    private bool _alive;

    // Use this for initialization
    void Start()
    {
        CheckEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
            _alive = false;
    }

    public void CheckEnemy()
    {
        
        if (gameObject.name == "Sunny")
            SetValues(Enemy.SUNNY);
        if (gameObject.name == "Beamie")
            SetValues(Enemy.EYE);
        if (gameObject.name == "Frog")
            SetValues(Enemy.FROGGY);
    }

    public void SetValues(Enemy enemy)
    {
        _alive = true;
        switch (enemy)
        {
            case Enemy.SUNNY:
                Health = 6;
                break;
            case Enemy.FROGGY:
                Health = 4;
                break;
            case Enemy.EYE:
                Health = 2;
                break;
            default:
                throw new ArgumentOutOfRangeException("enemy", enemy, null);
        }
    }
}
