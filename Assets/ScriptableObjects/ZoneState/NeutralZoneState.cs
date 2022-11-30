using State_Machines;
using UnityEngine;

namespace ScriptableObjects.ZoneState
{
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
            if (Machine.TeamsTanksInZone.Count == 1)
                Machine.ChangeState(EZoneState.CAPTURING);
            if (Machine.TeamsTanksInZone.Count > 1)
                Machine.ChangeState(EZoneState.CONFLICTED);
        }

        public override void FixedUpdateState()
        {

        }

        public override void LeaveState()
        {
            Machine.LastZState = EZoneState.NEUTRAL;
        }

        #endregion
    }
}
