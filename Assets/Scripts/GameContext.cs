using UnityEngine;

[CreateAssetMenu(menuName = "GameContext")]
public class GameContext : ScriptableObject, GLOBALS.GLOBALS.IContext
{
    private GLOBALS.GLOBALS.IState _current;
    public string CurrentStateName;

    public static bool PauseButtonClicked { get; set; }

    public static bool GameStarted { get; set; }
    private void OnEnable()
    {
        _current = new GameStartState();
        _current.OnEnter();
    }
    /// <summary>
    /// this should not be called by anyone but the states that funnel through here
    /// </summary>
    /// <param name="next"></param>
    public void ChangeState(GLOBALS.GLOBALS.IState next)
    {
        _current.OnExit();
        _current = next;
        CurrentStateName = _current.ToString();
        _current.OnEnter();
    }

    public void UpdateContext()
    {
        _current.UpdateState(this);
    }

    public void SetGameStarted()
    {
        GameStarted = !GameStarted;
    }
}

public class GameStartState : GLOBALS.GLOBALS.IState
{
    public void OnEnter()
    {
        Debug.Log("Enter: " + ToString());
    }

    public void UpdateState(GLOBALS.GLOBALS.IContext context)
    {
        //Change to the previous state or next state here if a condition is met
        if (GameContext.GameStarted)
        {
            context.ChangeState(new GameRunningState());
        }
    }

    public void OnExit() { }
}

public class GameRunningState : GLOBALS.GLOBALS.IState
{

    public void OnEnter()
    {
        Debug.Log("enter " + ToString());
        Time.timeScale = 1;
    }

    public void UpdateState(GLOBALS.GLOBALS.IContext context)
    {
        //Change to the previous state or next state here if a condition is met
        if (Input.GetKeyDown(KeyCode.Escape))
            GameContext.PauseButtonClicked = !GameContext.PauseButtonClicked;
        if (GameContext.PauseButtonClicked)
        {
            context.ChangeState(new GamePausedState());
        }
    }

    public void OnExit()
    {

    }
}

public class GamePausedState : GLOBALS.GLOBALS.IState
{
    public void OnEnter()
    {
        Debug.Log("enter " + ToString());
        Time.timeScale = 0;
    }

    public void UpdateState(GLOBALS.GLOBALS.IContext context)
    {
        //Change to the previous state or next state here if a condition is met
        if (Input.GetKeyDown(KeyCode.Escape))
            GameContext.PauseButtonClicked = !GameContext.PauseButtonClicked;
        if (!GameContext.PauseButtonClicked)
        {
            context.ChangeState(new GameRunningState());
        }
    }

    public void OnExit()
    {
        Time.timeScale = 1;
    }
}