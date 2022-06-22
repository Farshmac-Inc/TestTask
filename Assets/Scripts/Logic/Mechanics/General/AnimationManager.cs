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
                    animator.SetBool("IsRun", false);
                    break;
                }
                case UnitState.Run:
                {
                    animator.SetBool("IsRun", true);
                    break;
                }
                case UnitState.Die:
                {
                    animator.SetTrigger("Die");
                    break;
                }
            }
        }
    }
}