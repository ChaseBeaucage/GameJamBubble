using UnityEngine;
using System.Collections.Generic;

public class PathNodeV3 : MonoBehaviour
{
    [Header("Neighboring Nodes")]
    public List<PathNodeV3> neighbors = new List<PathNodeV3>();

    [Header("Pause Settings")]
    [Tooltip("Time in seconds to pause at this node. If 0, no time-based pause.")]
    public float pauseDuration = 0f;

    [Tooltip("If true, must wait for an external event before continuing.")]
    public bool waitForEvent = false;

    [HideInInspector]
    public bool eventTriggered = false;
    // Another script can set this to true when it's time to move on.

    private void OnDrawGizmos()
    {
        // Color node red if it has a pause or is waiting for an event.
        if (pauseDuration > 0f || waitForEvent)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.yellow;
        }

        // Scale the gizmo based on pauseDuration
        float baseSize = 0.1f;
        float sizeModifier = pauseDuration * 0.05f;
        float finalSize = baseSize + sizeModifier;

        Gizmos.DrawSphere(transform.position, finalSize);

        // Draw lines to neighbors
        Gizmos.color = Color.cyan;
        foreach (PathNodeV3 neighbor in neighbors)
        {
            if (neighbor != null)
            {
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }
    }
}
