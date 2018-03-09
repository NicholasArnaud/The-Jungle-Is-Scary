using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class Player_Data : ScriptableObject {

    public int hits = 4;
    public int lives;
    public int damage;
}
