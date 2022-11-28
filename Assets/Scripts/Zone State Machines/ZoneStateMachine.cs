using System;
using System.Collections.Generic;
using UnityEngine;

public class ZoneStateMachine : MonoBehaviour
{
    #region fields
    [SerializeField] private List<ZoneState> lstZStates;

    [SerializeField] private EZoneState currentZState;
    #endregion

    #region Properties
    public BaseZoneState CurrentZState => lstZStates.Find(zs => zs.state == currentZState).machine;
    public EZoneState CurrentStateType => currentZState;
    public EZoneState LastZState { get; set; }
    #endregion

    #region Methods

    #region Start And Init
    private void Start()
    {
        SubGStateInit();
        CurrentZState.StartState();
    }

    private void SubGStateInit()
    {
        foreach (var gState in lstZStates)
        {
            gState.machine.Init(this, gState.state);
        }

    }
    #endregion

    #region RunTime
    private void Update()
    {
        CurrentZState.UpdateState();
    }

    private void FixedUpdate()
    {
        CurrentZState.FixedUpdateState();
    }

    public void ChangeState(EZoneState nextState)
    {
        CurrentZState.LeaveState();
        currentZState = nextState;
        CurrentZState.StartState();
    }

    #endregion
    private void OnTriggerEnter(Collider other)
    {
        ChangeState(EZoneState.CAPTURING);
    }
    private void OnTriggerExit(Collider other)
    {
        ChangeState(EZoneState.NEUTRAL);
    }
    #endregion
}

[Serializable]
public struct ZoneState
{
    public EZoneState state;
    public BaseZoneState machine;
}

public enum EZoneState
{
    NEUTRAL,
    CAPTURING,
    CAPTURED,
    CONFLICTED,
    NONE
}

