using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PathAgent : MonoBehaviour
{
    [Header("Path Setup")]
    public PathNode startNode;
    private PathNode endNode;  // Optional: BFS from start to end

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float stoppingDistance = 0.2f;
    public bool loopPath = false; // Whether to loop once we finish

    [Header("Stuck Reset Settings")]
    [Tooltip("If the agent doesn't reach the next node within this time, we restart the path.")]
    public float stuckResetTime = 5f;

    private Rigidbody2D rb;
    private List<PathNode> path;
    private int currentIndex = 0;
    public bool isWaiting = false;

    public bool isPausedPathing = false;

    private float timeSinceLastNode = 0f; // Tracks how long since we last reached a node

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InitPath();
    }

    private void FixedUpdate()
    {
        if (isPausedPathing)
        {
            return;
        }

        if (path == null || path.Count == 0 || isWaiting)
            return;

        // Check if we've finished the path
        if (currentIndex >= path.Count)
        {
            if (loopPath)
            {
                // Reverse the path
                path.Reverse();
                currentIndex = 0;
            }
            else
            {
                return;
            }
        }

        PathNode targetNode = path[currentIndex];
        if (targetNode != null)
        {
            // Move toward current target node
            Vector2 direction = (targetNode.transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed);

            // Check distance
            float dist = Vector2.Distance(transform.position, targetNode.transform.position);
            if (dist <= stoppingDistance)
            {
                // Reached this node
                currentIndex++;
                timeSinceLastNode = 0f; // reset stuck timer

                // Pause logic
                StartCoroutine(HandleWait(targetNode));
            }
            else
            {
                // Not reached yet; increment stuck timer
                timeSinceLastNode += Time.fixedDeltaTime;
                if (timeSinceLastNode >= stuckResetTime)
                {
                    Debug.LogWarning("Agent is stuck! Turn around...");
                    TurnAroundOnPath();
                }
            }
        }
    }

    /// <summary>
    /// Set up the path list, either via BFS (start->end) or linear chain if endNode is null.
    /// </summary>
    private void InitPath()
    {
        path = new List<PathNode>();

        if (startNode == null)
        {
            Debug.LogError("No Start Node assigned to PathAgent!");
            return;
        }

        if (endNode != null)
        {
            // BFS
            BFSUtility.ResetNodes(startNode);
            path = BFSUtility.FindPath(startNode, endNode);

            if (path == null || path.Count == 0)
            {
                Debug.LogError("No valid BFS path found from start to end.");
            }
        }
        else
        {
            // Linear chain
            PathNode current = startNode;
            while (current != null)
            {
                path.Add(current);
                if (current.connections.Count > 0)
                {
                    current = current.connections[0];
                }
                else
                {
                    current = null;
                }
            }
        }

        currentIndex = 0;
        timeSinceLastNode = 0f;
        isWaiting = false;
    }

    /// <summary>
    /// Pauses the agent if it's a TimeBased or EventBased node.
    /// </summary>
    private IEnumerator HandleWait(PathNode node)
    {
        // Zero out velocity & angular velocity so we truly stop
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // If it's a pause node, handle accordingly
        if (node.pauseType == PauseType.TimeBased && node.pauseDuration > 0f)
        {
            isWaiting = true;
            yield return new WaitForSeconds(node.pauseDuration);
            isWaiting = false;
        }
        else if (node.pauseType == PauseType.EventBased)
        {
            // We wait for this node's UnityEvent to be invoked
            //if (node.nodeEvent != null)
            //{
            //    isWaiting = true;
            //    yield return StartCoroutine(WaitForUnityEvent(node.nodeEvent));
            //    isWaiting = false;
            //}
            if (node.isTriggered)
            {
                isWaiting = false;
            }
            else
            {
                isWaiting = true;
                yield return StartCoroutine(WaitForNodeTrigger(node));
                isWaiting = false;
            }
        }

        yield break;
    }

    public void PausePathing()
    {
        isPausedPathing = true;
    }
    public void ResumePathing()
    {
        isPausedPathing = false;
    }

    /// <summary>
    /// Coroutine that yields until the given UnityEvent is invoked.
    /// </summary>
    private IEnumerator WaitForUnityEvent(UnityEvent evt)
    {
        bool eventTriggered = false;

        // Create a local listener that flips the flag to true
        UnityAction onEventFired = () => { eventTriggered = true; };
        evt.AddListener(onEventFired);

        // Wait until eventTriggered is true
        yield return new WaitUntil(() => eventTriggered);

        // Remove our listener
        evt.RemoveListener(onEventFired);
    }

    private IEnumerator WaitForNodeTrigger(PathNode node)
    {
        bool eventTriggered = false;

        // Create a local listener that flips the flag to true
        UnityAction onEventFired = () => { eventTriggered = true; };
        node.nodeEvent.AddListener(onEventFired);

        // Wait until eventTriggered is true
        yield return new WaitUntil(() => eventTriggered);

        // Remove our listener
        node.nodeEvent.RemoveListener(onEventFired);
    }

    /// <summary>
    /// Public method to restart the path from the beginning.
    /// </summary>
    public void TurnAroundOnPath()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        //inverse the indexsxc
        currentIndex = path.Count - currentIndex;
        path.Reverse();
        timeSinceLastNode = 0f;
        //InitPath();
        Debug.Log("Path restarted.");
    }
}