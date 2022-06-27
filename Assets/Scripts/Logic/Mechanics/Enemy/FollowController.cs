using System;
using UnityEngine;

namespace Game.Mechanics.Enemy
{
    public class FollowController : MonoBehaviour
    {
        #region Fields

        private Vector2Int[] path;
        private int nodeNumber;
        private Vector2Int currentPosition;
        private bool isEnemyDie;

        public Func<Vector2Int, Vector2Int[]> findPathEvent;
        public Action<Vector2> SetMoveDirection;

        #endregion

        private void Start()
        {
            var position = transform.position;
            currentPosition = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
            FindPathToPlayer();
        }

        private void Update()
        {
            if (isEnemyDie) return;
            if (path == null) FindPathToPlayer();
            else
            {
                if (nodeNumber < path.Length - 1)
                {
                    var newDirection = path[nodeNumber + 1] - currentPosition;
                    SetMoveDirection?.Invoke(new Vector2(newDirection.x, newDirection.y).normalized);
                }

                else if (nodeNumber == path.Length - 1)
                {
                    var newDirection = path[nodeNumber] - currentPosition;
                    SetMoveDirection?.Invoke(new Vector2(newDirection.x, newDirection.y).normalized);
                }
            }
        }

        /// <summary>
        /// A method that asks the grid for a route to the player.
        /// </summary>
        public void FindPathToPlayer()
        {
            path = findPathEvent?.Invoke(currentPosition);
            nodeNumber = 1;
        }

        /// <summary>
        /// The method that processes the player's position change
        /// event checks whether the unit has reached the next route point.
        /// </summary>
        /// <param name="lastPos">Previous position of the object in the grid.</param>
        /// <param name="newPos">The new position of the object in the grid.</param>
        /// <param name="type">The type of the object.</param>
        public void ChangePosition(Vector2Int newPos, int movableObjectID)
        {
            currentPosition = newPos;
            if (currentPosition == path[nodeNumber]) nodeNumber++;
        }

        /// <summary>
        /// A method that disables the following system.
        /// </summary>
        public void BlockFollow()
        {
            isEnemyDie = true;
        }

        private void OnDrawGizmos()
        {
            if (path != null)
            {
                Gizmos.color = Color.red;
                for (int i = 1; i < path.Length; i++)
                {
                    var last = path[i - 1];
                    var targ = path[i];
                    Gizmos.DrawLine(new Vector3(last.x, 0, last.y), new Vector3(targ.x, 0, targ.y));
                    Gizmos.DrawSphere(new Vector3(last.x, 0, last.y), 0.1f);
                }
            }
        }
    }
}