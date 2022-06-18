using UnityEngine;

namespace Game
{
    public class Builder : MonoBehaviour
    {
        [SerializeField] private MovementController _movementController;
        [SerializeField] private Bomber _bomber;
        [SerializeField] private PlayerInput _playerInput;

        private void Start()
        {
            _playerInput.MoveEvent += _movementController.Move;
            _playerInput.DroppingBombEvent += _bomber.DropBomb;
        }
    }
}