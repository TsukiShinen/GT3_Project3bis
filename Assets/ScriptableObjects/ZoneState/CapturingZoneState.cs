using System.Linq;
using State_Machines;
using UnityEngine;

namespace ScriptableObjects.ZoneState
{
    [CreateAssetMenu(fileName = "ZoneState", menuName = "ZoneState/Capturing")]
    public class CapturingZoneState : BaseZoneState
    {
        #region fileds
        [SerializeField] private GameParametersSO gameParametersSo;
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
            if (Machine.TeamsTanksInZone.Count == 0 && Machine.score <= 0)
                Machine.ChangeState(EZoneState.NEUTRAL);
            if (Machine.TeamsTanksInZone.Count > 1)
                Machine.ChangeState(EZoneState.CONFLICTED);
            if (Machine.score >= gameParametersSo.timeTakeZone)
                Machine.ChangeState(EZoneState.CAPTURED);
        
            if (Machine.TeamsTanksInZone.Count == 0)
            {
                Debug.Log("minus");
                Machine.score -= Time.deltaTime / 2f;
            }
            else
            {
                var team = Machine.TeamsTanksInZone.Keys.ToList()[0];
                if (team == Machine.teamScoring)
                {
                    Machine.score += Time.deltaTime;
                }
                else
                {
                    Machine.score -= Time.deltaTime;
                    if (!(Machine.score <= 0))
                    {
                        Machine.score = 0;
                        Machine.teamScoring = team;
                    }
                }
            }

            Machine.zoneCircle.color = Machine.teamScoring.TeamColor;
            Machine.zoneCircle.transform.localScale = new Vector3(Machine.score / gameParametersSo.timeTakeZone, Machine.score / gameParametersSo.timeTakeZone, 1);
        }

        public override void FixedUpdateState()
        {

        }

        public override void LeaveState()
        {
            Machine.LastZState = EZoneState.CAPTURING;
        }

        #endregion
    }
}
