using System.Collections;
using UnityEngine;

namespace Game.Mechanics
{
    public class Bomber : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float droppingBombCooldown;
        [SerializeField] private GameObject bombPrefab;
        private bool isBombReady = true;

        #endregion
        
        /// <summary>
        /// A method that handles an event called by the player's control,
        /// which checks the possibility of dropping a bomb.
        /// If there is such an opportunity, resets it.
        /// </summary>
        public void DropBomb()
        {
            if (!isBombReady) return;
            StartCoroutine(BombPreparationCoroutine());
            Instantiate(bombPrefab, transform.position, new Quaternion());
        }

        private IEnumerator BombPreparationCoroutine()
        {
            isBombReady = false;
            yield return new WaitForSeconds(droppingBombCooldown);
            isBombReady = true;
        }
    }
}