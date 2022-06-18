using System;
using UnityEngine;

namespace Game
{
    public class Grid : MonoBehaviour
    {
        private static GridCellType[,] grid = new GridCellType[20, 15];

        /// <summary>
        /// Adds an object to a cell on the global grid. 
        /// </summary>
        /// <param name="type">The type of object located on the cells.</param>
        /// <param name="position">The position in which the object should be placed.</param>
        /// <returns>If the position in the grid is free then True is returned otherwise False.</returns>
        public static bool AddElement(GridCellType type, Vector2Int position)
        {
            if (grid == null) return false;
            var cell = grid[position.x, position.y];
            if (cell == GridCellType.Empty)
            {
                cell = type;
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
            var cell = grid[position.x, position.y];
            if (cell == GridCellType.WoodWall)
            {
                cell = GridCellType.Empty;
                return true;
            }
            return false;
        }
    }
}