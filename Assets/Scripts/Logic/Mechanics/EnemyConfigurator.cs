using UnityEngine;
using Grid = Game.GridSystem.Grid;

namespace Game.Logic
{
    public class EnemyConfigurator : UnitConfigurator
    {
        #region Fields

        [SerializeField] private FollowController followController;

        #endregion

        private void Start()
        {
            movementController.newPositionEvent += Grid.SetMovableElementPosition;
            Grid.GridChange += followController.FindPathToPlayer;
            followController.findPathEvent = Grid.FindPathToPlayer;
            
        }
    }
}