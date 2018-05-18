using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class SpawnEnemyBehaviour : MonoBehaviour
{
    public GameObject Enemy;
    public GameEvent EnemiesDead;
    public EnemyDataScriptable EnemyData;
    public GameEventArgs ENEMIESDEADARGS;
    public int StartSpawnDist;
    public bool SpawningEnabled;
    public Transform SpawnPoint;
    public int MaxEnemies;
    public int CooldownTime;

    private List<GameObject> _enemyList;
    private GameObject _playerGameObject;
    private float _distanceFromPlayer;
    private int _enemiesSpawned;
    private float _spawnCooldown;
    private int _enemydeathcount;
    public int deathstillrestart;

    // Use this for initialization
    void Start()
    {

        FindPlayer();
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
        //EnemyCheck();
        _spawnCooldown += Time.deltaTime;
        if (!(_spawnCooldown >= CooldownTime) || _enemiesSpawned >= MaxEnemies) return;
        _spawnCooldown = 0;
        var spawnedEnemy = Instantiate(Enemy, SpawnPoint.transform.position,SpawnPoint.transform.rotation);
        var newEnemyData = Instantiate(EnemyData);
        spawnedEnemy.GetComponent<DamageableEnemyBehaviour>().EnemyData = newEnemyData;
        spawnedEnemy.GetComponent<WoodCritterBehaviour>().Data = newEnemyData;
        foreach (var gameEventArgsListener in spawnedEnemy.GetComponents<GameEventArgsListener>())
        {
            gameEventArgsListener.Sender = newEnemyData;
        }
        _enemyList.Add(spawnedEnemy);
        deathstillrestart = _enemyList.Count;
        _enemiesSpawned++;

    }

    public int numdeaths = 0;
    public void onEnemyDied(Object[] args)
    {
        var sender = args[0] as GameObject;
        if (!_enemyList.Contains(sender))
            return;

        numdeaths++;

        deathstillrestart = MaxEnemies - numdeaths;
        if (deathstillrestart <= 0)
        {
            EnemiesDead.Raise();
            ENEMIESDEADARGS.Raise(gameObject);
        }


    }

    private void EnemyCheck()
    {
        if (_enemyList.Count <= 0)
            return;
        for (var i = _enemyList.Count - 1; i > -1; i--)
        {
            if (_enemyList[i] != null) continue;
            _enemyList.RemoveAt(i);
            _enemydeathcount++;
        }

        if (_enemydeathcount < MaxEnemies) return;
        //Raise Event
        EnemiesDead.Raise();
        ENEMIESDEADARGS.Raise(gameObject);
    }

    private void FindPlayer()
    {
        _playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    public GameEventArgsResponse WaitResponse;
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