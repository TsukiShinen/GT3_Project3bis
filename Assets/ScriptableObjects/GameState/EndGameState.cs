using State_Machines;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjects.GameState
{
    [CreateAssetMenu(fileName = "GameState", menuName = "GameState/End")]
    public class EndGameState : BaseGameState
    {
        public override void StartState()
        {
            SceneManager.LoadScene("EndMenu");
        }

        public void GoToStartMenu()
        {
            Machine.ChangeState(EGameState.MENU);
        }

        public override void UpdateState()
        {
            
        }

        public override void FixedUpdateState()
        {
            
        }

        public override void LeaveState()
        {
            
        }
    }
}