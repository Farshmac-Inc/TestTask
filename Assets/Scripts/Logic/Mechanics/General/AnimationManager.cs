using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Mechanics
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] private string RunAnimationName;
        [SerializeField] private string IdleAnimationName;
        [SerializeField] private string DieAnimationName;
        [SerializeField] private Animation animation;

        public void SetState(UnitState state)
        {
            switch (state)
            {
                case UnitState.Idle:
                {
                    PlayAnimaton(IdleAnimationName, state);
                    
                    break;
                }
                case UnitState.Run:
                {
                    PlayAnimaton(RunAnimationName, state);
                    break;
                }
                case UnitState.Die:
                {
                    PlayAnimaton(DieAnimationName, state);
                    break;
                }
            }
        }

        private void PlayAnimaton(string name, UnitState state)
        {
            try
            {
                animation.Play(name);
            }
            catch (Exception e)
            {
                Debug.LogError($"Specify the correct animation name for the state {state}");
                throw;
            }
        }
    }
}