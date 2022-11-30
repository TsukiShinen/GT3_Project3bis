using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace ScriptableObjects.PathFinding
{
    [CreateAssetMenu(fileName = "NavMeshSO", menuName = "PathFinding/NavMeshSO", order = 0)]
    public class NavMeshSO : PathFindingSO
    {
        public override IEnumerable<Vector3> FindPath(Vector3 position, Vector3 targetPos, NavMeshAgent navMeshAgent = null)
        {
            if (!navMeshAgent) return null;
            var path = new NavMeshPath();
            navMeshAgent.CalculatePath(targetPos, path);

            var finalPath = new List<Vector3>(path.corners);
            var gridPath = finalPath.Select(pos => grid.NodeFromWorldPoint(pos)).ToList();

            grid.Path = gridPath;
            return finalPath;
        }
    }
}
