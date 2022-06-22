using System;
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

            playerInput.MoveEvent += movementController.Move;
            movementController.SetState += ((PlayerAudioManager)audioManager).SetState;
            ((PlayerDamageable)damageableComponent).playerKilled += PlayerKilledAction;

            playerInput.SetPlayerInputState(true);
        }
        
        private void OnDestroy()
        {
            playerInput.PauseGameEvent -= GameManager.PauseGameButton;
            playerInput.DroppingBombEvent -= bomber.DropBomb;
            GridSystem.Grid.PlayerKilled -= Killed;
            
            playerInput.MoveEvent -= movementController.Move;
            movementController.SetState -= ((PlayerAudioManager)audioManager).SetState;
            ((PlayerDamageable)damageableComponent).playerKilled -= PlayerKilledAction;
        }

        private void PlayerKilledAction()
        {
            ((PlayerAudioManager)audioManager).Die();
            animationManager.SetState(UnitState.Die);
            playerInput.SetPlayerInputState(false);
            GameManager.LevelEnd(false);
        }
    }
}