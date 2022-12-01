using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using AddonBehaviourTree;
using System.Linq;

public class SelectNextZone : Action
{
	public SharedTank tank;
	public SharedZone zoneTarget;
	public SharedListZone listZones;
	
	public override void OnStart()
	{

    }

	public override TaskStatus OnUpdate()
	{
		if(zoneTarget != null)
		{
			if (zoneTarget.Value!= null)
			{
                if (zoneTarget.Value.currentZState != State_Machines.EZoneState.CAPTURED)
                {
                    return TaskStatus.Success;
                }
            }
		}
		
		var lst = listZones.Value.Where(z => (z.currentZState == State_Machines.EZoneState.CAPTURED && z.teamScoring != tank.Value.team)).ToList();

		if (lst.Count == 0) 
			lst = listZones.Value.Where(z => z.currentZState != State_Machines.EZoneState.CAPTURED).ToList();
		
		zoneTarget.Value = lst[Random.Range(0, lst.Count - 1)];

		return TaskStatus.Success;
	}
}