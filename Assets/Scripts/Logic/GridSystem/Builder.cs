using UnityEngine;

namespace Game
{
    public class Builder : MonoBehaviour
    {
        #region Fields

        [SerializeField] private MovementController movementController;
        [SerializeField] private Bomber bomber;
        [SerializeField] private PlayerInput playerInput;

        #endregion
        

        private void Start()
        {
            playerInput.MoveEvent += movementController.Move;
            playerInput.DroppingBombEvent += bomber.DropBomb;
        }
    }
}