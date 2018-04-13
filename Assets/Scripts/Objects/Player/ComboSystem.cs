using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{

    private GLOBALS.PlayerContext pC;
    private List<GLOBALS.IState> combo1;
    private List<GLOBALS.IState> combo2;
    private List<GLOBALS.IState> combo3;
    
    public List<GLOBALS.IState> activeCombo;

    public float inputTimer = 0;
	// Use this for initialization
	void Start () {
		pC = new GLOBALS.PlayerContext();
	    combo1.Add(new LightPunchState());
	    combo1.Add(new MeduimPunchState());
	    combo1.Add(new HeavyPunchState());
        
        combo2.Add(new MeduimPunchState());
        combo2.Add(new LightPunchState());
        combo2.Add(new HeavyPunchState());

        combo3.Add(new HeavyPunchState());
        combo3.Add(new MeduimPunchState());
        combo3.Add(new LightPunchState());
    }
	
	// Update is called once per frame
	void Update ()
	{

	    if (Input.GetKey(KeyCode.Space))
	    {
	        inputTimer += Time.deltaTime;
	        if (Input.GetKeyUp(KeyCode.Space))
	        {
	            if (inputTimer >= 2)
	            {
	                activeCombo = combo2;                    
	            }
	            else if(inputTimer >= 4)
	            {
	                activeCombo = combo3;
	            }
	            else
	            {
	                activeCombo = combo1;
	            }
	        }
            SetCombo(activeCombo);
            pC.UpdateContext();
        }
        pC.Current.UpdateState(pC);

    }

    public void SetCombo(List<GLOBALS.IState> combo)
    {
         pC.ChangeState(combo[0]);
    }

}
