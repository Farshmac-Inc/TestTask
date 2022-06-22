using UnityEngine;

namespace Game.Audio
{
    //[RequireComponent(typeof(AudioListener))]
    public class PlayerAudioManager : AudioManager
    {
        [SerializeField] private AudioClip[] stepSound;
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
    }
}