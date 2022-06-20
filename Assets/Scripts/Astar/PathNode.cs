using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PathFinder
{
    public class PathNode
    {
        public Vector2Int Position;
        public int PathLengthFromStart;
        public PathNode PreviousPathNode;
        public PathNode[] Neighbours;
        public int PathLengthToTarget;
        public bool IsAvalaible = true;

        public int FullPathLength
        {
            get { return PathLengthFromStart + PathLengthToTarget; }
        }

        public PathNode(Vector2Int position, int pathLengthFromStart, PathNode previousPathNode, int pathLengthToTarget,
            bool isAvalaible)
        {
            Position = position;
            PathLengthFromStart = pathLengthFromStart;
            PreviousPathNode = previousPathNode;
            Neighbours = null;
            PathLengthToTarget = pathLengthToTarget;
            IsAvalaible = isAvalaible;
        }

        public void SetNeighbors(PathNode[] neighbors) => Neighbours = neighbors;
    }
}