using System;
using Game.Audio;
using UnityEngine;
using Grid = Game.GridSystem.Grid;

namespace Game.Mechanics.Enemy
{
    [RequireComponent(typeof(FollowController))]
    [RequireComponent(typeof(EnemyDamageable))]
    [RequireComponent(typeof(EnemyAudioManager))]
    public class EnemyConfigurator : UnitConfigurator
    {
        #region Fields

        private FollowController followController;

        #endregion

        private void Start()
        {
            followController = GetComponent<FollowController>();
            audioManager = GetComponent<EnemyAudioManager>();


            Grid.GridChange += followController.FindPathToPlayer;
            followController.findPathEvent = Grid.FindPathToPlayer;
            movementController.newPositionEvent += followController.ChangePosition;
            followController.SetMoveDirection += direction =>
            {
                if (direction != Vector2.zero)
                {
                    movementController.Move(direction);
                    ((EnemyAudioManager)audioManager).Step(true);
                }
                else ((EnemyAudioManager)audioManager).Step(false);
            };

            ((EnemyDamageable)damageableComponent).EnemyKilled += () =>
            {
                ((EnemyAudioManager)audioManager).Die();
                animationManager.SetState(UnitState.Die);
                followController.BlockFollow();
            };
        }

        public void PlayerKilled()
        {
            ((EnemyAudioManager)audioManager).Attack();
            animationManager.SetState(UnitState.Attack);
            followController.BlockFollow();
        }
    }
}