using BehaviorDesigner.Runtime.Tasks;
using Library.Command;

namespace AddonBehaviourTree
{
	public class Shoot : Action
	{
		public SharedTank Tank;


        public override void OnStart()
		{
			CommandManager.Instance.AddCommand(new FireCommand(Tank.Value));
		}

		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}