using System;
using UnityEngine;

namespace GLOBALS
{
    public static class PlayerInput
    {
        public static bool A { get; set; }
        public static bool B { get; set; }
        public static bool X { get; set; }
        public static bool Y { get; set; }
        public static bool LIGHTPUNCH
        {
            get
            {
                return Input.GetKeyDown(KeyCode.Z);
            }
        }
        public static bool MEDIUMPUNCH
        {
            get
            {
                return Input.GetKeyDown(KeyCode.X);
            }
        }
        public static bool HEAVYPUNCH
        {
            get
            {
                return Input.GetKeyDown(KeyCode.C);
            }
        }
    }

    [System.Serializable]
    public class IdleState : GLOBALS.IState
    {
        public float TTL = 3.0f;

        public void OnEnter(GLOBALS.IContext context)
        {
            throw new NotImplementedException();
        }

        public void OnExit(GLOBALS.IContext context)
        {
            throw new NotImplementedException();
        }

        public void UpdateState(GLOBALS.IContext context)
        {

        }
    }

    public class LightPunchState : GLOBALS.IState
    {
        public float TTL = 3.0f;
        public void OnEnter(GLOBALS.IContext context)
        {
            Debug.Log("ENTER LIGHT PUNCH");
        }

        public void OnExit(GLOBALS.IContext context)
        {
            Debug.Log("EXIT LIGHT PUNCH");
        }

        public void UpdateState(GLOBALS.IContext context)
        {
            if (TTL <= 0)
            {
                context.ChangeState(new IdleState());               
            }
                
            else
            {
                if (PlayerInput.MEDIUMPUNCH)
                {
                    context.ChangeState(new LightPunchState());
                }
            }
            TTL -= UnityEngine.Time.deltaTime;
        }
    }

    public class MeduimPunchState : GLOBALS.IState
    {
        public float TTL = 3.0f;
        public void OnEnter(GLOBALS.IContext context)
        {
            Debug.Log("ENTER MEDUIM PUNCH");
        }

        public void UpdateState(GLOBALS.IContext context)
        {
            if (TTL <= 0)
                context.ChangeState(new IdleState());
            else
            {
                if (PlayerInput.HEAVYPUNCH)
                {
                    context.ChangeState(new LightPunchState());
                }
            }
            TTL -= UnityEngine.Time.deltaTime;
        }

        public void OnExit(GLOBALS.IContext context)
        {
            Debug.Log("EXIT MEDIUM PUNCH");
        }
    }

    public class HeavyPunchState : GLOBALS.IState
    {
        public float TTL = 3.0f;

        public void OnEnter(GLOBALS.IContext context)
        {
            Debug.Log("ENTER HEAVY PUNCH");
        }

        public void UpdateState(GLOBALS.IContext context)
        {
            if (TTL <= 0)
                context.ChangeState(new IdleState());
            else
            {

                if (PlayerInput.LIGHTPUNCH)
                {
                    context.ChangeState(new LightPunchState());
                }
            }
            TTL -= Time.deltaTime;
        }

        public void OnExit(GLOBALS.IContext context)
        {
            Debug.Log("EXIT HEAVY PUNCH");
        }
    }
}

