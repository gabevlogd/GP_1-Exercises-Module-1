using UnityEngine;

[CreateAssetMenu(fileName = "PathfindData", menuName = "Scriptable Objects/PathfindData")]
public class PathfindData : ScriptableObject
{

    public PathfindData()
    {
        GridWidth = 0;
        GridHeight = 0;
        GridNodeSize = 1;
        GridOriginWorlPosition = Vector3.zero;
    }

    public int GridWidth = 0;

    public int GridHeight = 0;

    public int GridNodeSize = 1;

    public Vector3 GridOriginWorlPosition = Vector3.zero;
    
}
