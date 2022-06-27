using UnityEngine;

namespace Game.GridSystem
{
    public class MovableGridObject
    {
        public int id { get; }
        public GridCellType Type { get; }
        public GameObject MovableGameObject;
        public Vector2Int CurrentPosition;
        public Mechanics.UnitConfigurator Configurator { get; }

        public MovableGridObject(GameObject movableGameObject, GridCellType type, Vector2Int currentPosition, int id)
        {
            this.MovableGameObject = movableGameObject;
            this.Type = type;
            this.CurrentPosition = currentPosition;
            this.id = id;
            Configurator = movableGameObject.GetComponent<Mechanics.UnitConfigurator>();
        }
        
    }
}