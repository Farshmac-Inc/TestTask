using System;
using UnityEngine;
using Grid = Game.GridSystem.Grid;

namespace Game.Mechanics
{
    public abstract class UnitConfigurator : MonoBehaviour
    {
        #region Fields

        [SerializeField] private protected MovementController movementController;
        [SerializeField] private protected AnimationManager animationManager;
        public Action Killed;

        #endregion
        
        private void Awake()
        {
            movementController.newPositionEvent += Grid.SetMovableElementPosition;
            movementController.SetState += animationManager.SetState;
        }
    }
}