using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWall : MonoBehaviour {

    public float timer = 0;
    public AnimationCurve ac;
    public float duration = 5;
    public float value;
    public bool flip = false;
    void Start()
    {
        timer = 0;
    }
    void Update()
    {

        if (timer >= duration)
        {
            timer = 0;
            flip = !flip;
        }

        value = ac.Evaluate(timer / duration);
        if (flip)
            value *= -1;
        transform.position = new Vector3(value, transform.position.y, transform.position.z); 
        timer += Time.deltaTime;
    }

}
