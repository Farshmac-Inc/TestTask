using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public struct GridCell
    {
        [SerializeField] public GridCellType type;
        [SerializeField] public GameObject gameObject;
        [SerializeField] public bool isAvailableForMove;

        public GridCell(GridCellType type, GameObject gameObject)
        {
            this.type = type;
            this.gameObject = gameObject;
            isAvailableForMove = (type != GridCellType.StoneWall && type != GridCellType.WoodWall);
        }

        public override string ToString()
        {
            return $"{type} | {gameObject}";
        }
    }
}