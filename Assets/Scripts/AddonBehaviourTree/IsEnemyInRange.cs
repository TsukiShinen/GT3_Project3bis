using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace AddonBehaviourTree
{
    public class IsEnemyInRange : Conditional
    {
        public SharedTank Tank;

        public override TaskStatus OnUpdate()
        {



            return TaskStatus.Success;
        }
    }
}
