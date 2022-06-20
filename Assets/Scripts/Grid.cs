using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private Tools.MapGridData mapGridData;
        private static GridCell[,] grid;
        private static bool[,] naviGrid;

        public static void SetMovableElementPosition(Vector2Int pos, Vector2Int lastPos, GridCellType type)
        {
            MoveElement(lastPos, pos, type);
        }

        private void Start()
        {
            if(mapGridData!= null) SetGrid(mapGridData);
        }

        private void SetGrid(Tools.MapGridData data)
        {
            grid = new GridCell[data.Grid.GetLength(0), data.Grid.GetLength(1)];
            grid = data.Grid;
            for (var x = 0; x < grid.GetLength(0); x++)
            for (var z = 0; z < grid.GetLength(1); z++)
            {
                ref var cell = ref grid[x, z];
                if (cell.type != GridCellType.Empty && cell.gameObject != null)
                {
                    var prefab = cell.gameObject;
                    cell.gameObject = Instantiate(prefab, new Vector3(x, 0, z), new Quaternion());
                }
            }
        }

        private static void MoveElement(Vector2Int lastPos, Vector2Int newPos, GridCellType type)
        {
            ref var startCell = ref grid[lastPos.x, lastPos.y];
            ref var finishCell = ref grid[newPos.x, newPos.y];
            finishCell = new GridCell(type, startCell.gameObject);
            startCell = new GridCell(GridCellType.Empty, null);
            
        }

        public static bool RemoveElement(Vector2Int cellPos)
        {
            ref var cell = ref grid[cellPos.x, cellPos.y];
            switch (cell.type)
            {
                case GridCellType.WoodWall:
                {
                    Destroy(cell.gameObject);
                    cell = new GridCell(GridCellType.Empty, null);
                    return true;
                }
                case GridCellType.StoneWall:
                {
                    return false;
                }
                case GridCellType.Player:
                {
                    Debug.Log("Player killed");
                    return true;
                }
                default:
                {
                    return false;
                }
            }
        }
    }
}

