using System;
using System.Collections.Generic;
using Game.PathFinder;
using UnityEngine;

namespace Game.GridSystem
{
    [RequireComponent(typeof(GridCollector))]
    public class Grid : MonoBehaviour
    {
        #region Field

        [SerializeField] private MapGridData mapGridData;
        private GridCollector collector;
        public static Action GridChange;
        public static Action PlayerKilled;

        private static GridCell[,] grid;
        private static PathNode[,] naviGrid;
        private static bool[,] naviGridAvailable;
        private static Vector2Int playerPosition;
        private static Vector2Int finishPosition;

        private static List<MovableGridObject> movableObjects;
        private static List<Vector2Int> positionDestroyableObjects;

        #endregion

        private void Awake()
        {
            collector = GetComponent<GridCollector>();
            if (mapGridData != null)
                grid = collector.SetGrid(mapGridData, out movableObjects, out positionDestroyableObjects,
                    out playerPosition, out finishPosition);
        }

        private static void CheckInteractionMovableObjects(MovableGridObject obj1, MovableGridObject obj2)
        {
            switch (obj1.Type)
            {
                case GridCellType.Enemy:
                {
                    if (obj2.Type == GridCellType.Player)
                    {
                        RemoveMovableObject(obj2);
                        ((Mechanics.Enemy.EnemyConfigurator)obj1.Configurator).PlayerKilled();
                    }

                    break;
                }
                case GridCellType.Player:
                {
                    playerPosition = obj2.CurrentPosition;
                    if (obj2.Type == GridCellType.Enemy)
                    {
                        RemoveMovableObject(obj1);
                        ((Mechanics.Enemy.EnemyConfigurator)obj2.Configurator).PlayerKilled();
                    }
                    else if (obj2.CurrentPosition == finishPosition)
                    {
                        GameManager.LevelEnd(true);
                    }

                    break;
                }
            }
        }

        private static void PlayerKill()
        {
            PlayerKilled?.Invoke();
        }


        private static void MoveElement(Vector2Int newPos, int objectID)
        {
            var movingObject = movableObjects[objectID];
            foreach (var obj in movableObjects)
            {
                if (obj.CurrentPosition == newPos)
                {
                    CheckInteractionMovableObjects(movingObject, obj);
                }
            }

            movingObject.CurrentPosition = newPos;
        }

        private static void RemoveMovableObject(MovableGridObject obj, int id)
        {
            switch (obj.Type)
            {
                case GridCellType.Player:
                {
                    PlayerKill();
                    movableObjects.RemoveAt(0);
                    break;
                }
                case GridCellType.Enemy:
                {
                    obj.Configurator.Killed?.Invoke();
                    for (var index = id + 1; index < movableObjects.Count; index++)
                    {
                        var movableObject = movableObjects[index];
                        movableObject.Configurator.SetNewID?.Invoke(id);
                    }

                    movableObjects.RemoveAt(id);
                    break;
                }
            }
        }


        private static void RemoveMovableObject(MovableGridObject removingObj)
        {
            var index = movableObjects.FindIndex((obj) => obj == removingObj);
            RemoveMovableObject(removingObj, index);
        }

        private static void RemoveStaticObject(int index)
        {
            var obj = positionDestroyableObjects[index];
            ref var cell = ref grid[obj.x, obj.y];
            positionDestroyableObjects.RemoveAt(index);
            Destroy(cell.gameObject);
            cell = new GridCell(GridCellType.Empty, null);
            GridChange?.Invoke();
        }

        public void UploadLevel(MapGridData data)
        {
            grid = collector.SetGrid(data, out movableObjects, out positionDestroyableObjects, out playerPosition,
                out finishPosition);
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


        /// <summary>
        /// A method that handles an event triggered by one of the movable game elements (Player, enemies).
        /// </summary>
        /// <param name="pos">The new position of the object in the grid.</param>
        /// <param name="movableObjectID">The sequential number of the object in the list movableObjects</param>
        public static void SetMovableElementPosition(Vector2Int pos, int movableObjectID)
        {
            MoveElement(pos, movableObjectID);
        }

        /// <summary>
        /// A method that checks the ability to remove an object from a cell.
        /// If there is such a possibility, it deletes it.
        /// </summary>
        /// <param name="cellPos">The grid cell that the method checks.</param>
        /// <returns>The ability to remove an object from a cell.</returns>
        public static void RemoveElement(Vector2Int cellPos)
        {
            for (var id = 0; id < movableObjects.Count; id++)
            {
                var movableObject = movableObjects[id];
                if (movableObject.CurrentPosition == cellPos)
                {
                    RemoveMovableObject(movableObject, id);
                }
            }

            for (var i = 0; i < positionDestroyableObjects.Count; i++)
            {
                var destroyableObject = positionDestroyableObjects[i];
                if (destroyableObject == cellPos)
                {
                    RemoveStaticObject(i);
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