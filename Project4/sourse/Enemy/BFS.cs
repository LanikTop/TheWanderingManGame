using System.Collections.Generic;
using Microsoft.Xna.Framework;

public static class BreadthFirstSearch
{
    public static List<Vector2> FindPath(int[,] grid, Vector2 start, Vector2 goal)
    {
        if (start.X < 0 || start.Y < 0 || goal.X < 0 || goal.Y < 0 ||
            start.X >= grid.GetLength(1) || start.Y >= grid.GetLength(0) ||
            goal.X >= grid.GetLength(1) || goal.Y >= grid.GetLength(0))
        {
            return null;
        }

        if (start == goal) return new List<Vector2>();
        if (grid[(int)goal.Y, (int)goal.X] == 1) return null;

        Queue<Vector2> queue = new Queue<Vector2>();
        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();
        HashSet<Vector2> visited = new HashSet<Vector2>();

        queue.Enqueue(start);
        visited.Add(start);

        Vector2[] directions = {
                new Vector2(0, 1),
                new Vector2(1, 0),
                new Vector2(0, -1),
                new Vector2(-1, 0)
            };

        while (queue.Count > 0)
        {
            Vector2 current = queue.Dequeue();

            if (current == goal)
            {
                return ReconstructPath(cameFrom, current);
            }

            foreach (var dir in directions)
            {
                Vector2 neighbor = new Vector2((int)(current.X + dir.X), (int)(current.Y + dir.Y));

                if (neighbor.X >= 0 && neighbor.Y >= 0 &&
                    neighbor.X < grid.GetLength(1) && neighbor.Y < grid.GetLength(0) &&
                    grid[(int)neighbor.Y, (int)neighbor.X] == 0 &&
                    !visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    cameFrom[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }
        return null;
    }

    private static List<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current)
    {
        List<Vector2> path = new List<Vector2>();
        path.Add(current);

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}