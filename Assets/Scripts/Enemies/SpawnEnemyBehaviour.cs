using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SpawnEnemyBehaviour : MonoBehaviour
{
    public GameObject Enemy;
    private GameObject _playerObject;
    public GameEvent EnemiesDead;
    private float _distanceFromPlayer;
    public int SpawnStartDist;
    public bool SpawningEnabled;
    public Transform SpawnPoint;
    public int MaxEnemies;
    private List<GameObject> _enemyList;
    private int _enemiesSpawned;
    public int CooldownTime;
    private float _spawnCooldown;
    // Use this for initialization
    void Start()
    {
        FindPlayer();
        Enemy.GetComponent<AICharacterControl>().SetTarget(_playerObject.transform);
        _enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SpawningEnabled)
        {
            _distanceFromPlayer = Vector3.Distance(_playerObject.transform.position, transform.position);
            if (_distanceFromPlayer <= SpawnStartDist)
                SpawningEnabled = true;
            return;
        }
        EnemyCheck();
        _spawnCooldown += Time.deltaTime;
        if (!(_spawnCooldown >= CooldownTime) || _enemiesSpawned > MaxEnemies) return;
        _spawnCooldown = 0;
        _enemyList.Add(Instantiate(Enemy, SpawnPoint));
        _enemiesSpawned++;

    }

    private void FindPlayer()
    {
        var gameobjects = FindObjectsOfType<GameObject>();

        foreach (var t in gameobjects)
        {
            if (t.tag == "Player")
            {
                _playerObject = t;
            }
        }
    }

    private void EnemyCheck()
    {
        var enemydeathcount = 0;
        foreach (var o in _enemyList)
        {
            if (o.GetComponent<EnemyDataScriptable>().Alive)
                return;
            enemydeathcount++;
        }

        if (enemydeathcount >= MaxEnemies)
        {
            //Raise Event
            EnemiesDead.Raise();
        }
    }
}