using System.Linq;
using UnityEngine;

public enum BuffState
{
    NONE = 0,
    PASSIVE = 1,
    AGGRESSIVE = 2,
    CHASING = 3,
    ATTACKING = 4,
    DEAD = 5,
}

public enum AttackState
{
    NONE = 0,
    QUICK = 1,
    HEAVY = 2,
}

public class BuffFlowerBehaviour : MonoBehaviour
{
    //General enemy variables located within scriptable
    public EnemyDataScriptable Data;
    private Animator _animatorController;
    
    //Specific values to Buff Flower
    [SerializeField]
    private bool _quickAttackReady = true;
    private float _timer;
    private float _secondTimer;
    private float _deathTimer = 4;
    private bool _inGround = true;
    [SerializeField]
    private BuffState _currentState = BuffState.NONE;
    [SerializeField]
    private AttackState _attackState;
    [Range(0, 4)]
    public float AttackCooldown;
    public float RiseTime;
    public Vector3 Smoothvelocity = Vector3.zero;
    private bool _activated;
    private float _distanceBetween;
    // Use this for initialization
    private void Start()
    {
        Data.PlayerGameObject = GameObject.FindWithTag("Player");
        _animatorController = GetComponent<Animator>();
        _timer = AttackCooldown;
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
            case BuffState.NONE:
                NoneStateHandler();
                break;
            case BuffState.PASSIVE:
                PassiveStateHandler();
                break;
            case BuffState.AGGRESSIVE:
                AggressiveStateHandler();
                break;
            case BuffState.CHASING:
                ChaseStateHandler(_distanceBetween);
                break;
            case BuffState.ATTACKING:
                AttackStateHandler(_distanceBetween);
                break;
            case BuffState.DEAD:
                DeathStateHandler();
                break;
        }
    }

    private void NoneStateHandler()
    {
        if (!Data.Alive)
        {
            ChangeState(BuffState.DEAD);
            return;
        }

        if (_distanceBetween <= 2)
        {
            _activated = true;
        }

        if (!_activated) return;

        if (_secondTimer <= 0)
            _animatorController.SetTrigger("Rising");
        _secondTimer += Time.deltaTime;
        _inGround = _secondTimer <= RiseTime;
        if (!_inGround)
            ChangeState(BuffState.PASSIVE);

    }

    private void PassiveStateHandler()
    {
        if (!Data.Alive)
        {
            ChangeState(BuffState.DEAD);
            return;
        }

        if (Data.FoundPlayer)
        {
            ChangeState(BuffState.AGGRESSIVE);
        }
    }

    private void AggressiveStateHandler()
    {
        transform.LookAt(Data.PlayerGameObject.transform
            .position); //if the enemy doesn't see the player it's not gonna stare at him

        if (!Data.Alive)
        {
            ChangeState(BuffState.DEAD);
            return;
        }

        //found player and not waiting to come out of ground
        if (Data.FoundPlayer)
        {
            ChangeState(BuffState.CHASING);
            return;
        }
        ChangeState(BuffState.PASSIVE);
    }

    private void ChaseStateHandler(float distance)
    {
        transform.LookAt(Data.PlayerGameObject.transform.position);

        if (!Data.Alive)
        {
            ChangeState(BuffState.DEAD);
            return;
        }
        //If the player enters the attack radius

        bool stillChasingPlayer = distance <= Data.DetectionRadius && distance >= Data.AttackRadius;
        if (stillChasingPlayer)
        {
            var tarPos = Data.PlayerGameObject.transform.position;
            tarPos.y = transform.position.y;
            transform.position = Vector3.SmoothDamp(transform.position, tarPos, ref Smoothvelocity, 10.0f);
            return;
        }
        if (distance <= Data.AttackRadius)
        {
            ChangeState(BuffState.ATTACKING);
            return;
        }
        ChangeState(BuffState.AGGRESSIVE);
    }

    private void AttackStateHandler(float distance)
    {
        if (_currentState == BuffState.ATTACKING)
        {
            //If the player is in the attack range
            if (!_quickAttackReady)
            {
                _timer -= Time.deltaTime;
                //If player is still in the attack range
                if (distance >= Data.AttackRadius)
                {
                    _quickAttackReady = true;
                    ChangeState(BuffState.CHASING);
                    return;
                }

                //If the player stays counttimer for his heavier attack
                if (_timer <= 0)
                {
                    _attackState = AttackState.HEAVY;
                    _timer = AttackCooldown;
                    RunAttackStateMachine(_attackState);
                }
            }
            //If the player just entered the attack range
            else
            {
                _attackState = AttackState.QUICK;
            }

            //Run attack
            if (_quickAttackReady && _attackState == AttackState.QUICK)
            {
                RunAttackStateMachine(_attackState);
                _quickAttackReady = false;
            }
        }
    }

    private void DeathStateHandler()
    {
        if (Data.Alive)
            return;
        //play Death animation
        _animatorController.SetBool("Alive", false);

        Destroy(gameObject, _deathTimer);
    }

    public BuffState CurrentState
    {
        get { return _currentState; }
        set
        {
            Debug.Log("Change state to " + value + " From " + _currentState);
            _currentState = value;
        }
    }

    private void ChangeState(BuffState state)
    {
        //ToDo:: Check for valid state transitions
        CurrentState = state;
    }

    #region No

    private void RunAttackStateMachine(AttackState attack)
    {
        //Quick is used when the player first enters the attack radius
        if (attack == AttackState.QUICK)
        {
            Debug.Log("Used a quick attack");
            //Run quick attack animation
        }
        //essentially a charge up attack
        else if (attack == AttackState.HEAVY)
        {
            Debug.Log("Used a heavy attack");
            //Run Heavy attack animation
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, .5f, 0, .5f);
        Gizmos.DrawSphere(transform.position, Data.DetectionRadius);
        Gizmos.color = new Color(1, .5f, 0, .5f);
        Gizmos.DrawSphere(transform.position, Data.AttackRadius);
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
    #endregion
}