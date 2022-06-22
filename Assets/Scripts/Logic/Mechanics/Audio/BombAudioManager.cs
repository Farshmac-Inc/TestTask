using UnityEngine;

namespace Game.Audio
{
    public class BombAudioManager : AudioManager
    {
        #region Field

        [SerializeField] private AudioClip explosion;

        #endregion
        
        internal void Explosion()
        {
            source.PlayOneShot(explosion);
        }
    }
}