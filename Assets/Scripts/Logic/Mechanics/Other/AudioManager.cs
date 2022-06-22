using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource ambientSource;
        [SerializeField] private AudioSource playerDiedSource;
        [SerializeField] private AudioSource enemyDiedSource;
        [SerializeField] private AudioSource explosionBombSource;

        private void Start()
        {
            ambientSource.Play();
        }

        public void SoundPlay(SoundType type)
        {
            switch (type)
            {
                case SoundType.EnemyDie:
                    enemyDiedSource.Play();
                    break;
                case SoundType.BombExplosion:
                    explosionBombSource.Play();
                    break;
                case SoundType.PlayerDie:
                    playerDiedSource.Play();
                    break;
            }
        }
    }

    public enum SoundType
    {
        PlayerDie,
        EnemyDie,
        BombExplosion
    }
    
}