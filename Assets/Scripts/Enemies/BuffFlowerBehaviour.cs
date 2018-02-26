using System.Linq;
using TreeEditor;
using UnityEngine;

public class BuffFlowerBehaviour : MonoBehaviour
{
    //General enemy variables located here
    public EnemyDataScriptable Data;
    [SerializeField]
    private bool _quickAttackReady = true;
    private float _timer;
    private float _secondTimer;
    private float _deathTimer;
    private bool _inGround;
    [SerializeField]
    private BuffState _currentState;
    [SerializeField]
    private AttackState _attackState;
    [Range(0, 4)]
    public float AttackCooldown;
    public float RiseTime;
    

    public Vector3 Smoothvelocity = Vector3.zero;

    public enum BuffState
    {
        IDLE,
        CHASING,
        ATTACKING,
    }

    public enum AttackState
    {
        QUICK,
        HEAVY,
    }

    // Use this for initialization
    void Start()
    {
        _inGround = true;
        Data.FoundPlayer = false;
        Data.PlayerGameObject = GameObject.FindWithTag("Player");
        _timer = AttackCooldown;
    }

    void Update()
    {
        if (!Data.Alive)
        {
            //play Death animation
            Debug.Log("Running Death Animation");
            Data.Alive = false;
            _deathTimer += Time.deltaTime;
            if (_deathTimer >= 4)
                Destroy(gameObject);
            return;
        }
        Data.FoundPlayer = EnableBehaviour(transform.position, Data.DetectionRadius);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Data.Health <= 0)
        {
            Data.Alive = false;
            return;
        }

        float distanceBetween = Vector3.Distance(Data.PlayerGameObject.transform.position, gameObject.transform.position);
        if (_currentState == BuffState.IDLE)
        {
            if (_inGround == false)
            {
                //if the player enters the sight radius
                if (Data.FoundPlayer)
                    _currentState = BuffState.CHASING;
                else
                {
                    //Run above ground idle
                    Debug.Log("Running Above-Idle Animation");
                }
            }
            else
            {

                if (Data.FoundPlayer)
                {
                    //Run rising animation
                    Debug.Log("Running Rise Animation");

                    _secondTimer += Time.deltaTime;
                    if (_secondTimer >= RiseTime)
                        _inGround = false;
                }
                else
                {
                    //Run below ground idle animation
                    Debug.Log("Running Below-Idle Animation");
                }
            }

        }
        else
        {
            transform.LookAt(Data.PlayerGameObject.transform.position);
        }

        if (_currentState == BuffState.ATTACKING)
        {
            //If the player is in the attack range
            if (!_quickAttackReady)
            {
                _timer -= Time.deltaTime;
                //If player is still in the attack range
                if (distanceBetween >= Data.AttackRadius)
                {
                    _quickAttackReady = true;
                    _currentState = BuffState.CHASING;
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

        //While the enemy is chasing the player
        if (_currentState == BuffState.CHASING)
        {
            //If the player enters the attack radius
            if (distanceBetween <= Data.AttackRadius)
            {
                _currentState = BuffState.ATTACKING;
            }
            //Still chasing player
            else if (distanceBetween <= Data.DetectionRadius)
            {
                Debug.Log("Running Chasing Animation");
                Vector3 tarPos = Data.PlayerGameObject.transform.position;
                tarPos.y = transform.position.y;
                transform.position = Vector3.SmoothDamp(transform.position, tarPos, ref Smoothvelocity, 10.0f);
            }
            else
            {
                _currentState = BuffState.IDLE;
            }
        }
    }

    void RunAttackStateMachine(AttackState attack)
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

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, .5f, 0, .5f);
        Gizmos.DrawSphere(transform.position, Data.DetectionRadius);
        Gizmos.color = new Color(1, .5f, 0, .5f);
        Gizmos.DrawSphere(transform.position, Data.AttackRadius);
    }

    bool EnableBehaviour(Vector3 center, float radius)
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
