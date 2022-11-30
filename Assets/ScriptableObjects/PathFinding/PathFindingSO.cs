using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace ScriptableObjects.PathFinding
{
    public abstract class PathFindingSO : ScriptableObject
    {
        public Grid grid;
        public abstract IEnumerable<Vector3> FindPath(Vector3 position, Vector3 targetPos, NavMeshAgent navMeshAgent = null);

        protected IEnumerable<Vector3> ListOfNodePosition => grid.Path.Select(node => node.WorldPosition).ToList();
        
        public void RetracePath(Node startNode, Node endNode)
        {
            var path = new List<Node>();
            var currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();

            grid.Path = path;

        }
    }
}
