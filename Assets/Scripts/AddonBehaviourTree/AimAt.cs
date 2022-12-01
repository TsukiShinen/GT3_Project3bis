using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Library.Command;

namespace AddonBehaviourTree
{
	public class AimAt : Action
	{
        public SharedTank Tank;
        public SharedTank Target;

        public override void OnStart()
		{
			CommandManager.Instance.AddCommand(new TurnToCommand(Tank.Value, Target.Value.transform));
        }

		public override TaskStatus OnUpdate()
		{
			if (!Tank.Value.tankMovement.mustTurn)
				return TaskStatus.Success;

            return TaskStatus.Running;
        }
	}
}