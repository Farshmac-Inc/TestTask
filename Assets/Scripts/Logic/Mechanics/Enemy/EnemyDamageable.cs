using System;
using System.Collections;
using UnityEngine;

namespace Game.Mechanics.Enemy
{
    public class EnemyDamageable : MonoBehaviour, IDamageable
    {
        #region Fields

        [SerializeField] private float timeToCorpseRemoval;
        public Action EnemyKilled;

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
            EnemyKilled?.Invoke();
            yield return new WaitForSeconds(timeToCorpseRemoval);
            Destroy(gameObject);
        }
        
    }
}

