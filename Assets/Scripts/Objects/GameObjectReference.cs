using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptables/GameObjectReference")]
public class GameObjectReference : ScriptableObject
{
    [HideInInspector]
    public GameObject Value;   
}
