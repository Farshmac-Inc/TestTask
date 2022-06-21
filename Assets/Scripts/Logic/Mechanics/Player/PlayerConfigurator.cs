using UnityEngine;
using Grid = Game.GridSystem.Grid;

namespace Game.Mechanics
{
    public class PlayerConfigurator : UnitConfigurator
    {
        #region Fields

        [SerializeField] private Bomber bomber;
        [SerializeField] private PlayerInput playerInput;

        #endregion

        private void Start()
        {
            playerInput.MoveEvent += movementController.Move;
            playerInput.DroppingBombEvent += bomber.DropBomb;
        }
    }
}