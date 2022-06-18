using UnityEngine;

namespace Game
{
    public class Grid : MonoBehaviour
    {
        private GridCellType[,] grid;

        public bool AddWall(GridCellType type, Vector2Int position)
        {
            if (grid[position.x, position.y] != GridCellType.Empty) return false;
            grid[position.x, position.y] = type;
            return true;
        }

        public bool RemoveWall(Vector2Int position)
        {
            if (grid[position.x, position.y] == GridCellType.WoodWall)
            {
                grid[position.x, position.y] = GridCellType.Empty;
                return true;
            }

            return false;
        }
    }
}