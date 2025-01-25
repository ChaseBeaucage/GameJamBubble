using System.Collections.Generic;
using UnityEngine;

public static class SimpleBFSPathV3
{
    public static List<PathNodeV3> FindPath(PathNodeV3 start, PathNodeV3 end)
    {
        if (start == null || end == null)
        {
            Debug.LogWarning("Start or End node is null. BFS cannot run.");
            return null;
        }

        Queue<PathNodeV3> queue = new Queue<PathNodeV3>();
        Dictionary<PathNodeV3, PathNodeV3> cameFrom = new Dictionary<PathNodeV3, PathNodeV3>();
        HashSet<PathNodeV3> visited = new HashSet<PathNodeV3>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            PathNodeV3 current = queue.Dequeue();
            if (current == end)
            {
                // Found a path to 'end'
                return ReconstructPath(cameFrom, end);
            }

            foreach (PathNodeV3 neighbor in current.neighbors)
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    cameFrom[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        // If we exhaust the queue without finding 'end', there's no route
        Debug.LogWarning("No path found between " + start.name + " and " + end.name);
        return null;
    }

    private static List<PathNodeV3> ReconstructPath(Dictionary<PathNodeV3, PathNodeV3> cameFrom, PathNodeV3 end)
    {
        // Reconstructs the path by backtracking from the end node
        List<PathNodeV3> path = new List<PathNodeV3>();
        PathNodeV3 current = end;
        while (cameFrom.ContainsKey(current))
        {
            path.Insert(0, current);
            current = cameFrom[current];
        }
        // Insert the start node
        path.Insert(0, current);

        return path;
    }
}
