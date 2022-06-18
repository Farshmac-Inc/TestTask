using UnityEngine;

namespace Game
{
    public class Builder : MonoBehaviour
    {
        [SerializeField] private MovementController movementController;
        [SerializeField] private Bomber bomber;
        [SerializeField] private PlayerInput playerInput;

        private void Start()
        {
            playerInput.MoveEvent += movementController.Move;
            playerInput.DroppingBombEvent += bomber.DropBomb;
        }
    }
}