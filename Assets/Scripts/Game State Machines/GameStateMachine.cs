using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    #region fields
    [SerializeField] private List<GameState> lstGStates;

    [SerializeField] private EGameState currentGState;
    #endregion

    #region Properties
    public BaseGameState CurrentGState => lstGStates.Find(gs => gs.state == currentGState).machine;
    public EGameState CurrentStateType => currentGState;
    public EGameState LastGState { get; set; }
    #endregion

    #region Methods

    #region Start And Init
    private void Start()
    {
        SubGStateInit();
        CurrentGState.StartState();
    }

    private void SubGStateInit()
    {
        foreach (var gState in lstGStates)
        {
            gState.machine.Init(this, gState.state);
        }

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
        currentGState = nextState;
        CurrentGState.StartState();
    }

    #endregion

    #endregion
}

[Serializable]
public struct GameState
{
    public EGameState state;
    public BaseGameState machine;
}

public enum EGameState
{
    START,
    MENU,
    NONE
}

