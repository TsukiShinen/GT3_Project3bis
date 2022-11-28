public abstract class BaseGameState
{
    #region Fields
    protected EGameState _state = EGameState.NONE;
    protected GameStateMachine _machine = null;
    #endregion

    #region Properties
    public EGameState State => _state;
    #endregion

    #region Methods
    public void Init(GameStateMachine machine, EGameState state)
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
