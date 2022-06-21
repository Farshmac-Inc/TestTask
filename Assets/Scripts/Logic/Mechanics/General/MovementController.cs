using System;
using UnityEngine;
using Game.GridSystem;
using Grid = Game.GridSystem.Grid;

namespace Game.Mechanics
{
    public class MovementController : MonoBehaviour
    {
        #region Fields & Events

        [SerializeField] private float moveSpeed;
        [SerializeField] private CharacterController charController;
        [SerializeField] private GridCellType type;
        
        public Action<Vector2Int, Vector2Int, GridCellType> newPositionEvent;
        public Action<UnitState> SetState;

        private Vector2Int position;

        #endregion

        private void Start()
        {
            newPositionEvent += Grid.SetMovableElementPosition;
            var worldPosition = transform.position;
            position = new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y));
        }

        /// <summary>
        /// Sets the movement of the object.
        /// </summary>
        /// <param name="moveVector">The vector of the direction of movement.</param>
        public void Move(Vector2 moveVector)
        {
            var lastPosition = position;
            if (moveVector == Vector2.zero)
            {
                SetState?.Invoke(UnitState.Idle);
                return;
            }
            SetState?.Invoke(UnitState.Run);
            Rotate(moveVector);
            moveVector *= moveSpeed;
            var deltaPosition = new Vector3(moveVector.x, 0, moveVector.y);
            deltaPosition *= Time.deltaTime;
            charController.Move(deltaPosition);
            SetPositionInGrid();
            if(lastPosition != position) newPositionEvent?.Invoke(position, lastPosition, type);
        }

        private void Rotate(Vector2 moveVector)
        {
            moveVector = moveVector.normalized;
            float angle = 0;
            if (moveVector.x >= 0) angle = Mathf.Acos(moveVector.x) * Mathf.Rad2Deg;
            else angle = 180 - Mathf.Acos(-moveVector.x) * Mathf.Rad2Deg;
            angle = (moveVector.y <= 0 ? angle : -angle) + 90;
            transform.Rotate(0, angle - transform.rotation.eulerAngles.y, 0);
        }

        private void SetPositionInGrid()
        {
            var worldPosition = transform.position;
            position = new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.z));
        }
    }
}