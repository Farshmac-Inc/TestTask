using System.Collections;
using UnityEngine;

namespace Game
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float timeBeforeExplosion;

        private void Start()
        {
            StartCoroutine(BombTimer());
        }

        private IEnumerator BombTimer()
        {
            var position = transform.position;
            Vector2 positionCell = new Vector2(Mathf.RoundToInt(position.x),
                Mathf.RoundToInt(position.z));
            yield return new WaitForSeconds(timeBeforeExplosion);
            Debug.Log("Bomb explosion in cell: " + positionCell.ToString());
            ExplosionArea(new Vector2Int((int)transform.position.x, (int)transform.position.z));
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
                    if(explosionAreMask[i,j]) Grid.RemoveElement(epicenterPosition - Vector2Int.one);
                }
        }
    }
}