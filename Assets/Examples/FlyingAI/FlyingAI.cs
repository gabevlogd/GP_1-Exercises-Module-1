using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlyingAI : MonoBehaviour
{
    [Header("Waypoints Settings")]
    public List<Transform> waypoints;
    public float waitTime = 2f;  // Time to wait at each waypoint

    [Header("Movement Settings")]
    public float speed = 5f;      // Flight speed
    public float stoppingDistance = 0.5f; // Distance to consider the waypoint reached
    public float obstacleAvoidanceDistance = 2f; // Distance to detect obstacles
    public float obstacleAvoidanceForce = 5f; // Intensity of the avoidance deviation
    public float SphereCastRadius = 0.5f;

    private Rigidbody rb;
    private Transform targetWaypoint;
    private bool isWaiting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable gravity to make it "fly"
        SelectNewWaypoint();
    }

    void FixedUpdate()
    {
        if (targetWaypoint == null || isWaiting) return;

        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        // Avoid obstacles using a raycast
        Vector3 avoidance = AvoidObstacles(direction);
        Vector3 finalDirection = (direction + avoidance).normalized;

        // Set the velocity
        rb.linearVelocity = finalDirection * speed * Time.fixedDeltaTime;

        // Check if the waypoint has been reached
        if (Vector3.Distance(transform.position, targetWaypoint.position) < stoppingDistance)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    void SelectNewWaypoint()
    {
        if (waypoints.Count == 0) return;
        targetWaypoint = waypoints[Random.Range(0, waypoints.Count)];
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        rb.linearVelocity = Vector3.zero; // Stop movement
        yield return new WaitForSeconds(waitTime);
        SelectNewWaypoint();
        isWaiting = false;
    }

    Vector3 AvoidObstacles(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, SphereCastRadius, direction, out hit, obstacleAvoidanceDistance))
        {
            // If an obstacle is detected, adjust the trajectory
            Vector3 avoidanceDir = Vector3.Cross(hit.normal, Vector3.up).normalized;
            return avoidanceDir * obstacleAvoidanceForce;
        }
        return Vector3.zero;
    }

    void OnDrawGizmos()
    {
        if (targetWaypoint != null)
        {
            Gizmos.color = Color.green;
            //Gizmos.DrawLine(transform.position, targetWaypoint.position);
            //Gizmos.DrawWireSphere(targetWaypoint.position, SphereCastRadius);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * obstacleAvoidanceDistance);
    }
}