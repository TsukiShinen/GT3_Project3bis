using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathFindingSO : ScriptableObject
{
    public Grid grid;

    public abstract List<Vector3> FindPath(Vector3 startPos, Vector3 targetPos);

    public void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;

    }

    public List<Vector3> ListOfNodePosition()
    {
        List<Vector3> list = new List<Vector3>();
        foreach(Node node in grid.path)
        {
            list.Add(node.worldPosition);
        }
        return list;
    }
}
