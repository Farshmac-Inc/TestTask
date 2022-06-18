using System.Collections;
using UnityEngine;

namespace Game
{
    public class Bomber : MonoBehaviour
    {
        [SerializeField] private float droppingBombCooldown;
        [SerializeField] private GameObject bombPrefab;
        private bool isBombReady = true;

        /// <summary>
        /// Dropping the bomb if possible
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