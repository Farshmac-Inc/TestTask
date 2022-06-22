using UnityEngine;

namespace Game.Audio
{
    public class BombAudioManager : AudioManager
    {
        [SerializeField] private AudioClip explosion;

        internal void Explosion()
        {
            source.PlayOneShot(explosion);
        }
    }
}