using System.Linq;
using State_Machines;
using UnityEngine;

namespace ScriptableObjects.ZoneState
{
    [CreateAssetMenu(fileName = "ZoneState", menuName = "ZoneState/Captured")]
    public class CapturedZoneState : BaseZoneState
    {
        #region fileds

        [SerializeField] private GameManagerSO gameManagerSo;
        [SerializeField] private GameParametersSO gameParametersSo;

        private float _timer;
        #endregion

        #region Properties

        #endregion

        #region Methods   
        public override void StartState()
        {
            _timer = 0;
            Machine.score = gameParametersSo.timeTakeZone;
            Machine.flags1.material.color = Machine.teamScoring.TeamColor;
            Machine.flags2.material.color = Machine.teamScoring.TeamColor;
            Machine.flagIcon.color = Machine.teamScoring.TeamColor;
        }

        public override void UpdateState()
        {
            // Changing state
            if (Machine.score <= 0)
                Machine.ChangeState(EZoneState.NEUTRAL);
            if (Machine.TeamsTanksInZone.Count > 1)
                Machine.ChangeState(EZoneState.CONFLICTED);

            Scoring();

            if (Machine.TeamsTanksInZone.Count == 0)
            { 
                Machine.score -= Time.deltaTime / 2f;
            }
            else
            {
                var team = Machine.TeamsTanksInZone.Keys.ToList()[0];
                if (team == Machine.teamScoring)
                {
                    if (Machine.score <= gameParametersSo.timeTakeZone)
                    {
                        Machine.score += Time.deltaTime;
                    }
                }
                else
                {
                    Machine.score -= Time.deltaTime;
                    if (Machine.score <= 0)
                    {
                        Machine.score = 0;
                        Machine.teamScoring = team;
                    }
                }
            }
        
            Machine.zoneCircle.color = Machine.teamScoring.TeamColor;
            Machine.zoneCircle.transform.localScale = new Vector3(Machine.score / gameParametersSo.timeTakeZone, Machine.score / gameParametersSo.timeTakeZone, 1);
        }

        private void Scoring()
        {
            _timer += Time.deltaTime;
            if (!(_timer >= 1)) return;
            _timer = 0;
            gameManagerSo.Scores[Machine.teamScoring]++;
        }

        public override void FixedUpdateState()
        {

        }

        public override void LeaveState()
        {
            Machine.flags1.material.color = Color.white;
            Machine.flags2.material.color = Color.white;
            Machine.LastZState = EZoneState.CAPTURED;
            Machine.flagIcon.color = Color.black;
        }

        #endregion
    }
}
