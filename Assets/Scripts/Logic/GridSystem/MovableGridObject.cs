using System.Collections;
using UnityEngine;

namespace Game.GridSystem
{
    public class MovableGridObject
    {
        public int ID { get; }
        public GridCellType Type { get; }
        public GameObject MovableGameObject;
        public Vector2Int CurrentPosition;
        public Mechanics.UnitConfigurator Configurator { get; }

        public MovableGridObject(GameObject movableGameObject, GridCellType type, Vector2Int currentPosition, int id)
        {
            MovableGameObject = movableGameObject;
            Type = type;
            CurrentPosition = currentPosition;
            ID = id;
            Configurator = movableGameObject.GetComponent<Mechanics.UnitConfigurator>();
            Configurator.Setuper();
            Configurator.SetNewID?.Invoke(id);
        }

    }
}