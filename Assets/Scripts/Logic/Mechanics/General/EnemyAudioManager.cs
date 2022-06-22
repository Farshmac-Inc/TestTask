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

        private void Start()
        {
            source.clip = stepSound[Random.Range(0, stepSound.Length - 1)];
        }

        internal void Step(bool isMove)
        {
            if(isMove) source.Play();
            else source.Pause();
        }

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