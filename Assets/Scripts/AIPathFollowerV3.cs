using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class AIPathFollowerV3 : MonoBehaviour
{
    [Header("PathfindingV3")]
    public PathNodeV3 startNode;
    public PathNodeV3 endNode;
    public List<PathNodeV3> currentPath; // The BFS-discovered path

    [Header("Movement Settings")]
    [Tooltip("Movement speed for the agent")]
    public float speed = 5f;

    [Tooltip("Higher means more responsive direction changes")]
    public float steeringForce = 10f;

    [Tooltip("Distance threshold to consider a node 'reached'")]
    public float waypointThreshold = 0.2f;

    [Header("Obstacle Avoidance")]
    public float obstacleAvoidanceForce = 5f;
    public float obstacleDetectionDistance = 1f;
    public LayerMask obstacleLayers;

    [Header("Looping")]
    [Tooltip("If true, the agent will go from startNode to endNode, then reverse back.")]
    public bool loopBackAndForth = false;

    private Rigidbody2D rb;
    private int currentWaypointIndex = 0;
    private bool isPaused = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Compute the path immediately if we have assigned nodes
        ComputePath();
    }

    void FixedUpdate()
    {
        // If we have no path or are paused, do nothing
        if (currentPath == null || currentPath.Count == 0 || isPaused)
            return;

        // Move toward the current waypoint
        Vector2 currentPos = transform.position;
        Vector2 targetPos = currentPath[currentWaypointIndex].transform.position;
        float distance = Vector2.Distance(currentPos, targetPos);

        // Check if we've arrived at this node
        if (distance < waypointThreshold)
        {
            // Arrived at the node: pause if necessary
            StartCoroutine(HandlePause(currentPath[currentWaypointIndex]));
        }

        // Apply steering force to move the agent
        Vector2 desiredDirection = (targetPos - currentPos).normalized;
        Vector2 desiredVelocity = desiredDirection * speed;
        Vector2 steering = (desiredVelocity - rb.linearVelocity) * steeringForce;
        rb.AddForce(steering);

        // Simple forward-based obstacle avoidance
        AvoidObstacles();
    }

    private IEnumerator HandlePause(PathNodeV3 node)
    {
        isPaused = true;
        rb.linearVelocity = Vector2.zero;  // Stop movement

        // 1) Handle time-based pause
        if (node.pauseDuration > 0f)
        {
            yield return new WaitForSeconds(node.pauseDuration);
        }

        // 2) Handle event-based pause
        if (node.waitForEvent)
        {
            // Wait until some external script sets 'node.eventTriggered = true'
            while (!node.eventTriggered)
            {
                yield return null;
            }
            // Optionally reset the flag if you want to wait each time
            node.eventTriggered = false;
        }

        // Once done, move to next node in path
        AdvanceWaypoint();

        isPaused = false;
    }

    private void AdvanceWaypoint()
    {
        currentWaypointIndex++;
        // Check if we reached the end of the path
        if (currentWaypointIndex >= currentPath.Count)
        {
            // If looping, reverse the path to go back
            if (loopBackAndForth)
            {
                currentPath.Reverse();
                currentWaypointIndex = 0; // Start at the new "first" node
            }
            else
            {
                // Otherwise, just stop at the end
                rb.linearVelocity = Vector2.zero;
                currentWaypointIndex = currentPath.Count - 1;
            }
        }
    }

    private void AvoidObstacles()
    {
        Vector2 forward = rb.linearVelocity.normalized;
        if (forward.sqrMagnitude < 0.01f)
            return; // Not really moving, skip

        RaycastHit2D hit = Physics2D.Raycast(transform.position, forward, obstacleDetectionDistance, obstacleLayers);
        if (hit.collider != null)
        {
            // Simple perpendicular "side-step" force
            Vector2 left = Quaternion.Euler(0, 0, 90) * forward;
            Vector2 right = Quaternion.Euler(0, 0, -90) * forward;

            bool leftHit = Physics2D.Raycast(transform.position, left, obstacleDetectionDistance, obstacleLayers);
            bool rightHit = Physics2D.Raycast(transform.position, right, obstacleDetectionDistance, obstacleLayers);

            Vector2 avoidDirection = rightHit ? left : right;
            rb.AddForce(avoidDirection * obstacleAvoidanceForce, ForceMode2D.Force);
        }
    }

    // Called from Start or anytime you want to recalc the BFS path
    public void ComputePath()
    {
        if (startNode == null || endNode == null)
        {
            Debug.LogWarning("Cannot compute path. Start or End node is not set.");
            return;
        }

        currentPath = SimpleBFSPathV3.FindPath(startNode, endNode);
        currentWaypointIndex = 0;
    }

    // Public method if you want to dynamically set start/end at runtime
    public void SetPath(PathNodeV3 newStart, PathNodeV3 newEnd)
    {
        startNode = newStart;
        endNode = newEnd;
        ComputePath();
    }
}