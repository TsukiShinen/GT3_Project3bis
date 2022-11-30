using BehaviorDesigner.Runtime;

namespace AddonBehaviourTree
{
	[System.Serializable]
	public class SharedTank : SharedVariable<Tank>
	{
		public override string ToString() { return mValue == null ? "null" : mValue.ToString(); }
		public static implicit operator SharedTank(Tank value) { return new SharedTank { mValue = value }; }
	}
}