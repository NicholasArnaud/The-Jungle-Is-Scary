using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class THROWTHISAWAYBEHAVIOUR : MonoBehaviour {
    public TimerObject To;
    public UnityEngine.Events.UnityEvent Response;
	// Use this for initialization
 
    public void OnTimerEnded()
    {
       // To.Execute(this, Response);
    }
	
}
