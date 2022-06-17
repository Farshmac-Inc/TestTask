using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    [SerializeField] private float droppingBombCooldown;
    private bool isBombReady = true;

    public void DropBomb()
    {
        if (!isBombReady) return;
        StartCoroutine(BombPreparationCoroutine());
        Debug.Log("Bomb Droped");
    }
    
    private IEnumerator BombPreparationCoroutine()
    {
        isBombReady = false;
        yield return new WaitForSeconds(droppingBombCooldown);
        isBombReady = true;
    }
}
