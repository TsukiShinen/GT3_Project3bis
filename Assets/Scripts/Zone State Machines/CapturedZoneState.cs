using System.Linq;
using DefaultNamespace;
using UnityEngine;

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
       Debug.Log("Captured");
       _timer = 0;
       _machine.score = gameParametersSo.timeTakeZone;
       _machine.flags1.enabled = true;
       _machine.flags2.enabled = true;
       _machine.flags1.material.color = _machine.teamScoring.TeamColor;
       _machine.flags2.material.color = _machine.teamScoring.TeamColor;
       _machine.flagIcon.color = _machine.teamScoring.TeamColor;
    }

    public override void UpdateState()
    {
        // Changing state
        if (_machine.score <= 0)
            _machine.ChangeState(EZoneState.NEUTRAL);
        if (_machine.TeamsTanksInZone.Count > 1)
            _machine.ChangeState(EZoneState.CONFLICTED);

        Scoring();

        if (_machine.TeamsTanksInZone.Count == 0)
        { 
            _machine.score -= Time.deltaTime / 2f;
        }
        else
        {
            if (_machine.score <= gameParametersSo.timeTakeZone)
            {
                _machine.score += Time.deltaTime;
            }
        }
        
        _machine.zoneCircle.color = _machine.teamScoring.TeamColor;
        _machine.zoneCircle.transform.localScale = new Vector3(_machine.score / gameParametersSo.timeTakeZone, _machine.score / gameParametersSo.timeTakeZone, 1);
    }

    private void Scoring()
    {
        _timer += Time.deltaTime;
        if (!(_timer >= 1)) return;
        _timer = 0;
        gameManagerSo.Scores[_machine.teamScoring]++;
    }

    public override void FixedUpdateState()
    {

    }

    public override void LeaveState()
    {
        _machine.flags1.enabled = false;
        _machine.flags2.enabled = false;
        _machine.LastZState = EZoneState.CAPTURED;
        _machine.flagIcon.color = Color.black;
    }

    #endregion
}
