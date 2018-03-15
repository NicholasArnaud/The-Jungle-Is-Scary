using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

// ReSharper disable UnusedMember.Local
// ReSharper disable once CheckNamespace
public enum MovementState
{
    NONE = 0,
    PASSIVE = 1,
    AGGRESSIVE = 2,
    CHASING = 3,
    ATTACKING = 4,
    DEAD = 5,
}

public class BuffFlowerBehaviour : MonoBehaviour
{
    //General enemy variables located within scriptable
    public EnemyDataScriptable Data;

    //Specific values to Buff Flower
    public float RiseTime;

    private const float DeathTimer = 4;
    private float _risingTimer;
    private float _distanceBetween;
    private bool _inGround = true;
    private bool _activated;
    
    private Animator _animatorController;
    private NavMeshAgent _nav;
    [SerializeField]
    private MovementState _currentState = MovementState.NONE;


    private void Start()
    {
        Data.PlayerGameObject = GameObject.FindWithTag("Player");
        _animatorController = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //must have checks per frame
        Data.Alive = (Data.Health >= 0);
        Data.FoundPlayer = EnableBehaviour(transform.position, Data.DetectionRadius);
        
        //anystate check for being dead 
        _distanceBetween = Vector3.Distance(Data.PlayerGameObject.transform.position, transform.position);
        _animatorController.SetFloat("Player Dist",_distanceBetween);

        switch (_currentState)
        {
            case MovementState.NONE:
                NoneStateHandler();
                break;
            case MovementState.PASSIVE:
                PassiveStateHandler();
                break;
            case MovementState.AGGRESSIVE:
                AggressiveStateHandler();
                break;
            case MovementState.CHASING:
                ChaseStateHandler();
                break;
            case MovementState.ATTACKING:
                AttackStateHandler();
                break;
            case MovementState.DEAD:
                DeathStateHandler();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public MovementState CurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }

    private void ChangeState(MovementState state)
    {
        CurrentState = state;
    }

    private void NoneStateHandler()
    {
        if (!Data.Alive)
        {
            ChangeState(MovementState.DEAD);
            return;
        }

        if (_distanceBetween <= 4)
        {
            _activated = true;
        }

        if (!_activated) return;

        if (_risingTimer <= 0)
            _animatorController.SetTrigger("Rising");
        _risingTimer += Time.deltaTime;
        _inGround = _risingTimer <= (RiseTime + 0.5f);
        if (!_inGround)
            ChangeState(MovementState.PASSIVE);

    }

    private void PassiveStateHandler()
    {
        if (!Data.Alive)
        {
            ChangeState(MovementState.DEAD);
            return;
        }

        if (Data.FoundPlayer)
        {
            ChangeState(MovementState.AGGRESSIVE);
            return;
        }
        _nav.SetDestination(transform.position);
    }

    private void AggressiveStateHandler()
    {
         var targetPoint = new Vector3(Data.PlayerGameObject.transform.position.x, transform.position.y, Data.PlayerGameObject.transform.position.z) - transform.position;
        var targetRotation = Quaternion.LookRotation(-targetPoint, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
        transform.LookAt(Data.PlayerGameObject.transform.position);

        if (!Data.Alive)
        {
            ChangeState(MovementState.DEAD);
            return;
        }
        
        if (_distanceBetween < 10)
        {
            ChangeState(MovementState.CHASING);
            return;
        }

        if (_distanceBetween > 15)
        {
            ChangeState(MovementState.PASSIVE);
            return;
        }
        _nav.speed = 1.5f;
        _nav.SetDestination(Data.PlayerGameObject.transform.position);
    }

    private void ChaseStateHandler()
    {
        transform.LookAt(Data.PlayerGameObject.transform.position);

        if (!Data.Alive)
        {
            ChangeState(MovementState.DEAD);
            return;
        }

        if (_distanceBetween > 10)
        {
            ChangeState(MovementState.AGGRESSIVE);
            return;
        }
         
        if (_distanceBetween <= Data.AttackRadius)
        {
            _nav.SetDestination(transform.position);
            ChangeState(MovementState.ATTACKING);
            return;
        }

        _nav.speed = 4.0f;
        _nav.SetDestination(Data.PlayerGameObject.transform.position);
    }

    private void AttackStateHandler()
    {
        if (!Data.Alive)
        {
            ChangeState(MovementState.DEAD);
            return;
        }

        if (_distanceBetween < Data.AttackRadius) return;
        ChangeState(MovementState.CHASING);
    }

    private void DeathStateHandler()
    {
        if (Data.Alive)
            return;
        _nav.enabled = false;
        _animatorController.SetBool("Alive", false);

        Destroy(gameObject, DeathTimer);
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, .5f, 0, .5f);
        Gizmos.DrawSphere(transform.position, Data.DetectionRadius);
        Gizmos.color = new Color(1, .5f, 0, .5f);
        Gizmos.DrawSphere(transform.position, Data.AttackRadius);
    }
#endif
    private bool EnableBehaviour(Vector3 center, float radius)
    {
        var playerfound = false;
        var hitColliders = Physics.OverlapSphere(center, radius);
        var collidedObjects = hitColliders.ToList();
        var playercollider = Data.PlayerGameObject.GetComponent<Collider>();
        if (collidedObjects.Contains(playercollider))
            playerfound = true;
        return playerfound;
    }
}