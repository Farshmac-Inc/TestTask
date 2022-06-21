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
            Grid.GridChange += followController.FindPathToPlayer;
            followController.findPathEvent = Grid.FindPathToPlayer;
        }
    }
}