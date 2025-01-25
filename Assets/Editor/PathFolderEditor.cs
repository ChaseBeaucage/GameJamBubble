using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Transform))]
public class PathFolderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // We only proceed if this transform is a "folder" with children that have PathNode components
        Transform folder = (Transform)target;

        if (GUILayout.Button("Link Child Nodes in Order"))
        {
            LinkChildrenInOrder(folder);
        }
    }

    private void LinkChildrenInOrder(Transform folder)
    {
        // Gather all PathNode components in children
        List<PathNode> nodes = new List<PathNode>();
        for (int i = 0; i < folder.childCount; i++)
        {
            Transform child = folder.GetChild(i);
            PathNode pn = child.GetComponent<PathNode>();
            if (pn != null)
            {
                nodes.Add(pn);
            }
        }

        // Sort by sibling index (which is effectively the order in the Hierarchy)
        nodes.Sort((a, b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));

        // Now link them linearly in order
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            nodes[i].connections.Clear();
            nodes[i].connections.Add(nodes[i + 1]);
        }

        // For the last node, you might want no connections
        if (nodes.Count > 0)
        {
            nodes[nodes.Count - 1].connections.Clear();
        }

        Debug.Log("Linked " + nodes.Count + " nodes in order!");
    }
}
