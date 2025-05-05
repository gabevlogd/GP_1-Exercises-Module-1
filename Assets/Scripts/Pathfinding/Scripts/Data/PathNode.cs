using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public int X;
    public int Y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool IsWalkable;
    public PathNode CameFromNode;

    public PathNode(int x, int y)
    {
        this.X = x;
        this.Y = y;
        IsWalkable = true;
    }

    public void CalculateFCost() => fCost = gCost + hCost;

    public override string ToString() => X + ", " + Y;

}
