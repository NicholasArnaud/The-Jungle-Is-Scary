using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWall : MonoBehaviour
{

    public float timer = 0;
    public AnimationCurve ac;
    public float duration = 5;
    public float value;
    float posx;
    void Start()
    {
        timer = 0;
        posx = transform.position.x;
    }
     
    void Update()
    {
        if (timer >= duration)
        {
            timer = 0;
        }
    
        value = ac.Evaluate(timer / duration);
        transform.position = new Vector3(posx + value, transform.position.y, transform.position.z);
        timer += Time.deltaTime;
    }

}
