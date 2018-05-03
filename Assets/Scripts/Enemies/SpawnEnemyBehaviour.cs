using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SpawnEnemyBehaviour : MonoBehaviour
{
    public GameObject Enemy;
    public ScriptableObject EnemyData;
    public GameEvent EnemiesDead;
    public int StartSpawnDist;
    public bool SpawningEnabled;
    public Transform SpawnPoint;
    public int MaxEnemies;
    public int CooldownTime;

    private List<GameObject> _enemyList;
    private GameObject _playerObject;
    private float _distanceFromPlayer;
    private int _enemiesSpawned;
    private float _spawnCooldown;
    private int _enemydeathcount;

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
            if (_distanceFromPlayer <= StartSpawnDist)
                SpawningEnabled = true;
            return;
        }
        EnemyCheck();
        _spawnCooldown += Time.deltaTime;
        if (!(_spawnCooldown >= CooldownTime) || _enemiesSpawned >= MaxEnemies) return;
        _spawnCooldown = 0;
        var spawnedEnemy = Instantiate(Enemy, SpawnPoint);
        spawnedEnemy.GetComponent<DataUpdater>().Data = Instantiate(EnemyData) as EnemyDataScriptable;
        _enemyList.Add(spawnedEnemy);
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
        if(_enemyList.Count <= 0)
            return;
        for (var i = _enemyList.Count - 1; i > -1; i--)
        {
            if (_enemyList[i] != null) continue;
            _enemyList.RemoveAt(i);
            _enemydeathcount++;
        }

        if (_enemydeathcount >= MaxEnemies)
        {
            //Raise Event
            EnemiesDead.Raise();
            Debug.Log("Eneies dead event Raised");
        }
    }
}