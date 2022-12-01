using BehaviorDesigner.Runtime.Tasks;
using Library.Command;
using UnityEngine;

namespace AddonBehaviourTree
{
    public class GoTo : Action
    {
        public SharedTank Tank;
        public SharedZone Target;

        public override void OnStart()
        {
            Debug.Log(Target.Value.transform.position);
            CommandManager.Instance.AddCommand(new MoveToCommand(Tank.Value, Target.Value.transform.position));
        }

        public override TaskStatus OnUpdate()
        {
            return Tank.Value.ArrivedAtDestination ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}