using System;
using System.Collections;
using UnityEngine;

namespace Game.Mechanics.Enemy
{
    public class EnemyDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private float timeToCorpseRemoval;
        public Action EnemyKilled;
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

