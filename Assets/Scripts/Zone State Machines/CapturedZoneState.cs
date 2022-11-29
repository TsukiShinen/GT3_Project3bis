using UnityEngine;

[CreateAssetMenu(fileName = "ZoneState", menuName = "ZoneState/Captured")]
public class CapturedZoneState : BaseZoneState
{
    #region fileds

    [SerializeField] private GameManagerSO gameManagerSo;

    private float _timer;
    #endregion

    #region Properties

    #endregion

    #region Methods   
    public override void StartState()
    {
       Debug.Log("Captured");
       _timer = 0;
    }

    public override void UpdateState()
    {
        // Changing state
        if (_machine.TeamsTanksInZone.Count == 0)
            _machine.ChangeState(EZoneState.NEUTRAL);
        if (_machine.TeamsTanksInZone.Count > 1)
            _machine.ChangeState(EZoneState.CONFLICTED);

        _timer += Time.deltaTime;
        if (_timer >= 1)
        {
            _timer = 0;
            gameManagerSo.Scores[_machine.teamScoring]++;
        }
    }

    public override void FixedUpdateState()
    {

    }

    public override void LeaveState()
    {
        _machine.LastZState = EZoneState.CAPTURED;
    }

    #endregion
}
