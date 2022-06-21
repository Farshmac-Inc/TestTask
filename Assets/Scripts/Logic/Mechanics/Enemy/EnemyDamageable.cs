using System;
using System.Collections;
using UnityEngine;

namespace Game.Mechanics.Enemy
{
    public class EnemyDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private float timeToCorpseRemoval;
        private Action enemyKilled;
        public void GetDamage()
        {
            StartCoroutine(CorpseRemovalTimer());
        }

        private IEnumerator CorpseRemovalTimer()
        { 
            enemyKilled?.Invoke();
            yield return new WaitForSeconds(timeToCorpseRemoval);
            Destroy(gameObject);
        }
        
    }
}

