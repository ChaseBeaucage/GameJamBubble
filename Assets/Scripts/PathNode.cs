using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public enum PauseType
{
    None,
    TimeBased,
    EventBased
}

public class PathNode : MonoBehaviour
{
    [Tooltip("Connections to next nodes (can be multiple if you have branching).")]
    public List<PathNode> connections = new List<PathNode>();

    [Header("Pause Settings")]
    public PauseType pauseType = PauseType.None;

    [Tooltip("Pause duration if this node is TimeBased.")]
    public float pauseDuration = 0f;

    [Header("Event-Based Pause")]
    [Tooltip("If pauseType = EventBased, the path agent will wait until this event is invoked.")]
    public UnityEvent nodeEvent;
    public bool isTriggered = false;

    // For BFS traversal
    [HideInInspector] public bool visited = false;
    [HideInInspector] public PathNode parent = null;

    public void ResetNode()
    {
        visited = false;
        parent = null;
    }

    public void TriggerEvent()
    {
        if (!isTriggered) { 
        isTriggered = true;
        nodeEvent.Invoke();
            //pauseType = PauseType.None;
        }
    }

    private void OnDrawGizmos()
    {
        // Gizmo logic for visualizing the node in the Editor
        float baseRadius = 0.2f;
        float extraRadius = (pauseType == PauseType.TimeBased) ? pauseDuration * 0.05f : 0f;
        float totalRadius = baseRadius + extraRadius;

        // Color based on pause type
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

        // Draw lines to each connected node
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