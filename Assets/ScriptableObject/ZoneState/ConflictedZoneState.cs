using UnityEngine;

[CreateAssetMenu(fileName = "ZoneState", menuName = "ZoneState/Conflict")]
public class ConflictedZoneState : BaseZoneState
{
    #region fileds

    #endregion

    #region Properties

    #endregion

    #region Methods   
    public override void StartState()
    {
        Debug.Log("Conflicted");
    }

    public override void UpdateState()
    {
        // Changing state
        if (_machine.TeamsTanksInZone.Count == 0)
            _machine.ChangeState(EZoneState.NEUTRAL);
        if (_machine.TeamsTanksInZone.Count == 1)
            _machine.ChangeState(EZoneState.CAPTURING);
    }

    public override void FixedUpdateState()
    {

    }

    public override void LeaveState()
    {
        _machine.LastZState = EZoneState.CONFLICTED;
    }

    #endregion
}
