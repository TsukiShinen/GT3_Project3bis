using UnityEngine;

[CreateAssetMenu(fileName = "ZoneState", menuName = "ZoneState/Neutral")]
public class NeutralZoneState : BaseZoneState
{
    #region fileds

    #endregion

    #region Properties

    #endregion

    #region Methods   
    public override void StartState()
    {
        Debug.Log("Neutral");
    }

    public override void UpdateState()
    {
        // Changing state
        if (_machine.TeamsTanksInZone.Count == 1)
            _machine.ChangeState(EZoneState.CAPTURING);
        if (_machine.TeamsTanksInZone.Count > 1)
            _machine.ChangeState(EZoneState.CONFLICTED);

        if (_machine.score != 0f)
        {
            _machine.score -= Time.deltaTime / 2f;
        }
    }

    public override void FixedUpdateState()
    {

    }

    public override void LeaveState()
    {
        _machine.LastZState = EZoneState.NEUTRAL;
    }

    #endregion
}
