using System.Collections.Generic;
using UnityEngine;

public static class PathfindingV3
{
    /*public static List<PathNodeV3> FindPath(PathNodeV3 start, PathNodeV3 goal)
    {
        List<PathNodeV3> openSet = new List<PathNodeV3>() { start };
        Dictionary<PathNodeV3, PathNodeV3> cameFrom = new Dictionary<PathNodeV3, PathNodeV3>();
        Dictionary<PathNodeV3, float> gScore = new Dictionary<PathNodeV3, float>();
        Dictionary<PathNodeV3, float> fScore = new Dictionary<PathNodeV3, float>();

        // Initialize dictionaries
        foreach (PathNodeV3 node in GameObject.FindObjectsByType<PathNodeV3>(FindObjectsSortMode.None))
        {
            gScore[node] = Mathf.Infinity;
            fScore[node] = Mathf.Infinity;
        }
        gScore[start] = 0f;
        fScore[start] = Heuristic(start, goal);

        while (openSet.Count > 0)
        {
            PathNodeV3 current = GetLowestFScore(openSet, fScore);
            if (current == goal)
            {
                return ReconstructPath(cameFrom, current);
            }

            openSet.Remove(current);

            foreach (PathNodeV3 neighbor in current.neighbors)
            {
                float tentativeGScore = gScore[current] + Vector2.Distance(current.transform.position, neighbor.transform.position) * neighbor.movementCost;
                if (tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, goal);
                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        // No path found
        return null;
    }

    private static float Heuristic(PathNodeV3 a, PathNodeV3 b)
    {
        // Straight-line distance
        return Vector2.Distance(a.transform.position, b.transform.position);
    }

    private static PathNodeV3 GetLowestFScore(List<PathNodeV3> openSet, Dictionary<PathNodeV3, float> fScore)
    {
        PathNodeV3 lowest = openSet[0];
        float minF = fScore[lowest];

        for (int i = 1; i < openSet.Count; i++)
        {
            float score = fScore[openSet[i]];
            if (score < minF)
            {
                lowest = openSet[i];
                minF = score;
            }
        }
        return lowest;
    }

    private static List<PathNodeV3> ReconstructPath(Dictionary<PathNodeV3, PathNodeV3> cameFrom, PathNodeV3 current)
    {
        List<PathNodeV3> totalPath = new List<PathNodeV3>() { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Insert(0, current);
        }
        return totalPath;
    }*/
}
