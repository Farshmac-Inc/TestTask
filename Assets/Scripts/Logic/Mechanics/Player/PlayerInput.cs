using System;
using UnityEngine;

namespace Game.Mechanics.PLayer
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private KeyCode DroppingBombButton = KeyCode.Space;

        private bool isPlayerInputActive;
        
        
        /// <summary>
        /// The event sets the movement vector for the player.
        /// </summary>
        public Action<Vector2> MoveEvent;
        /// <summary>
        /// An event triggered by pressing the bomb reset button.
        /// </summary>
        public Action DroppingBombEvent;

        private void Update()
        {
            if (!isPlayerInputActive) return;
            MoveEvent?.Invoke(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            if (Input.GetKeyDown(DroppingBombButton)) DroppingBombEvent?.Invoke();
        }

        public void SetPlayerInputState(bool state) => isPlayerInputActive = state;
    }
}