using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PathFinder
{
    public class PathNode
    {
        #region Fields & Property
        public int FullPathLength => PathLengthFromStart + pathLengthToTarget;
        public Vector2Int Position;
        public int PathLengthFromStart;
        public PathNode PreviousPathNode;
        public PathNode[] Neighbours;
        public bool IsAvalaible;
        private int pathLengthToTarget;

        #endregion

        /// <summary>
        /// Constructor of the class. 
        /// </summary>
        /// <param name="position">The position of the path node in the matrix grid.</param>
        /// <param name="pathLengthFromStart">Distance from the node to the start.</param>
        /// <param name="previousPathNode">Link to the previous node.</param>
        /// <param name="pathLengthToTarget">Distance from the current node to the target.</param>
        /// <param name="isAvalaible">Ability to navigate through this node.</param>
        public PathNode(Vector2Int position, int pathLengthFromStart, PathNode previousPathNode, int pathLengthToTarget,
            bool isAvalaible)
        {
            Position = position;
            PathLengthFromStart = pathLengthFromStart;
            PreviousPathNode = previousPathNode;
            Neighbours = null;
            this.pathLengthToTarget = pathLengthToTarget;
            IsAvalaible = isAvalaible;
        }
    }
}