namespace GLOBALS
{
    public class GLOBALS
    {

        public interface IState
        {
            void OnEnter(IContext context);
            void UpdateState(IContext context);
            void OnExit(IContext context);
        }

        public interface IContext
        {
            void ChangeState(IState next);
        }



        [System.Serializable]
        public class PlayerContext : IContext
        {
            public IState Current;
            public void ChangeState(IState next)
            {
                Current.OnExit(this);
                Current = next;
                Current.OnEnter(this);
            }

            public void UpdateContext()
            {
                Current.UpdateState(this);
            }
        }

        [System.Serializable]
        public class GameContext : IContext
        {
            public IState Current;
            public void ChangeState(IState next)
            {
                Current.OnExit(this);
                Current = next;
                Current.OnEnter(this);
            }

            public void UpdateContext()
            {
                Current.UpdateState(this);
            }
        }
    }
}