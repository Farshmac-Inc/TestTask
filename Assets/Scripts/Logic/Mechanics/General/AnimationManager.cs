using System;
using UnityEngine;

namespace Game.Mechanics
{
    [RequireComponent(typeof(Animator))]
    public class AnimationManager : MonoBehaviour
    {
        #region Fields

        private Animation animation;
        private Animator animator;
        private static readonly int IsRun = Animator.StringToHash("IsRun");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Attack = Animator.StringToHash("Attack");

        #endregion

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Sets the animation according to the transmitted state.
        /// </summary>
        /// <param name="state">The state in which the unit is located.</param>
        public void SetState(UnitState state)
        {
            switch (state)
            {
                case UnitState.Idle:
                {
                    animator.SetBool(IsRun, false);
                    break;
                }
                case UnitState.Run:
                {
                    animator.SetBool(IsRun, true);
                    break;
                }
                case UnitState.Die:
                {
                    animator.SetTrigger(Die);
                    break;
                }
                case UnitState.Attack:
                {
                    animator.SetTrigger(Attack);
                    break;
                }
            }
        }
    }
}