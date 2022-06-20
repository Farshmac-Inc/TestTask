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
            currentPosition = Vector2Int.RoundToInt(new Vector2(position.x, position.z));
            Grid.GridChange += RefindPathToPlayer;
            RefindPathToPlayer();
        }

        private void ChangePosition(Vector2Int last, Vector2Int newPos, GridCellType type)
        {
            currentPosition = newPos;
            if (currentPosition == path[nodeNumber]) nodeNumber++;
        }

        private void Update()
        {
            if (path == null) RefindPathToPlayer();
            else
            {
                if(nodeNumber < path.Length) moveControl.Move(path[nodeNumber] - currentPosition);
            }
        }

        public void RefindPathToPlayer()
        {
            path = Grid.FindPathToPlayer(currentPosition);
            nodeNumber = 0;
        }
    }
}