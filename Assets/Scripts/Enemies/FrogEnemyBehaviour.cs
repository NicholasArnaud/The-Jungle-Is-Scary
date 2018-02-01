using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class FrogEnemyBehaviour : MonoBehaviour
{
    private GameObject playerGameObject;
    private Rigidbody rBody;
    private bool foundPlayer;
    private float timer;
    // Use this for initialization
    void Start()
    {
        foundPlayer = false;
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!foundPlayer)
        {
            if (InableBehaviour(transform.position, 15))
            {
                Debug.Log("FoundPlayer");
            }
        }
        else
        {
            transform.LookAt(playerGameObject.transform);
            timer += Time.fixedDeltaTime;
            if (timer >= 4)
            {
                timer = 0;
                rBody.velocity = Vector3.forward *3 + Vector3.up* 7;
            }
        }
        
    }
    bool InableBehaviour(Vector3 center, float radius)
    {
        playerGameObject = GameObject.FindWithTag("Player");
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        List<Collider> CollidedObjects = hitColliders.ToList();
        if (!CollidedObjects.Contains(playerGameObject.GetComponent<Collider>()))
            return false;
        foundPlayer = true;
        return true;
    }
}
