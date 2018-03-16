using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class Player_Data : ScriptableObject {

    public int hp = 12;
    public int lives;
    public bool alive;
    public int damage;
}
