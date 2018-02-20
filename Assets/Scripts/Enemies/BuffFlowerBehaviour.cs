using System.Linq;
using UnityEngine;

public class BuffFlowerBehaviour : MonoBehaviour
{

    private GameObject playerGameObject;
    [Range(1, 20)]
    public float detectionRadius;
    [Range(0.2f, 5f)]
    public float attackRadius;
    private bool foundPlayer;
    private float timer;

    public enum BuffState
    {
        IDLE,
        CHASING,
        ATTACKING,
    }

    [SerializeField]
    private BuffState currentState;
    [Range(0, 4)]
    public float attackCooldown = 4;
    public Vector3 smoothvelocity = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        foundPlayer = false;
        playerGameObject = GameObject.FindWithTag("Player");
        timer = attackCooldown;
    }

    void Update()
    {
        foundPlayer = EnableBehaviour(transform.position, detectionRadius);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceBetween = Vector3.Distance(playerGameObject.transform.position, gameObject.transform.position);
        if (currentState == BuffState.IDLE)
        {
            if (foundPlayer)
                currentState = BuffState.CHASING;
        }
        else
        {
            transform.LookAt(playerGameObject.transform);
        }

        if (currentState == BuffState.ATTACKING)
        {
            timer -= Time.deltaTime;
            if (distanceBetween >= attackRadius)
            {
                currentState = BuffState.CHASING;
            }
            if (timer <= 0)
            {
                Debug.Log("Attack");
                timer = attackCooldown;
            }
        }

        if (currentState == BuffState.CHASING)
        {
            if (distanceBetween <= attackRadius)
            {
                currentState = BuffState.ATTACKING;
            }
            else
            {
                Debug.Log("Chasing");
                transform.position = Vector3.SmoothDamp(transform.position, playerGameObject.transform.position, ref smoothvelocity, 10.0f);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, .5f, 0, .5f);
        Gizmos.DrawSphere(transform.position, detectionRadius);
        Gizmos.color = new Color(1, .5f, 0, .5f);
        Gizmos.DrawSphere(transform.position, attackRadius);
    }

    bool EnableBehaviour(Vector3 center, float radius)
    {
        var playerfound = false;
        var hitColliders = Physics.OverlapSphere(center, radius);
        var collidedObjects = hitColliders.ToList();
        var playercollider = playerGameObject.GetComponent<Collider>();
        if (collidedObjects.Contains(playercollider))
            playerfound = true;
        return playerfound;
    }
}
