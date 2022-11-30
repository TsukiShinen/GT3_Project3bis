using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Library.Command;
using UnityEngine;

namespace BehaviourTree
{
    public class GoToTask : Action
    {
        public SharedTank Tank;
        public SharedVector3 Target;

        public override void OnStart()
        {
            CommandManager.Instance.AddCommand(new MoveToCommand(Tank.Value, Target.Value));
        }

        public override TaskStatus OnUpdate()
        {
            
            return TaskStatus.Running;
        }
    }
}