using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.GridSystem
{
    public class GridCollector : MonoBehaviour
    {
        //private GridCell[,] grid;

        private List<MovableGridObject> movableObjects;
        private List<Vector2Int> positionDestroyableObjects;

        private Vector2Int playerPosition;
        private Vector2Int finishPosition;


        internal GridCell[,] SetGrid(MapGridData data, out List<MovableGridObject> movableObjects,
            out List<Vector2Int> PositionDestroyableObjects, out Vector2Int playerPosition,
            out Vector2Int finishPosition)
        {
            var grid = GetStaticGrid(data);
            grid = InstallStaticObjectInMap(grid);
            InstallDynamicObjects();
            movableObjects = this.movableObjects;
            PositionDestroyableObjects = this.positionDestroyableObjects;
            playerPosition = this.playerPosition;
            finishPosition = this.finishPosition;
            return grid;
        }

        private GridCell[,] GetStaticGrid(MapGridData data)
        {
            var grid = data.Grid;
            movableObjects = new List<MovableGridObject>();
            int moveObjID = 1;
            positionDestroyableObjects = new List<Vector2Int>();
            for (var x = 0; x < grid.GetLength(0); x++)
            for (var z = 0; z < grid.GetLength(1); z++)
            {
                ref var cell = ref grid[x, z];
                switch (cell.type)
                {
                    case GridCellType.Enemy:
                    {
                        movableObjects.Add(new MovableGridObject(cell.gameObject,
                            GridCellType.Enemy, new Vector2Int(x, z), moveObjID));
                        cell = new GridCell(GridCellType.Empty, null);
                        moveObjID++;
                        break;
                    }
                    case GridCellType.Player:
                    {
                        movableObjects.Add(new MovableGridObject(cell.gameObject,
                            GridCellType.Player, new Vector2Int(x, z), 0));
                        
                        cell = new GridCell(GridCellType.Empty, null);
                        break;
                    }
                    case GridCellType.Finish:
                    {
                        finishPosition = new Vector2Int(x, z);
                        cell = new GridCell(GridCellType.Empty, null);
                        break;
                    }
                    case GridCellType.WoodWall:
                    {
                        positionDestroyableObjects.Add(new Vector2Int(x, z));
                        break;
                    }
                }
            }

            return grid;
        }

        private GridCell[,] InstallStaticObjectInMap(GridCell[,] grid)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            for (var z = 0; z < grid.GetLength(1); z++)
            {
                ref var cell = ref grid[x, z];
                if (cell.type != GridCellType.Empty && cell.gameObject != null)
                {
                    var prefab = cell.gameObject;
                    cell.gameObject = Instantiate(prefab, new Vector3(x, 0, z), new Quaternion());
                }
                else
                {
                    cell.gameObject = null;
                }

                cell.isAvailableForMove = cell.type != GridCellType.WoodWall && cell.type != GridCellType.StoneWall;
            }

            return grid;
        }

        private void InstallDynamicObjects()
        {
            for (var i = 0; i < movableObjects.Count; i++)
            {
                var movableObject = movableObjects[i];
                ref var gameObject = ref movableObject.MovableGameObject;
                ref var position = ref movableObject.CurrentPosition;
                gameObject = Instantiate(gameObject);
                gameObject.gameObject.transform.position = new Vector3(position.x, 0, position.y);
                movableObject.Configurator.Setup?.Invoke(i, position);
            }
        }
    }
}