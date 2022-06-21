using System;
using UnityEngine;

namespace Game.Mechanics
{
    [RequireComponent(typeof(Animation))]
    public class AnimationManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private string RunAnimationName;
        [SerializeField] private string IdleAnimationName;
        [SerializeField] private string DieAnimationName;

        private Animation animation;

        #endregion

        private void Start()
        {
            animation = GetComponent<Animation>();
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
                    PlayAnimation(IdleAnimationName, state);
                    break;
                }
                case UnitState.Run:
                {
                    PlayAnimation(RunAnimationName, state);
                    break;
                }
                case UnitState.Die:
                {
                    PlayAnimation(DieAnimationName, state);
                    break;
                }
            }
        }

        private void PlayAnimation(string name, UnitState state)
        {
            try
            {
                animation.Play(name);
            }
            catch (Exception e)
            {
                //Debug.LogError($"Specify the correct animation name for the state {state}");
                throw;
            }
        }
    }
}