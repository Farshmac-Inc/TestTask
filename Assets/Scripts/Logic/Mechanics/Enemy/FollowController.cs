using System;
using UnityEngine;

namespace Game.Mechanics
{
    public class FollowController : MonoBehaviour
    {
        #region Fields

        private MovementController moveControl;
        private Vector2Int[] path;
        private int nodeNumber = 0;
        private Vector2Int currentPosition;

        public Func<Vector2Int, Vector2Int[]> findPathEvent; 

        #endregion
        

        private void Start()
        {
            moveControl = GetComponent<MovementController>();
            moveControl.newPositionEvent += ChangePosition;
            var position = transform.position;
            currentPosition = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
            FindPathToPlayer();
        }

        private void Update()
        {
            if (path == null) FindPathToPlayer();
            else
            {
                if(nodeNumber < path.Length-1) moveControl.Move(path[nodeNumber+1] - currentPosition);
                else if(nodeNumber == path.Length-1) moveControl.Move(path[nodeNumber] - currentPosition);
            }
            
        }

        public void FindPathToPlayer()
        {
            path = findPathEvent?.Invoke(currentPosition);           
            nodeNumber = 1; 
        }

        private void ChangePosition(Vector2Int last, Vector2Int newPos, Game.GridSystem.GridCellType type)
        {
            currentPosition = newPos;
            if (currentPosition == path[nodeNumber]) nodeNumber++;
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