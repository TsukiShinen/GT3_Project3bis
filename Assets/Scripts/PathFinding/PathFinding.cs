using UnityEngine;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour
{

    public Transform start, target;
    private Grid _grid;

    private void Awake()
    {
        _grid = GetComponent<Grid>();
    }

    private void Update()
    {
        FindPath(start.position, target.position);
    }

    private void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        var startNode = _grid.NodeFromWorldPoint(startPos);
        var targetNode = _grid.NodeFromWorldPoint(targetPos);

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
                return;
            }

            foreach (var neighbour in _grid.GetNeighbours(node))
            {
                if (!neighbour.Walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                var newCostToNeighbour = node.GCost + GetDistance(node, neighbour);
                if (newCostToNeighbour >= neighbour.GCost && openSet.Contains(neighbour)) continue;
                neighbour.GCost = newCostToNeighbour;
                neighbour.HCost = GetDistance(neighbour, targetNode);
                neighbour.Parent = node;

                if (!openSet.Contains(neighbour))
                    openSet.Add(neighbour);
            }
        }
    }

    private void RetracePath(Node startNode, Node endNode)
    {
        var path = new List<Node>();
        var currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Reverse();

        _grid.Path = path;

    }

    private static int GetDistance(Node nodeA, Node nodeB)
    {
        var dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        var dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}