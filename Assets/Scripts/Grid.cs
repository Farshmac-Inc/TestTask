using System;
using UnityEngine;

namespace Game
{
    public class GridScriptableObject : ScriptableObject
    {
        public GridScriptableObject(GridCell[,] grid)
        {
            Grid = grid;
        }

        public GridCell[,] Grid { get; private set; }
    }
    
    public class Grid : MonoBehaviour
    {
        private static GridCell[,] grid = new GridCell[30, 20];

        /// <summary>
        /// Adds an object to a cell on the global grid. 
        /// </summary>
        /// <param name="type">The type of object located on the cells.</param>
        /// <param name="position">The position in which the object should be placed.</param>
        /// <returns>If the position in the grid is free then True is returned otherwise False.</returns>
        public static bool AddElement(GridCellType type, GameObject gameObject, Vector2Int position)
        {
            if (grid == null) return false;
            ref var cell = ref grid[position.x, position.y];
            if (cell.type == GridCellType.Empty)
            {
                cell = new GridCell(type, gameObject);
                Debug.Log(grid[position.x, position.y]);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes an object to a cell on the global grid.
        /// </summary>
        /// <param name="position">The position from which you want to delete the object.</param>
        /// <returns>If it is possible to delete an object it returns True otherwise False.</returns>
        public static bool RemoveElement(Vector2Int position)
        {
            if (grid == null) return false;
            if (position.x < 0 || position.x > grid.GetLength(0)) return false;
            if (position.y < 0 || position.y > grid.GetLength(0)) return false;
            ref var cell = ref grid[position.x, position.y];
            Debug.Log(cell.type);
            if (cell.type == GridCellType.WoodWall)
            {
                Debug.Log(cell.gameObject);
                Destroy(cell.gameObject);
                cell = new GridCell();
                cell.type = GridCellType.Empty;
                return true;
            }
            return false;
        }
    }
}