using UnityEngine;
using Grid = Game.GridSystem.Grid;

namespace Game.Logic
{
    public class PlayerConfigurator : UnitConfigurator
    {
        #region Fields

        [SerializeField] private MovementController movementController;
        [SerializeField] private Bomber bomber;
        [SerializeField] private PlayerInput playerInput;

        #endregion

        private void Start()
        {
            movementController.newPositionEvent += Grid.SetMovableElementPosition;
            playerInput.MoveEvent += movementController.Move;
            playerInput.DroppingBombEvent += bomber.DropBomb;
        }
    }
}