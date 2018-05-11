using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class WoodCritterBehaviour : MonoBehaviour
{
    [SerializeField]
    private MovementState _currentState = MovementState.NONE;

    public int DistToAggressive;
    public int DistToChase;
    public float walkSpeed;
    public float runSpeed;
    public EnemyDataScriptable Data;
    private const float DeathTimer = 4;

    public float _distanceBetween;
    private Animator _animatorController;
    private NavMeshAgent _nav;
    public List<BoxCollider> hitBoxes;

    public void OnWoodCritterStartAttack()
    {
        hitBoxes.ForEach(hb => hb.enabled = true);
    }

    public void WoodCritterAttack(string value)
    {
        switch (value)
        {
            case "start":
                OnWoodCritterStartAttack();
                break;
            case "end":
                OnWoodCritterEndAttack();
                break;
            default:
                Debug.Log("nope");
                break;
        }
    }
    public void OnWoodCritterEndAttack()
    {
        hitBoxes.ForEach(hb => hb.enabled = false);
    }
    // Use this for initialization
    void Start()
    {
        OnWoodCritterEndAttack();
        Data.PlayerGameObject = GameObject.FindWithTag("Player");
        _animatorController = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();
        CurrentState = MovementState.NONE;
        Data.AttackRadius = _nav.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        //must have checks per frame
        Data.Alive = (Data.Health > 0);
        Data.FoundPlayer = EnableBehaviour(transform.position, Data.DetectionRadius);
        //anystate check for being dead 
        _distanceBetween = Vector3.Distance(Data.PlayerGameObject.transform.position, transform.position);

        if (_distanceBetween <= Data.AttackRadius + 1)
        {
            _animatorController.SetBool("Idle", true);
            _animatorController.SetBool("Walk", true);
            _animatorController.SetBool("Chase", true);
            _animatorController.SetTrigger("Attack");
        }
            
        else if (_distanceBetween <= DistToChase)
        {
            _animatorController.SetBool("Idle", true);
            _animatorController.SetBool("Walk", true);
            _animatorController.SetBool("Chase", true);
        }

        else if (_distanceBetween <= DistToAggressive)
        {
            _animatorController.SetBool("Idle", true);
            _animatorController.SetBool("Walk", true);
            _animatorController.SetBool("Chase", false);
        }   
        else
        {
            _animatorController.SetBool("Idle", true);
            _animatorController.SetBool("Walk", false);
            _animatorController.SetBool("Chase", false);
        }
            

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
        if (!Data.Alive)
        {
            ChangeState(MovementState.DEAD);
            return;
        }

        if (_distanceBetween <= DistToChase)
        {
            ChangeState(MovementState.CHASING);
            return;
        }

        if (_distanceBetween > DistToAggressive)
        {
            ChangeState(MovementState.PASSIVE);
            return;
        }
        _nav.speed = walkSpeed;
        _nav.SetDestination(Data.PlayerGameObject.transform.position);
    }
    
    private void ChaseStateHandler()
    {
        if (!Data.Alive)
        {
            ChangeState(MovementState.DEAD);
            return;
        }

        if (_distanceBetween > DistToChase)
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

        _nav.speed = runSpeed;
        _nav.SetDestination(Data.PlayerGameObject.transform.position);
    }

    private void AttackStateHandler()
    {
        if (!Data.Alive)
        {
            ChangeState(MovementState.DEAD);
            return;
        }

        _nav.SetDestination(transform.position);
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

    public MovementState CurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }



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
