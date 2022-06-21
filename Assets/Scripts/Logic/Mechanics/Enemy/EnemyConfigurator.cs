using System;
using UnityEngine;
using Grid = Game.GridSystem.Grid;

namespace Game.Mechanics
{
    public class EnemyConfigurator : UnitConfigurator
    {
        #region Fields

        [SerializeField] private FollowController followController;

        #endregion

        private void Start()
        {
            Grid.GridChange += followController.FindPathToPlayer;
            followController.findPathEvent = Grid.FindPathToPlayer;
            
        }
    }
}