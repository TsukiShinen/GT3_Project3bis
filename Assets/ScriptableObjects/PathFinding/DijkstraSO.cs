using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ScriptableObjects.PathFinding
{
    [CreateAssetMenu(fileName = "AStarSO", menuName = "PathFinding/DijkstraSO", order = 0)]
    public class DijkstraSO : PathFindingSO
    {
        public override IEnumerable<Vector3> FindPath(Vector3 startPos, Vector3 targetPos, NavMeshAgent navMeshAgent = null)
        {
            var startNode = grid.NodeFromWorldPoint(startPos);
            var targetNode = grid.NodeFromWorldPoint(targetPos);

            var openSet = new List<Node>();
            var closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                var node = openSet[0];
                for (var i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost >= node.FCost && openSet[i].FCost != node.FCost) continue;
                    if (openSet[i].HCost < node.HCost)
                        node = openSet[i];
                }

                openSet.Remove(node);
                closedSet.Add(node);

                if (node == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    return ListOfNodePosition;
                }

                foreach (var neighbour in grid.GetNeighbours(node))
                {
                    if (!neighbour.Walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    var newCostToNeighbour = node.GCost;
                    if (newCostToNeighbour >= neighbour.GCost && openSet.Contains(neighbour)) continue;
                    neighbour.GCost = newCostToNeighbour;
                    neighbour.Parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }

            RetracePath(startNode, targetNode);
            return ListOfNodePosition;
        }
    }
}
