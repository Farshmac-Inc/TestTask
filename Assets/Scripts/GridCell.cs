using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public struct GridCell
    {
        [SerializeField] internal GridCellType type;
        [SerializeField] internal GameObject gameObject;

        public GridCell(GridCellType type, GameObject gameObject)
        {
            this.type = type;
            this.gameObject = gameObject;
        }

        public override string ToString()
        {
            return $"{type} | {gameObject}";
        }
    }
}