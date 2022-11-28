using UnityEngine;

[CreateAssetMenu(fileName = "ZoneState", menuName = "ZoneState/Capturing")]
public class CapturingZoneState : BaseZoneState
{
    #region fileds

    #endregion

    #region Properties

    #endregion

    #region Methods   
    public override void StartState()
    {
       
    }

    public override void UpdateState()
    {

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
