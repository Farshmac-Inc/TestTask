using System;
using System.Collections;
using System.Collections.Generic;
using Game.GridSystem;
using UnityEngine;
using Grid = Game.GridSystem.Grid;

public class EnemySpawner : MonoBehaviour
{
    #region Fields

    [SerializeField] private float spawnCooldown;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GridCellType enemyType;

    private Vector2Int spawnPosition;

    #endregion
    
    
    private void Start()
    {
        var position = transform.position;
        spawnPosition = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
        Spawn();
        StartCoroutine(SpawnCooldown());
    }

    private IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(spawnCooldown);
        Spawn();
        
    }

    private void Spawn()
    {
        Grid.SpawnEnemy(spawnPosition, prefab, enemyType);
        StartCoroutine(SpawnCooldown());
    }
}
