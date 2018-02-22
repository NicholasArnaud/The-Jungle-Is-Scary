using System.Linq;
using UnityEngine;

public class BuffFlowerBehaviour : MonoBehaviour
{

    private GameObject _playerGameObject;
    [Range(1, 20)]
    public float DetectionRadius;
    [Range(0.2f, 5f)]
    public float AttackRadius;
    private bool _foundPlayer;
    private float _timer;

    public enum BuffState
    {
        IDLE,
        CHASING,
        ATTACKING,
    }

    [SerializeField]
    private BuffState _currentState;
    [Range(0, 4)]
    public float AttackCooldown = 4;
    public Vector3 Smoothvelocity = Vector3.zero;
    [SerializeField]
    private bool _attackedOnInstant = true;

    // Use this for initialization
    void Start()
    {
        _foundPlayer = false;
        _playerGameObject = GameObject.FindWithTag("Player");
        _timer = AttackCooldown;
    }

    void Update()
    {
        _foundPlayer = EnableBehaviour(transform.position, DetectionRadius);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceBetween = Vector3.Distance(_playerGameObject.transform.position, gameObject.transform.position);

        if (_currentState == BuffState.IDLE)
        {
            if (_foundPlayer)
                _currentState = BuffState.CHASING;
        }
        else
        {
            transform.LookAt(_playerGameObject.transform);
        }

        if (_currentState == BuffState.ATTACKING)
        {
            if (_attackedOnInstant == false)
            {
                _timer -= Time.deltaTime;
                if (distanceBetween >= AttackRadius)
                {
                    _attackedOnInstant = true;
                    _currentState = BuffState.CHASING;
                }
                if (_timer <= 0)
                {
                    Debug.Log("Attack");
                    _timer = AttackCooldown;
                }
            }
            else
            {
                Debug.Log("Attack");
                _attackedOnInstant = false;
            }

        }

        if (_currentState == BuffState.CHASING)
        {
            if (distanceBetween <= AttackRadius)
            {
                _currentState = BuffState.ATTACKING;
            }
            else
            {
                Debug.Log("Chasing");
                transform.position = Vector3.SmoothDamp(transform.position, _playerGameObject.transform.position, ref Smoothvelocity, 10.0f);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, .5f, 0, .5f);
        Gizmos.DrawSphere(transform.position, DetectionRadius);
        Gizmos.color = new Color(1, .5f, 0, .5f);
        Gizmos.DrawSphere(transform.position, AttackRadius);
    }

    bool EnableBehaviour(Vector3 center, float radius)
    {
        var playerfound = false;
        var hitColliders = Physics.OverlapSphere(center, radius);
        var collidedObjects = hitColliders.ToList();
        var playercollider = _playerGameObject.GetComponent<Collider>();
        if (collidedObjects.Contains(playercollider))
            playerfound = true;
        return playerfound;
    }
}
