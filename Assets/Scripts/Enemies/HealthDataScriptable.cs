using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData",menuName = "Health")]
public class HealthDataBehaviour : ScriptableObject
{
    public int Health;
    public bool Alive;
}
