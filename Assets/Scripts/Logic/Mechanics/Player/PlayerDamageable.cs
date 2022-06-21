using System;
using System.Collections;
using UnityEngine;

namespace Game.Mechanics.PLayer
{
    public class PlayerDamageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private float timeToRestartLevel;
        private Action playerKilled;
        public void GetDamage()
        {
            StartCoroutine(CorpseRemovalTimer());
        }

        IEnumerator CorpseRemovalTimer()
        { 
            playerKilled?.Invoke();
            yield return new WaitForSeconds(timeToRestartLevel);
            Destroy(gameObject);
        }
    }
}

