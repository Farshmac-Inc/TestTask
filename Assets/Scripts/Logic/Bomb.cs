using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float timeBeforeExplosion;
        private Vector2Int position;

        private void Start()
        {
            StartCoroutine(BombTimer());
            var bombWorldPosition = transform.position;
            position = new Vector2Int(Mathf.RoundToInt(bombWorldPosition.x), Mathf.RoundToInt(bombWorldPosition.z));
            transform.position = new Vector3(position.x, 0, position.y);
        }

        private IEnumerator BombTimer()
        {
            yield return new WaitForSeconds(timeBeforeExplosion);
            ExplosionArea(position);
            Destroy(this.gameObject);
        }

        private void ExplosionArea(Vector2Int epicenterPosition)
        {
            var explosionAreMask = new bool[,]
            {
                { false, true, false },
                { true, true, true },
                { false, true, false },
            };
            for (int i = 0; i < explosionAreMask.GetLength(0); i++)
                for (int j = 0; j < explosionAreMask.GetLength(1); j++)
                {
                    if(explosionAreMask[i,j]) 
                        if(Grid.RemoveElement(epicenterPosition - Vector2Int.one + new Vector2Int(i,j))
                           );
                }
        }
    }
}