using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EyeflowerBehaviour : MonoBehaviour
{

    public enum EyeState
    {
        IDLE,
        LOOKING,
        FIRING,
    }

    private bool _foundPlayer;
    private GameObject playerGameObject;
    private Rigidbody rBody;

    private EyeState _currentState;
    [Range(1, 4)]
    public float cooldown = 4;

    private float timer;

    [Range(0, 20)] public float DetectionRadius;

    void Start()
    {
        _foundPlayer = false;
        playerGameObject = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        _foundPlayer = EnableBehaviour(transform.position, DetectionRadius);
    }

    void FixedUpdate()
    {

        if (_currentState == EyeState.IDLE)
        {
            timer = cooldown;
            if (_foundPlayer)
                _currentState = EyeState.LOOKING;
        }

        if (_currentState == EyeState.FIRING)
        {
            Debug.Log("FIRE!!");
            _currentState = EyeState.LOOKING;
        }

        if (_currentState == EyeState.LOOKING)
        {
            timer -= Time.fixedDeltaTime;

            if (timer <= 0)
            {
                timer = cooldown;
                _currentState = EyeState.FIRING;
            }
            
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, .5f, 0, .5f);
        Gizmos.DrawSphere(transform.position, DetectionRadius);
    }

    bool EnableBehaviour(Vector3 center, float radius)
    {
        var hitColliders = Physics.OverlapSphere(center, radius);
        var collidedObjects = hitColliders.ToList();
        var playercollider = playerGameObject.GetComponent<Collider>();
        if (collidedObjects.Contains(playercollider))
            _foundPlayer = true;
        return _foundPlayer;
    }
}
