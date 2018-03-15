using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : MonoBehaviour
{
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
        transform.Rotate(Vector3.up, value);
        timer += Time.deltaTime;
    }

}
