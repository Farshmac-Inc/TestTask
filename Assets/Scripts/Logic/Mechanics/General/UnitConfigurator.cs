using System;
using UnityEngine;
using Grid = Game.GridSystem.Grid;
using Object = UnityEngine.Object;

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
        public Action Killed;

        #endregion
        
        private void Awake()
        {
            movementController = GetComponent<MovementController>();
            animationManager = GetComponent<AnimationManager>();
            damageableComponent = GetComponent<IDamageable>();
            movementController.newPositionEvent += Grid.SetMovableElementPosition;
            movementController.SetState += animationManager.SetState;
            Killed += damageableComponent.GetDamage;
        }
    }

    internal class TypeRestrictionAttribute : Attribute
    {
        public TypeRestrictionAttribute(Type type)
        {
            throw new NotImplementedException();
        }
    }
}