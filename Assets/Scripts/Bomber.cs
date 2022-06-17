using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    [SerializeField] private float droppingBombCooldown;
    [SerializeField] private GameObject bombPrefab;
    private bool isBombReady = true;

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
