using State_Machines;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjects.GameState
{
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
            SceneManager.LoadScene("StartMenu");
        }

        public override void UpdateState()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayButton();
            }
        }

        public override void FixedUpdateState()
        {

        }

        public override void LeaveState()
        {
            Machine.LastGState = EGameState.MENU;
        }

        public void PlayButton()
        {
            Machine.ChangeState(EGameState.GAMEPLAY);
        }

        public void QuitButton()
        {
            Application.Quit();
        }

        #endregion
    }
}
