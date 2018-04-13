using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBALS {

    public interface IState
    {
        void OnEnter();
        void UpdateState(IContext context);
        void OnExit();
    }

    public interface IContext
    {
        void ChangeState(IState next);
    }

    [System.Serializable]
    public class PlayerContext : IContext
    {
        public IState Current;
        public List<GLOBALS.IState> combos;
        public void ChangeState(IState next)
        {
            Current.OnExit();
            Current = next;
            Current.OnEnter();
        }

        public void UpdateContext()
        {
            Current.UpdateState(this);
        }
    }
}
