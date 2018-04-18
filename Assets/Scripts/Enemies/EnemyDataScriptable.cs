using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData",menuName = "Data")]
public class EnemyDataScriptable : ScriptableObject
{
    [HideInInspector]
    public GameObject PlayerGameObject;
    public bool FoundPlayer;
    public int Health;
    public bool Alive;
    [Range(1, 30)]
    public float DetectionRadius;

    [Range(0.5f, 15f)]
    public float AttackRadius;
}
