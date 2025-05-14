using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPathfind : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private List<Vector3> currentPath;

    private void Start()
    {
        PathfindManager.RandomizeObstacles();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Vector3 randomEndPosition = new Vector3(Random.Range(0, 8), transform.position.y, Random.Range(0, 8));
            randomEndPosition.y = transform.position.y;

            if (PathfindManager.TryFindPath(transform.position, randomEndPosition, out currentPath))
            {
                Debug.Log("Start: " + transform.position + "  End: " + randomEndPosition);
                StopAllCoroutines();
                StartCoroutine(FollowPath(currentPath));
            }
            else
            {
                Debug.Log("No valid path found");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PathfindManager.RandomizeObstacles();
        }
    }

    private IEnumerator FollowPath(List<Vector3> path)
    {
        if (path == null)
        {
            yield break;
        }

        int currentPathIndex = 0;

        while (currentPathIndex < path.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[currentPathIndex], Time.deltaTime * speed);

            if (Vector3.Distance(transform.position, path[currentPathIndex]) < 0.1f)
            {
                currentPathIndex++;
            }

            yield return null;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        VisualizePath();
    }
#endif

    private void VisualizePath()
    {
        if (currentPath == null) return;
        for (int i = 0; i < currentPath.Count; i++)
        {
            Gizmos.DrawSphere(currentPath[i], 0.5f);
        }
    }
}

