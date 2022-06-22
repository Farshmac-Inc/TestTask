using Game.Audio;
using UnityEngine;

namespace Game.Mechanics.PLayer
{
    [RequireComponent(typeof(Bomber))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerDamageable))]
    [RequireComponent(typeof(PlayerAudioManager))]
    public class PlayerConfigurator : UnitConfigurator
    {
        #region Fields

        private Bomber bomber;
        private PlayerInput playerInput;

        #endregion

        private void Start()
        {
            audioManager = GetComponent<PlayerAudioManager>();
            bomber = GetComponent<Bomber>();
            playerInput = GetComponent<PlayerInput>();

            playerInput.PauseGameEvent += GameManager.PauseGameButton;
            playerInput.DroppingBombEvent += bomber.DropBomb;
            GridSystem.Grid.PlayerKilled += Killed;

            playerInput.MoveEvent += direction =>
            {
                if (direction != Vector2.zero)
                {
                    movementController.Move(direction);
                    
                    ((PlayerAudioManager)audioManager).Step(true);
                }
                else
                {
                    ((PlayerAudioManager)audioManager).Step(false);
                }
            };

            ((PlayerDamageable)damageableComponent).playerKilled += () =>
            {
                ((PlayerAudioManager)audioManager).Die();
                animationManager.SetState(UnitState.Die);
                playerInput.SetPlayerInputState(false);
                GameManager.LevelEnd(false);
            };

            playerInput.SetPlayerInputState(true);
        }
    }
}