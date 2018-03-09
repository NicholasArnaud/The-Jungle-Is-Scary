using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Vector3Variable")]
public class Vector3Variable : ScriptableObject
{
    public Vector3 Value
    {
        get;set;
    }
}
