using AddonBehaviourTree;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BulletEntered : Conditional
{
	public SharedTank tank;
	
	public override TaskStatus OnUpdate()
	{
		return tank.Value.tankBulletDetection.bulletEntered ? TaskStatus.Success : TaskStatus.Failure;
	}
}