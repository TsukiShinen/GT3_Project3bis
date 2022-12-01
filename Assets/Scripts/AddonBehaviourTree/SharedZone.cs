using BehaviorDesigner.Runtime;
using State_Machines;

namespace AddonBehaviourTree
{
	[System.Serializable]
	public class SharedZone : SharedVariable<ZoneStateMachine>
	{
		public override string ToString() { return mValue == null ? "null" : mValue.ToString(); }
		public static implicit operator SharedZone(ZoneStateMachine value) { return new SharedZone { mValue = value }; }
	}
}