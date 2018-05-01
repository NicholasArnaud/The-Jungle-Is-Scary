using UnityEngine;

public class SpawnEnemyBehaviour : MonoBehaviour
{
    public GameObject Enemy;
    private GameObject playerObject;
    private float distanceFromPlayer;
    public int SpawnStartDist;
    public bool SpawningEnabled;
    public int maxEnemies;
    int enemiesSpawned;
    public int coolDownTime;
    float spawnCooldown = 0;
    // Use this for initialization
    void Start()
    {
        FindPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(!SpawningEnabled)
        {
            distanceFromPlayer = Vector3.Distance(playerObject.transform.position, transform.position);
            if (distanceFromPlayer <= SpawnStartDist)
                SpawningEnabled = true;
            return;
        }
        spawnCooldown += Time.deltaTime;
        if (spawnCooldown == coolDownTime && enemiesSpawned <=maxEnemies)
        {
            spawnCooldown = 0;
            Instantiate(Enemy, transform);
            enemiesSpawned++;
        }

    }

    void FindPlayer()
    {
        GameObject[] gameobjects = FindObjectsOfType<GameObject>();

        for (int i = 0; i < gameobjects.Length; i++)
        {
            if (gameobjects[i].tag == "Player")
            {
                playerObject = gameobjects[i];
            }
        }
    }
}
