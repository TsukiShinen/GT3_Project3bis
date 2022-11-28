using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "GameState/Gameplay")]
public class GameplayGameState : BaseGameState
{
    #region fileds
    [SerializeField] private GameManagerSO gameManagerSO;
    #endregion

    #region Properties

    #endregion

    #region Methods   
    public override void StartState()
    {
        gameManagerSO.Init();
    }

    public override void UpdateState()
    {
        gameManagerSO.timer -= Time.deltaTime;
    }

    public override void FixedUpdateState()
    {

    }

    public override void LeaveState()
    {
        _machine.LastGState = EGameState.START;
    }

    #endregion
}