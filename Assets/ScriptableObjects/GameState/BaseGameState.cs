using State_Machines;
using UnityEngine;

namespace ScriptableObjects.GameState
{
    [CreateAssetMenu(fileName = "GameState", menuName = "GameState/Base")]
    public abstract class BaseGameState : ScriptableObject
    {
        #region Fields
        protected GameStateMachine Machine;
        #endregion

        #region Properties
        #endregion

        #region Methods
        public void Init(GameStateMachine machine)
        {
            Machine = machine;
        }

        public abstract void StartState();
        public abstract void UpdateState();
        public abstract void FixedUpdateState();
        public abstract void LeaveState();
        #endregion
    }
}
