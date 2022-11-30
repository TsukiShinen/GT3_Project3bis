using State_Machines;
using UnityEngine;

namespace ScriptableObjects.ZoneState
{
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
        }

        public override void UpdateState()
        {
            // Changing state
            if (Machine.TeamsTanksInZone.Count == 0)
                Machine.ChangeState(EZoneState.NEUTRAL);
            if (Machine.TeamsTanksInZone.Count == 1)
                Machine.ChangeState(EZoneState.CAPTURING);
        }

        public override void FixedUpdateState()
        {

        }

        public override void LeaveState()
        {
            Machine.LastZState = EZoneState.CONFLICTED;
        }

        #endregion
    }
}
