using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class FollowController : MonoBehaviour
    {
        private MovementController moveControl;
        private Vector2Int[] path;
        private int nodeNumber = 0;
        private Vector2Int currentPosition;

        private void Start()
        {
            moveControl = GetComponent<MovementController>();
            moveControl.newPositionEvent += ChangePosition;
            var position = transform.position;
            currentPosition = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
            Grid.GridChange += FindPathToPlayer;
            FindPathToPlayer();
        }

        private void ChangePosition(Vector2Int last, Vector2Int newPos, GridCellType type)
        {
            currentPosition = newPos;
            if (currentPosition == path[nodeNumber]) nodeNumber++;
        }

        private void Update()
        {
            if (path == null) FindPathToPlayer();
            else
            {
                Debug.Log($"curPos: {currentPosition} | curPoint: {path[nodeNumber]} | targetPoint: {path[nodeNumber+1]} | {currentPosition == path[nodeNumber]} ");
                if(nodeNumber < path.Length) moveControl.Move(path[nodeNumber+1] - currentPosition);
            }
        }

        public void FindPathToPlayer()
        {
            path = Grid.FindPathToPlayer(currentPosition);            
            nodeNumber = 1;
            
            
            
            if(path == null) return;
            string PATHSTR = null;
            foreach(var node in path) PATHSTR += $"{node} | ";
            Debug.LogError(PATHSTR);
            
            
            
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