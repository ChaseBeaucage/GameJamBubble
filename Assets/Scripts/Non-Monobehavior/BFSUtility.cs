using System.Collections.Generic;
using UnityEngine;

public static class BFSUtility
{
    public static List<PathNode> FindPath(PathNode start, PathNode end)
    {
        // Edge case
        if (start == null || end == null) return null;
        if (start == end) return new List<PathNode>() { start };

        // Reset visited
        Queue<PathNode> queue = new Queue<PathNode>();
        queue.Enqueue(start);

        // Mark visited
        start.visited = true;
        start.parent = null;

        bool pathFound = false;

        while (queue.Count > 0)
        {
            PathNode current = queue.Dequeue();

            foreach (PathNode neighbor in current.connections)
            {
                if (!neighbor.visited)
                {
                    neighbor.visited = true;
                    neighbor.parent = current;
                    queue.Enqueue(neighbor);

                    if (neighbor == end)
                    {
                        pathFound = true;
                        break;
                    }
                }
            }
            if (pathFound) break;
        }

        // Reconstruct path if found
        if (pathFound)
        {
            List<PathNode> path = new List<PathNode>();
            PathNode cur = end;
            while (cur != null)
            {
                path.Add(cur);
                cur = cur.parent;
            }
            path.Reverse();
            return path;
        }

        // If no path found
        return null;
    }

    public static void ResetNodes(PathNode node)
    {
        // BFS can be complicated if you have multiple paths. 
        // This is one approach to reset visited flags in the entire graph. 
        // Alternatively, you could keep track of visited nodes during BFS and reset them individually.
        if (node == null) return;
        Queue<PathNode> queue = new Queue<PathNode>();
        HashSet<PathNode> visitedSet = new HashSet<PathNode>();

        queue.Enqueue(node);
        visitedSet.Add(node);

        while (queue.Count > 0)
        {
            PathNode current = queue.Dequeue();
            current.visited = false;
            current.parent = null;
            foreach (var neighbor in current.connections)
            {
                if (!visitedSet.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visitedSet.Add(neighbor);
                }
            }
        }
    }
}
