using BehaviorDesigner.Runtime.Tasks;
using Library.Command;
using UnityEngine;

namespace AddonBehaviourTree
{
	public class SpecialJump : Action
	{
		public SharedTank tank;
		
		public override void OnStart()
		{
			CommandManager.Instance.AddCommand(new SpecialJumpCommand(tank.Value));
		}

		public override TaskStatus OnUpdate()
		{
			return !tank.Value.isJumping ? TaskStatus.Success : TaskStatus.Running;
		}
	}
}