using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    #region fields
    private Dictionary<EGameState, BaseGameState> _statesGDict = new Dictionary<EGameState, BaseGameState>();

    private EGameState _currentGState;
    #endregion

    #region Properties
    public BaseGameState CurrentGState => _statesGDict[_currentGState];
    public EGameState CurrentStateType => _currentGState;
    public EGameState LastGState { get; set; }
    #endregion

    #region Methods

    #region Start And Init
    private void Start()
    {
        _statesGDict = new Dictionary<EGameState, BaseGameState>();
        SubGStateInit();
        CurrentGState.StartState();
    }

    private void SubGStateInit()
    {
        var startState = new StartGameState();
        startState.Init(this, EGameState.START);
        _statesGDict.Add(EGameState.START, startState);

        var menuState = new MenuGameState();
        menuState.Init(this, EGameState.MENU);
        _statesGDict.Add(EGameState.MENU, menuState);

    }
    #endregion

    #region RunTime
    private void Update()
    {
        CurrentGState.UpdateState();
    }

    private void FixedUpdate()
    {
        CurrentGState.FixedUpdateState();
    }

    public void ChangeState(EGameState nextState,bool switchSong)
    {
        CurrentGState.LeaveState();
        _currentGState = nextState;
        CurrentGState.StartState();
    }

    #endregion

    #endregion
}

public enum EGameState
{
    START,
    MENU,
    NONE
}

