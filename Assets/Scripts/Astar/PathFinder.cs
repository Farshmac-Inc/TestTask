using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.PathFinder
{
    public static class PathFinder
    {
        public static Vector2Int[] FindPath(Vector2Int startPos, Vector2Int targetPos,
            GridCell[,] nodes)
        {
            var mapSize = new Vector2Int(nodes.GetLength(0), nodes.GetLength(1));
            var nodesArray = MatrixInArrayPathNodes(nodes, targetPos, mapSize);
            var availableNodes = new List<PathNode>();
            var exploredNodes = new List<PathNode>();
            
            foreach (var node in nodesArray)
                if (!node.IsAvalaible)
                    exploredNodes.Add(node);

            PathNode startNode = new PathNode(startPos, 0, null,
                CalculateDistance(startPos, targetPos), true);
            startNode.SetNeighbors(
                GetNeighbors(startPos.x * nodes.GetLength(1) + startPos.y,
                nodesArray, nodes.GetLength(1), mapSize)
                );
            availableNodes.Add(startNode);
            
            while (availableNodes.Count > 0)
            {
                var a = availableNodes.OrderBy(node => node.FullPathLength);
                var currentNode = a.First();
                
                if (currentNode.Position == targetPos)
                {
                    return GetPathForNode(currentNode);
                }

                availableNodes.Remove(currentNode);
                exploredNodes.Add(currentNode);
                foreach (var neighbour in currentNode.Neighbours)
                {
                    if (exploredNodes.Count(node => node.Position == neighbour.Position) > 0) continue;
                    var openNode = availableNodes.FirstOrDefault(node => node.Position == neighbour.Position);
                    if (openNode == null)
                    {
                        availableNodes.Add(neighbour);
                    }
                    else if (openNode.PathLengthFromStart > neighbour.PathLengthFromStart)
                    {
                        neighbour.PreviousPathNode = currentNode;
                        neighbour.PathLengthFromStart = neighbour.PathLengthFromStart;
                    }
                }
            }
            return null;
        }


        private static PathNode[] MatrixInArrayPathNodes(GridCell[,] matrix, Vector2Int target, Vector2Int mapSize)
        {
            int width = matrix.GetLength(1);
            PathNode[] array = new PathNode[matrix.GetLength(0) * width];
            for (var i = 0; i < matrix.GetLength(0); i++)
            for (var j = 0; j < width; j++)
            {
                var pathNodeCell = new PathNode(new Vector2Int(i, j), 0, null,
                    CalculateDistance(new Vector2Int(i, j), target), matrix[i, j].isAvailableForMove);
                array[i * width + j] = pathNodeCell;
            }

            for (var i = 0; i < array.Length; i++)
            {
                array[i].SetNeighbors(GetNeighbors(i, array, width, mapSize));
            }

            return array;
        }


        private static PathNode[] GetNeighbors(int i, PathNode[] nodes, int width, Vector2Int mapSize)
        {
            int x = i / width, y = i % width;
            List<PathNode> result = new List<PathNode>();
            if (x > 0) result.Add(nodes[(x - 1) * width + y]);
            if (x < mapSize.x - 1) result.Add(nodes[(x + 1) * width + y]);
            if (y > 0) result.Add(nodes[x * width + y - 1]);
            if (y < mapSize.y - 1) result.Add(nodes[x * width + y + 1]);
            return result.ToArray();
        }

        private static int CalculateDistance(Vector2Int point1, Vector2Int point2)
        {
            return Mathf.Abs(point1.x - point2.x) + Mathf.Abs(point1.y - point2.y);
        }

        private static Vector2Int[] GetPathForNode(PathNode pathNode)
        {
            var result = new List<Vector2Int>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.PreviousPathNode;
            }

            result.Reverse();
            return result.ToArray();
        }
    }
}