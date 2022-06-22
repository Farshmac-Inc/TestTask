using System;
using UnityEngine;
using Grid = Game.GridSystem.Grid;

namespace Game.Mechanics.Enemy
{
    [RequireComponent(typeof(FollowController))]
    [RequireComponent(typeof(EnemyDamageable))]
    public class EnemyConfigurator : UnitConfigurator
    {
        #region Fields

        private FollowController followController;

        #endregion

        private void Start()
        {
            followController = GetComponent<FollowController>();
            followController.SetMoveDirection += movementController.Move;
            Grid.GridChange += followController.FindPathToPlayer;
            followController.findPathEvent = Grid.FindPathToPlayer;
            movementController.newPositionEvent += followController.ChangePosition;
            ((EnemyConfigurator)damageableComponent).Killed += 
                () =>
                {
                    animationManager.SetState(UnitState.Die);
                    followController.BlockFollow();
                };
        }
    }
}