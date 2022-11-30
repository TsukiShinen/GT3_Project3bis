using State_Machines;
using UnityEngine;

namespace ScriptableObjects.GameState
{
    [CreateAssetMenu(fileName = "GameState", menuName = "GameState/Start")]
    public class StartGameState : BaseGameState
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
            Machine.LastGState = EGameState.START;
        }

        #endregion
    }
}
