using System.Linq;
using DefaultNamespace;
using UnityEngine;

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
        Debug.Log("Capturing");
    }

    public override void UpdateState()
    {
        // Changing state
        if (_machine.TeamsTanksInZone.Count == 0 && _machine.score <= 0)
            _machine.ChangeState(EZoneState.NEUTRAL);
        if (_machine.TeamsTanksInZone.Count > 1)
            _machine.ChangeState(EZoneState.CONFLICTED);
        if (_machine.score >= gameParametersSo.timeTakeZone)
            _machine.ChangeState(EZoneState.CAPTURED);
        
        if (_machine.TeamsTanksInZone.Count == 0)
        {
            Debug.Log("minus");
            _machine.score -= Time.deltaTime / 2f;
        }
        else
        {
            var team = _machine.TeamsTanksInZone.Keys.ToList()[0];
            if (team == _machine.teamScoring)
            {
                _machine.score += Time.deltaTime;
            }
            else
            {
                _machine.score -= Time.deltaTime;
                if (!(_machine.score <= 0)) return;
                _machine.score = 0;
                _machine.teamScoring = team;
            }
        }

        _machine.zoneCircle.color = _machine.teamScoring.TeamColor;
        _machine.zoneCircle.transform.localScale = new Vector3(_machine.score / gameParametersSo.timeTakeZone, _machine.score / gameParametersSo.timeTakeZone, 1);
    }

    public override void FixedUpdateState()
    {

    }

    public override void LeaveState()
    {
        _machine.LastZState = EZoneState.CAPTURING;
    }

    #endregion
}
