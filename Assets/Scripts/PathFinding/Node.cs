using UnityEngine;

public class Node
{

    public readonly bool Walkable;
    public Vector3 WorldPosition;
    public readonly int GridX;
    public readonly int GridY;

    public int GCost;
    public int HCost;
    public Node Parent;

    public int FCost => GCost + HCost;

    public Node(bool walkable, Vector3 worldPos, int gridX, int gridY)
    {
        Walkable = walkable;
        WorldPosition = worldPos;
        GridX = gridX;
        GridY = gridY;
    }
}