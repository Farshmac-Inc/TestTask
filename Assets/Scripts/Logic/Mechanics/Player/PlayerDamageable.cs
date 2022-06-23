using System;
using System.Collections;
using UnityEngine;

namespace Game.Mechanics.PLayer
{
    public class PlayerDamageable : MonoBehaviour, IDamageable
    {

        #region Fields

        [SerializeField] private float timeToRestartLevel;
        public Action playerKilled;

        #endregion
        
        /// <summary>
        /// A method that processes an event caused as a result of damage.
        /// </summary>
        public void GetDamage()
        {
            StartCoroutine(CorpseRemovalTimer());
        }

        private IEnumerator CorpseRemovalTimer()
        { 
            playerKilled?.Invoke();
            yield return new WaitForSeconds(timeToRestartLevel);
            GameManager.LevelEnd(false);
            Destroy(gameObject);
        }
    }
}

