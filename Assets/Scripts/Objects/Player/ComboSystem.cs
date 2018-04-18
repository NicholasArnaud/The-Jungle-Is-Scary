using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GLOBALS
{
    public class ComboSystem : MonoBehaviour
    {

        private GLOBALS.PlayerContext pC;
        private List<GLOBALS.IState> combo1;
        private List<GLOBALS.IState> combo2;
        private List<GLOBALS.IState> combo3;
        private int index = 0;

        public float inputTimer = 0;

        // Use this for initialization
        void Start()
        {
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
        void Update()
        {
            if (pC.combos == null && Input.GetKey(KeyCode.Space))
            {
                inputTimer += Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                SetCombo(inputTimer);
                inputTimer = 0;
            }

            if (pC.combos != null && Input.GetKeyDown(KeyCode.Space))
            {
                pC.ChangeState(pC.combos[index += 1]);
            }

            if (index >= pC.combos.Count)
            {
                pC.ChangeState(new IdleState());
                pC.combos = null;
                index = 0;
            }
        }

        public void SetCombo(float time)
        {
            if (time >= 2)
            {
                pC.combos = combo2;                
            }
            else if (time >= 4)
            {
                pC.combos = combo3;
            }
            else
            {
                pC.combos = combo1;
            }
            pC.ChangeState(pC.combos[0]);
        }



    }
}
