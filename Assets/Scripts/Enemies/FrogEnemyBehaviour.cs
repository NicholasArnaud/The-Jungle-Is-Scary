using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class FrogEnemyBehaviour : MonoBehaviour
{
    private GameObject playerGameObject;
    private Rigidbody rBody;
    [Range(1, 20)]
    public float detectionRadius;
    private bool foundPlayer;
    private float timer;

    public enum FrogState
    {
        IDLE,
        LOOKING,
        JUMPING,
    }

    [SerializeField]
    private FrogState currentState;
    private float jumpHeight = 4;
    private float jumpDistance = 6;
    [Range(0, 4)]
    public float cooldown = 4;

    // Use this for initialization
    void Start()
    {
        foundPlayer = false;
        rBody = GetComponent<Rigidbody>();
        playerGameObject = GameObject.FindWithTag("Player");
        timer = cooldown;
    }

    void Update()
    {
        foundPlayer = EnableBehaviour(transform.position, detectionRadius);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var dir = (playerGameObject.transform.position - transform.position).normalized;

        if (currentState == FrogState.IDLE)
        {
            timer = cooldown;
            if (foundPlayer)
                currentState = FrogState.LOOKING;
        }

        if (currentState == FrogState.JUMPING)
        {
            var jumpvector = Vector3.forward * jumpDistance + Vector3.up * jumpHeight;
            Debug.DrawLine(transform.position, dir * 5f);
            rBody.AddRelativeForce(jumpvector, ForceMode.Impulse);
            currentState = FrogState.LOOKING;
        }

        if (currentState == FrogState.LOOKING)
        {
            timer -= Time.fixedDeltaTime;
            if (timer <= 3)
            {
                rBody.rotation = Quaternion.Slerp(rBody.rotation, Quaternion.LookRotation(dir), timer);
                rBody.isKinematic = true;

            }
            if (timer <= 0)
            {
                currentState = FrogState.JUMPING;
                rBody.isKinematic = false;
                timer = cooldown;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, .5f, 0, .5f);
        Gizmos.DrawSphere(transform.position, detectionRadius);
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
