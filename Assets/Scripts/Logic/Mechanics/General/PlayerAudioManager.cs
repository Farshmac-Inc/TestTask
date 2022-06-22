using UnityEngine;

namespace Game.Audio
{
    //[RequireComponent(typeof(AudioListener))]
    public class PlayerAudioManager : AudioManager
    {
        [SerializeField] private AudioClip[] stepSound;
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
    }
}