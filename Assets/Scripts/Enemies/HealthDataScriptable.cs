using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData",menuName = "EnemyData")]
public class HealthDataScriptable : ScriptableObject
{
    public int Health;
    public bool Alive;
}
