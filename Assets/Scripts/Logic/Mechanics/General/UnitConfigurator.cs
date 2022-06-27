using System;
using Game.Audio;
using UnityEngine;
using Grid = Game.GridSystem.Grid;

namespace Game.Mechanics
{
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(AnimationManager))]
    public abstract class UnitConfigurator : MonoBehaviour
    {
        #region Fields

        private protected MovementController movementController;
        private protected AnimationManager animationManager;
        private protected IDamageable damageableComponent;
        private protected AudioManager audioManager;
        public Action Killed;
        public Action<int, Vector2Int> Setup;
        public Action<int> SetNewID;

        #endregion
        
        private void Awake()
        {
            movementController = GetComponent<MovementController>();
            animationManager = GetComponent<AnimationManager>();
            damageableComponent = GetComponent<IDamageable>();
            movementController.newPositionEvent += Grid.SetMovableElementPosition;
            movementController.SetState += animationManager.SetState;
            Killed += damageableComponent.GetDamage;
            Setup += movementController.SetNewID;
            SetNewID += movementController.SetNewID;
        }

        private void OnDestroy()
        {
            movementController.newPositionEvent -= Grid.SetMovableElementPosition;
            movementController.SetState -= animationManager.SetState;
            Killed -= damageableComponent.GetDamage;
            Setup -= movementController.SetNewID;
            SetNewID -= movementController.SetNewID;
        }
    }
}