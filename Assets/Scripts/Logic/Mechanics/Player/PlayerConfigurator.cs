using UnityEngine;
using Grid = Game.GridSystem.Grid;

namespace Game.Mechanics.PLayer
{
    [RequireComponent(typeof(Bomber))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerDamageable))]
    public class PlayerConfigurator : UnitConfigurator
    {
        #region Fields

        private Bomber bomber;
        private PlayerInput playerInput;

        #endregion

        private void Start()
        {
            bomber = GetComponent<Bomber>();
            playerInput = GetComponent<PlayerInput>();
            playerInput.MoveEvent += movementController.Move;
            playerInput.DroppingBombEvent += bomber.DropBomb;
        }
    }
}