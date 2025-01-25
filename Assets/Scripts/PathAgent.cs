using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class PathAgent : MonoBehaviour
{
    public PathNode startNode;
    public PathNode endNode;  // If you specifically want an end
    public float moveSpeed = 5f;
    public float stoppingDistance = 0.2f;

    private Rigidbody2D rb;
    private List<PathNode> path;
    private int currentIndex = 0;
    private bool isPaused = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Optional BFS to get the path from start to end:
        if (endNode != null)
        {
            // Reset the BFS data
            BFSUtility.ResetNodes(startNode);
            path = BFSUtility.FindPath(startNode, endNode);
        }
        else
        {
            // If it’s strictly linear (e.g., from startNode through all connected nodes),
            // we can manually gather them in sequence. For example:
            path = new List<PathNode>();
            PathNode current = startNode;
            while (current != null && current.connections.Count > 0)
            {
                path.Add(current);
                current = current.connections[0];
            }
            // Add the last node if it exists
            if (current != null) path.Add(current);
        }

        currentIndex = 0;
    }

    private void FixedUpdate()
    {
        if (path == null || path.Count == 0 || isPaused) return;
        if (currentIndex >= path.Count) return;

        // Move toward current target node
        PathNode targetNode = path[currentIndex];
        Vector2 direction = (targetNode.transform.position - transform.position).normalized;
        rb.AddForce(direction * moveSpeed);

        // Check distance
        float dist = Vector2.Distance(transform.position, targetNode.transform.position);
        if (dist <= stoppingDistance)
        {
            StartCoroutine(HandlePause(targetNode));
            currentIndex++;
        }
    }

    private IEnumerator HandlePause(PathNode node)
    {
        // We reached a node. Check if it’s a pause node.
        if (node.pauseType == PauseType.TimeBased && node.pauseDuration > 0f)
        {
            isPaused = true;
            yield return new WaitForSeconds(node.pauseDuration);
            isPaused = false;
        }
        else if (node.pauseType == PauseType.EventBased && !string.IsNullOrEmpty(node.pauseEvent))
        {
            // Example approach:
            // isPaused = true;
            // yield return new WaitUntil(() => SomeEventManager.CheckEventTriggered(node.pauseEvent));
            // isPaused = false;
            //
            // Implementation depends on your event system. 
            // For demonstration, we’ll just do a quick wait:
            isPaused = true;
            Debug.Log($"Waiting on event: {node.pauseEvent} (placeholder).");
            // Placeholder wait:
            yield return new WaitForSeconds(2f);
            isPaused = false;
        }
        // else: no pause

        yield break;
    }
}
