using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    private PlayerContext pC;

    public List<IState> combo1;

	// Use this for initialization
	void Start () {
		pC = new PlayerContext();
        combo1.Add(new IdleState());
	    combo1.Add(new LightPunchState());
	    combo1.Add(new MeduimPunchState());
	    combo1.Add(new HeavyPunchState());
	    pC.Current = combo1[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
