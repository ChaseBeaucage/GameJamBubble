using UnityEngine;
using System.Collections.Generic;

public enum PauseType
{
    None,
    TimeBased,
    EventBased
}

public class PathNode : MonoBehaviour
{
    [Tooltip("A list of connected nodes (if you have branching paths). If it's strictly linear, it will just have 1 next node.")]
    public List<PathNode> connections = new List<PathNode>();

    [Header("Pause Settings")]
    public PauseType pauseType = PauseType.None;
    [Tooltip("Pause duration if this node is TimeBased")]
    public float pauseDuration = 0f;
    [Tooltip("Event name or ID if this node is EventBased. (Implementation for event triggers not shown here.)")]
    public string pauseEvent = "";

    // You can use this to help BFS find which node was visited
    [HideInInspector] public bool visited = false;

    // For optional BFS "parent" reference in path reconstruction
    [HideInInspector] public PathNode parent = null;

    private void OnDrawGizmos()
    {
        // 1) Draw the node
        //    Increase size if we have a time-based pause; the bigger the pause, the bigger the node radius.
        float baseRadius = 0.2f;
        float extraRadius = (pauseType == PauseType.TimeBased) ? pauseDuration * 0.05f : 0f;
        float totalRadius = baseRadius + extraRadius;

        // 2) Color the node differently if it’s a pause node
        switch (pauseType)
        {
            case PauseType.TimeBased:
                Gizmos.color = Color.yellow;
                break;
            case PauseType.EventBased:
                Gizmos.color = Color.magenta;
                break;
            default:
                Gizmos.color = Color.green;
                break;
        }

        Gizmos.DrawSphere(transform.position, totalRadius);

        // 3) Draw lines to each connected node
        Gizmos.color = Color.cyan;
        foreach (var conn in connections)
        {
            if (conn != null)
            {
                Gizmos.DrawLine(transform.position, conn.transform.position);
            }
        }
    }
}
