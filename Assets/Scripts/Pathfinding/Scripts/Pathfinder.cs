using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{

    internal PathfindManager manager; 

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public Grid<PathNode> Grid { get => _grid; }
    private Grid<PathNode> _grid;
    private List<PathNode> _openList;
    private List<PathNode> _closedList;

    public Pathfinder(int width, int height, int nodeSize, Vector3 worldPositionOrigin)
    {
        _grid = new Grid<PathNode>(width, height, nodeSize, worldPositionOrigin, (int x, int y) => new PathNode(x, y));
    }

    /// <summary>
    /// Returns a path of Vector3 from startWorldPosition to endWorldPosition using A* algorithm
    /// </summary>
    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {
        Vector2Int startCoord = _grid.GetGridPosition(startWorldPosition);
        Vector2Int endCoord = _grid.GetGridPosition(endWorldPosition);

        List<PathNode> path = FindPath(startCoord.x, startCoord.y, endCoord.x, endCoord.y);
        if (path == null) return null;
        else
        {
            return PathNodeToVectorPath(path);
        }
    }

    /// <summary>
    /// Returns a path of PathNode from passed start coordinates to end cordinates using A* algorithm
    /// </summary>
    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = _grid.GetGridObject(startX, startY);
        PathNode endNode = _grid.GetGridObject(endX, endY);

        if (startNode == null || endNode == null) return null; //invalid path

        _openList = new List<PathNode> { startNode };
        _closedList = new List<PathNode>();

        for (int x = 0; x < _grid.Width; x++)
        {
            for (int y = 0; y < _grid.Height; y++)
            {
                PathNode pathNode = _grid.GetGridObject(x, y);
                pathNode.gCost = 99999999;
                pathNode.CalculateFCost();
                pathNode.CameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (_openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(_openList);
            if (currentNode == endNode) return CalculatePath(endNode);

            _openList.Remove(currentNode);
            _closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (_closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.IsWalkable)
                {
                    _closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.CameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!_openList.Contains(neighbourNode)) _openList.Add(neighbourNode);
                }
            }
        }
        // Out of nodes on the openList
        return null;
    }

    /// <summary>
    /// Returns a list of all currentNode's neighbours
    /// </summary>
    public List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.X - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y));
            // Left Down
            if (currentNode.Y - 1 >= 0) neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y - 1));
            // Left Up
            if (currentNode.Y + 1 < _grid.Height) neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y + 1));
        }
        if (currentNode.X + 1 < _grid.Width)
        {
            // Right
            neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y));
            // Right Down
            if (currentNode.Y - 1 >= 0) neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y - 1));
            // Right Up
            if (currentNode.Y + 1 < _grid.Height) neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y + 1));
        }
        // Down
        if (currentNode.Y - 1 >= 0) neighbourList.Add(GetNode(currentNode.X, currentNode.Y - 1));
        // Up
        if (currentNode.Y + 1 < _grid.Height) neighbourList.Add(GetNode(currentNode.X, currentNode.Y + 1));

        return neighbourList;
    }

    public PathNode GetNode(int x, int y) => _grid.GetGridObject(x, y);

    /// <summary>
    /// Returns the path obtained from the A* algorithm
    /// </summary>
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;

        while (currentNode.CameFromNode != null)
        {
            path.Add(currentNode.CameFromNode);
            currentNode = currentNode.CameFromNode;
        }
        path.Reverse();
        return path;
    }

    /// <summary>
    /// Return the value of the path cost from a to b whit no obstacle in between
    /// </summary>
    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.X - b.X);
        int yDistance = Mathf.Abs(a.Y - b.Y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost) lowestFCostNode = pathNodeList[i];
        }
        return lowestFCostNode;
    }

    /// <summary>
    /// Converts a list of PathNode in to a list of Vectro3
    /// </summary>
    private List<Vector3> PathNodeToVectorPath(List<PathNode> path)
    {
        List<Vector3> vectorPath = new List<Vector3>();
        foreach (PathNode pathNode in path)
        {
            //vectorPath.Add(new Vector3(pathNode.x, 0f, pathNode.y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f); //codeMonkey       //TAKE A LOOK HERE
            vectorPath.Add(_grid.GetWorldPosition(pathNode.X, pathNode.Y)); //mine
        }
        return vectorPath;
    }

}
