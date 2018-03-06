using UnityEditor.Animations;
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

    //Animations that will come with all enemies
    public Animation IdleAnimation;
    public Animation ChasingAnimation;
    public Animation DeathAnimation;
    public Animation CurrentAnimation;
}
