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

        public void FindPathToPlayer()
        {
            path = findPathEvent?.Invoke(currentPosition);           
            nodeNumber = 1; 
        }

        public void ChangePosition(Vector2Int last, Vector2Int newPos, GridSystem.GridCellType type)
        {
            currentPosition = newPos;
            if (currentPosition == path[nodeNumber]) nodeNumber++;
        }

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