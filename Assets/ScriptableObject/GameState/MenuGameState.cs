using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "GameState/Menu")]
public class MenuGameState : BaseGameState
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

    }

    public override void FixedUpdateState()
    {

    }

    public override void LeaveState()
    {
        _machine.LastGState = EGameState.MENU;
    }

    public void PlayButton()
    {
        _machine.ChangeState(EGameState.GAMEPLAY);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    #endregion
}
