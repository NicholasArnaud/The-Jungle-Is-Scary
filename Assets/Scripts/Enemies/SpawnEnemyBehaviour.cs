using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnEnemyBehaviour : MonoBehaviour
{
    public List<GameObject> _enemyList;

    [Header("References")]
    public GameObject _playerGameObject;
    public GameObject EnemyPrefab;

    [Header("Events")]
    public GameEventArgs AllEnemiesDead;
    public GameEventArgsResponse WaitResponse;

    [Header("Configuration")]
    public Transform SpawnPoint;
    public int StartSpawnDist;
    public bool SpawningEnabled;
    public float _spawnCooldown;
    public int MaxEnemies;
    public int CooldownTime;

    [Header("Counters")]
    public float _distanceFromPlayer;
    public int deathstillrestart;
    public int _enemiesSpawned;
    public int numdeaths = 0;

    // Use this for initialization
    void Start()
    {
        _playerGameObject = GameObject.FindGameObjectWithTag("Player");
        _enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SpawningEnabled)
        {
            _distanceFromPlayer = Vector3.Distance(_playerGameObject.transform.position, transform.position);
            if (_distanceFromPlayer <= StartSpawnDist)
                SpawningEnabled = true;
            return;
        }

        _spawnCooldown += Time.deltaTime;
        if (!(_spawnCooldown >= CooldownTime) || _enemiesSpawned >= MaxEnemies)
            return;

        _spawnCooldown = 0;

        var spawnedEnemy = Instantiate(EnemyPrefab, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        _enemyList.Add(spawnedEnemy);
        deathstillrestart = _enemyList.Count;

        _enemiesSpawned++;

    }

    public void onEnemyDied(Object[] args)
    {
        var sender = args[0] as DataScriptable;//enemydata

        bool datainlist = false;
        //if we didn't find our data in the list...exit
        _enemyList.ForEach(e => datainlist |= sender == e.GetComponent<DamageableBehaviour>().Data);
        if (!datainlist)
            return;

        numdeaths++;

        deathstillrestart = MaxEnemies - numdeaths;
        if (deathstillrestart <= 0)
            AllEnemiesDead.Raise(gameObject);
        
    }

    public void WaitAndInvoke(int time)
    {
        StartCoroutine(WaitAndRestart(time));
    }

    IEnumerator WaitAndRestart(int num)
    {
        yield return new WaitForSeconds(num);
        WaitResponse.Invoke(null);
    }
}