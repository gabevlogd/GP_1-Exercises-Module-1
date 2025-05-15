using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PathfindManager : Singleton<PathfindManager>
{
    [SerializeField]
    private PathfindData _data;
    static public PathfindData Data { get => This._data; }

    private Pathfinder _pathfinder;

    protected override void Awake()
    {
        base.Awake();

        if (_data == null)
        {
            _data = ScriptableObject.CreateInstance<PathfindData>();
            Debug.LogWarning("PathfindManager has invalid data, initializing with default data values");
        }

        _pathfinder = new Pathfinder(_data.GridWidth, _data.GridHeight, _data.GridNodeSize, _data.GridOriginWorlPosition);
    }

    static public bool TryFindPath(Vector3 startPosition, Vector3 endPosition, out List<Vector3> path)
    {
        path = null;

        if (This == null)
        {
            Debug.LogWarning("Pathfind Manager not instanced properly");
            return false;
        }

        path = This._pathfinder.FindPath(startPosition, endPosition);

        if (path == null)
        {
            return false;
        }
        return true;
    }

    static public void RandomizeObstacles()
    {
        if (This == null)
        {
            Debug.LogWarning("Pathfind Manager not instanced properly");
            return;
        }
        for (int row = 0; row < This._pathfinder.Grid.Height; row++)
        {
            for (int column = 0; column < This._pathfinder.Grid.Width; column++)
            {
                PathNode pathNode = This._pathfinder.Grid.GetGridObject(row, column);
                pathNode.IsWalkable = Random.Range(0f, 1f) > 0.3f;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        VisualizeGrid();
        VisualizeObstacles();
    }
#endif

    private void VisualizeGrid()
    {
        if (_pathfinder == null) return;

        for (int row = 0; row < _pathfinder.Grid.Height; row++)
        {
            for (int column = 0; column < _pathfinder.Grid.Width; column++)
            {
                Vector3 tileCenter = _pathfinder.Grid.GetWorldPosition(row, column);
                Vector3 vertex1 = new Vector3(tileCenter.x - _pathfinder.Grid.CellSize * 0.5f, 0f, tileCenter.z - _pathfinder.Grid.CellSize * 0.5f);
                Vector3 vertex2 = new Vector3(tileCenter.x + _pathfinder.Grid.CellSize * 0.5f, 0f, tileCenter.z + _pathfinder.Grid.CellSize * 0.5f);
                Vector3 vertex3 = new Vector3(tileCenter.x + _pathfinder.Grid.CellSize * 0.5f, 0f, tileCenter.z - _pathfinder.Grid.CellSize * 0.5f);
                Vector3 vertex4 = new Vector3(tileCenter.x - _pathfinder.Grid.CellSize * 0.5f, 0f, tileCenter.z + _pathfinder.Grid.CellSize * 0.5f);
                Gizmos.DrawLine(vertex1, vertex4);
                Gizmos.DrawLine(vertex4, vertex2);
                Gizmos.DrawLine(vertex2, vertex3);
                Gizmos.DrawLine(vertex3, vertex1);
            }
        }
    }

    private void VisualizeObstacles()
    {
        if (_pathfinder == null) return;
        for (int row = 0; row < _pathfinder.Grid.Height; row++)
        {
            for (int column = 0; column < _pathfinder.Grid.Width; column++)
            {
                PathNode pathNode = _pathfinder.Grid.GetGridObject(row, column);
                if (pathNode.IsWalkable)
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }
                Gizmos.DrawCube(_pathfinder.Grid.GetWorldPosition(row, column), Vector3.one * _pathfinder.Grid.CellSize * 0.5f);
            }
        }
    }
}
