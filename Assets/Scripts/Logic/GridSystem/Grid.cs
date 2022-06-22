using System;
using Game.PathFinder;
using UnityEngine;

namespace Game.GridSystem
{
    public class Grid : MonoBehaviour
    {
        #region Serialized Field

        [SerializeField] private MapGridData mapGridData;
        public static Action GridChange;
        public static Action PlayerKilled;

        #endregion

        #region Private Fields

        private static GridCell[,] grid;
        private static PathNode[,] naviGrid;
        private static bool[,] naviGridAvailable;
        private static Vector2Int playerPosition;

        #endregion

        private void Awake()
        {
            if (mapGridData != null) grid = SetGrid(mapGridData);
        }

        private GridCell[,] SetGrid(MapGridData data)
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

                if (cell.type == GridCellType.Player) playerPosition = new Vector2Int(x, z);
                if (cell.type == GridCellType.Spawner)
                {
                    cell.type = GridCellType.Empty;
                    cell.gameObject = null;
                }
                cell.isAvailableForMove = cell.type != GridCellType.WoodWall && cell.type != GridCellType.StoneWall;
            }
            return grid;
        }
        
        /// <summary>
        /// A method that creates an opponent on the grid.
        /// </summary>
        /// <param name="position">The position in which the enemy should sleep.</param>
        /// <param name="prefab">Enemy prefab used to create an instance.</param>
        /// <param name="type">The type of enemy being created.</param>
        public static void SpawnEnemy(Vector2Int position, GameObject prefab, GridCellType type)
        {
            ref var cell = ref grid[position.x, position.y];
            cell.type = type;
            cell.gameObject = Instantiate(prefab, new Vector3(position.x, 0, position.y), new Quaternion());
        }

        private static void MoveElement(Vector2Int lastPos, Vector2Int newPos, GridCellType type)
        {
            ref var startCell = ref grid[lastPos.x, lastPos.y];
            ref var finishCell = ref grid[newPos.x, newPos.y];
            if (startCell.type == GridCellType.Player && finishCell.type == GridCellType.Enemy ||
                startCell.type == GridCellType.Enemy && finishCell.type == GridCellType.Player)
            {
                PlayerKilled?.Invoke();
                return;
            }
            finishCell = new GridCell(type, startCell.gameObject);
            startCell = new GridCell(GridCellType.Empty, null);
            
            if (type == GridCellType.Player)
            {
                GridChange?.Invoke();
                playerPosition = newPos;
            }
        }

        /// <summary>
        /// A method that handles an event triggered by one of the movable game elements (Player, enemies).
        /// </summary>
        /// <param name="pos">The new position of the object in the grid.</param>
        /// <param name="lastPos">The old position of the object in the grid.</param>
        /// <param name="type">The type of the object being moved.</param>
        public static void SetMovableElementPosition(Vector2Int pos, Vector2Int lastPos, GridCellType type)
        {
            MoveElement(lastPos, pos, type);
        }

        /// <summary>
        /// A method that checks the ability to remove an object from a cell.
        /// If there is such a possibility, it deletes it.
        /// </summary>
        /// <param name="cellPos">The grid cell that the method checks.</param>
        /// <returns>The ability to remove an object from a cell.</returns>
        public static bool RemoveElement(Vector2Int cellPos)
        {
            ref var cell = ref grid[cellPos.x, cellPos.y];
            switch (cell.type)
            {
                case GridCellType.WoodWall:
                {
                    Destroy(cell.gameObject);
                    cell = new GridCell(GridCellType.Empty, null);
                    GridChange?.Invoke();
                    return true;
                }
                case GridCellType.Player:
                {
                    PlayerKilled?.Invoke();
                    return true;
                }
                case GridCellType.Enemy:
                {
                    cell.gameObject.GetComponent<Mechanics.UnitConfigurator>().Killed?.Invoke();
                    return true;
                }
                default:
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// A method that handles an event caused by enemies when a player moves across the grid, or the grid itself changes.
        /// </summary>
        /// <param name="enemyPos">The grid cell in which the enemy is currently located.</param>
        /// <returns>A straight-ordered array of grid cells that must be traversed to get to the player.</returns>
        public static Vector2Int[] FindPathToPlayer(Vector2Int enemyPos)
        {
            if (grid == null) return null;
            if (playerPosition == Vector2Int.zero) return null;
            return PathFinder.PathFinder.FindPath(enemyPos, playerPosition, grid);
        }
    }
}