using UnityEngine;
using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using State_Machines;

[System.Serializable]
public class SharedListZone : SharedVariable<List<ZoneStateMachine>>
{
	public override string ToString() { return mValue == null ? "null" : mValue.ToString(); }
	public static implicit operator SharedListZone(List<ZoneStateMachine> value) { return new SharedListZone { mValue = value }; }
}