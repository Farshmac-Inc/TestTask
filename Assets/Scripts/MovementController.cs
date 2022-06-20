using System;
using UnityEngine;

namespace Game
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private CharacterController charController;
        [SerializeField] private Animation animation;
        [SerializeField] private GridCellType type;
        private Action<Vector2Int, Vector2Int, GridCellType> newPositionEvent;
        private Vector2Int position;
        

        private void Start()
        {
            newPositionEvent += Grid.SetMovableElementPosition;
            var worldPosition = transform.position;
            position = new Vector2Int(Mathf.FloorToInt(worldPosition.x), Mathf.FloorToInt(worldPosition.y));
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
                animation.Play("Idle");
                return;
            }

            animation.Play("Run");
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