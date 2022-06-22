using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Audio
{
    public class EnemyAudioManager : AudioManager
    {
        [SerializeField] private AudioClip[] stepSound;
        [SerializeField] private AudioClip attackSound;
        [SerializeField] private AudioClip dieSound;

        private Mechanics.UnitState state;

        private void Start()
        {
            source.clip = stepSound[Random.Range(0, stepSound.Length - 1)];
        }

        private void Update()
        {
            switch (state)
            {
                case Mechanics.UnitState.Run:
                {
                    if(!source.isPlaying) source.Play();
                    break;
                }
                default:
                {
                    source.Pause();
                    break;
                    ;
                }
            }
        }

        internal void SetState(Mechanics.UnitState state) => this.state = state;

        internal void Die()
        {
            source.PlayOneShot(dieSound);
        }

        internal void Attack()
        {
            source.PlayOneShot(attackSound);
        }
    }
}