using UnityEngine;

[CreateAssetMenu(fileName = "ZoneState", menuName = "ZoneState/Base")]
public abstract class BaseZoneState : ScriptableObject
{
    #region Fields
    protected EZoneState _state = EZoneState.NONE;
    protected ZoneStateMachine _machine = null;
    #endregion

    #region Properties
    public EZoneState State => _state;
    #endregion

    #region Methods
    public void Init(ZoneStateMachine machine, EZoneState state)
    {
        _machine = machine;
        _state = state;
    }

    public abstract void StartState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void LeaveState();
    #endregion
}
