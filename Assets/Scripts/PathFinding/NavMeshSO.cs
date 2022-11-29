using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "NavMeshSO", menuName = "PathFinding/NavMeshSO", order = 0)]
public class NavMeshSO : PathFindingSO
{
    public override List<Vector3> FindPath(Vector3 Position, Vector3 targetPos, NavMeshAgent navMeshAgent)
    {
        var path = new NavMeshPath();

        navMeshAgent.CalculatePath(targetPos, path);

        return new List<Vector3>(path.corners);
    }
}
