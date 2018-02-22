using UnityEngine;

public class BuffFlowerHealthBehaviour : MonoBehaviour
{
    public HealthDataScriptable HealthScriptableData;

    private int Health;
    private bool Alive;
	// Use this for initialization
	void Start ()
    {
        Health = HealthScriptableData.Health;
        Alive = HealthScriptableData.Alive;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Health <= 0)
            Alive = false;
	}
}
