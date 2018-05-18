using System;
using System.Collections;
using System.Collections.Generic;
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
    public TimerObject ParticleTimer;
    public float walkSpeed;
    public float runSpeed;
    public List<ParticleSystem> AOEparticles;
    public List<BoxCollider> hitBoxes;
    public SphereCollider AOEAttack;
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
        Data = Instantiate(Data);
        Data.name += GetInstanceID().ToString();
        GLOBALGAMEMANAGER.SetSendersToInstantiatedClone(data: Data, go: gameObject);
        Data.PlayerGameObject = GameObject.FindWithTag("Player");

        _animatorController = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();
        CurrentState = MovementState.NONE;
    }


    public void OnSunnyStartAttack()
    {

        if (Data.Health < 1)
            return;
        _nav.SetDestination(transform.position);
        _animatorController.applyRootMotion = false;
        hitBoxes.ForEach(hb => hb.enabled = true);
    }
    public void OnSunnyEndAttack()
    {
        if (Data.Health < 1)
            return;
            _nav.SetDestination(Data.PlayerGameObject.transform.position);

        _animatorController.applyRootMotion = true;
        hitBoxes.ForEach(hb => hb.enabled = false);
    }
    public void SunnyAttack(string value)
    {
        switch (value)
        {
            case "start":
                OnSunnyStartAttack();
                break;
            case "end":
                OnSunnyEndAttack();
                break;
            default:
                Debug.Log("nope");
                break;
        }
    }
    private void Update()
    {
        if (Data.Health < 1)
        {
            ChangeState(MovementState.DEAD);
        }
        //must have checks per frame
        Data.Alive = (Data.Health > 0);
        Data.FoundPlayer = EnableBehaviour(transform.position, Data.DetectionRadius);

        //anystate check for being dead 
        _distanceBetween = Vector3.Distance(Data.PlayerGameObject.transform.position, transform.position);
        _animatorController.SetFloat("Player Dist", _distanceBetween);

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

        _nav.speed = 0;
        _nav.SetDestination(transform.position);
    }

    private void AggressiveStateHandler()
    {
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
        _nav.SetDestination(Data.PlayerGameObject.transform.position);
    }

    private void ChaseStateHandler()
    {
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

    //Animation/Particle controller
    public void PlayAnimation()
    {
        var pO = GetComponentsInChildren<ParticleSystem>();
        foreach (var system in pO)
        {
            system.Play();
            ParticleTimer.Execute(this, system.Stop);
        }

    }

    public void VariableNavSpeedOnAnimation(float value)
    {
        _nav.speed = value;
    }

    public void SetNavSpeedWhenNotAttacking()
    {
        if (CurrentState.Equals(MovementState.CHASING))
        {
            _nav.speed = runSpeed;
        }

        if (CurrentState.Equals(MovementState.AGGRESSIVE))
        {
            _nav.speed = walkSpeed;
        }

        if (CurrentState.Equals(MovementState.PASSIVE))
        {
            _nav.speed = 0;
        }
    }

    public void AOEAttackEffect()
    {
        AOEAttack.enabled = true;
        _nav.speed = 0;
        AOEparticles.ForEach(x => x.Play());
        StartCoroutine(AOELastingEffect());
    }

    public IEnumerator AOELastingEffect()
    {
        _nav.speed = 0;
        yield return new WaitForSeconds(1);
        AOEAttack.enabled = false;
        SetNavSpeedWhenNotAttacking();
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
}