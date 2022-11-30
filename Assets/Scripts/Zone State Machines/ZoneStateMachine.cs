using System;
using System.Collections.Generic;
using UnityEngine;

public class ZoneStateMachine : MonoBehaviour
{
    #region fields
    [SerializeField] private List<ZoneState> lstZStates;
    [SerializeField] private EZoneState currentZState;
    public MeshRenderer flags1;
    public MeshRenderer flags2;
    public SpriteRenderer flagIcon;
    public SpriteRenderer zoneCircle;

    public TeamSO teamScoring;
    public float score;
    public Dictionary<TeamSO, int> TeamsTanksInZone;
    #endregion

    #region Properties

    private BaseZoneState CurrentZState => lstZStates.Find(zs => zs.state == currentZState).machine;
    public EZoneState CurrentStateType => currentZState;
    public EZoneState LastZState { get; set; }
    #endregion

    #region Methods

    #region Start And Init
    private void Start()
    {
        score = 0;
        TeamsTanksInZone = new Dictionary<TeamSO, int>();
        SubGStateInit();
        CurrentZState.StartState();
    }

    private void SubGStateInit()
    {
        foreach (var gState in lstZStates)
        {
            gState.machine = Instantiate(gState.machine);
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
        if (!other.CompareTag("Tank")) return;

        var tank = other.GetComponent<Tank>();

        if (TeamsTanksInZone.ContainsKey(tank.team))
        {
            TeamsTanksInZone[tank.team]++;
        }
        else
        {
            TeamsTanksInZone.Add(tank.team, 1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Tank")) return;

        var tank = other.GetComponent<Tank>();

        if (TeamsTanksInZone.ContainsKey(tank.team))
        {
            TeamsTanksInZone[tank.team]--;

            if (TeamsTanksInZone[tank.team] == 0)
            {
                TeamsTanksInZone.Remove(tank.team);
            }
        }
    }
    #endregion
}

[Serializable]
public class ZoneState
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

