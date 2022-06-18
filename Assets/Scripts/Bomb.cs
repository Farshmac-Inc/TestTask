using System.Collections;
using UnityEngine;

namespace Game
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float _timeBeforeExplosion;

        void Start()
        {
            StartCoroutine(BombTimer());
        }

        private IEnumerator BombTimer()
        {
            var position = transform.position;
            Vector2 positionCell = new Vector2(Mathf.RoundToInt(position.x),
                Mathf.RoundToInt(position.z));
            yield return new WaitForSeconds(_timeBeforeExplosion);
            Debug.Log("Bomb explosion in cell: " + positionCell.ToString());
            Destroy(this.gameObject);
        }
    }
}