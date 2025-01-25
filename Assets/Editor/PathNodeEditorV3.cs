//#if UNITY_EDITOR
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(PathNodeV3))]
//public class PathNodeEditorV3 : Editor
//{
//    //public override void OnInspectorGUI()
//    //{
//    //    DrawDefaultInspector();

//    //    PathNodeV3 node = (PathNodeV3)target;

//    //    if (GUILayout.Button("Auto Link Neighbors"))
//    //    {
//    //        AutoLinkNeighbors(node);
//    //    }
//    //}

//    //private void AutoLinkNeighbors(PathNodeV3 node)
//    //{
//    //    float linkRadius = 2f; // Adjust as desired
//    //    PathNodeV3[] allNodes = FindObjectsByType<PathNodeV3>(FindObjectsSortMode.None);

//    //    node.neighbors.Clear();
//    //    foreach (PathNodeV3 other in allNodes)
//    //    {
//    //        if (other != node)
//    //        {
//    //            float dist = Vector2.Distance(node.transform.position, other.transform.position);
//    //            if (dist <= linkRadius)
//    //            {
//    //                node.neighbors.Add(other);
//    //            }
//    //        }
//    //    }

//    //    EditorUtility.SetDirty(node);
//    //    Debug.Log("Auto-linked neighbors for " + node.name);
//    //}
//}
//#endif