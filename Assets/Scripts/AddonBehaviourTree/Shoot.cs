using BehaviorDesigner.Runtime.Tasks;

namespace AddonBehaviourTree
{
	public class Shoot : Action
	{
		public override void OnStart()
		{
		
		}

		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}