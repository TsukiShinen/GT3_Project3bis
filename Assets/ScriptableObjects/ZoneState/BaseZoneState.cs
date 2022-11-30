using State_Machines;
using UnityEngine;

namespace ScriptableObjects.ZoneState
{
    [CreateAssetMenu(fileName = "ZoneState", menuName = "ZoneState/Base")]
    public abstract class BaseZoneState : ScriptableObject
    {
        #region Fields
        protected ZoneStateMachine Machine;
        #endregion

        #region Properties
        #endregion

        #region Methods
        public void Init(ZoneStateMachine machine)
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
