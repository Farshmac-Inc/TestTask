using UnityEngine;

namespace Game
{
    
    public struct GridCell
    {
        internal GridCellType type;
        internal GameObject gameObject;

        public GridCell(GridCellType type, GameObject gameObject)
        {
            this.type = type;
            this.gameObject = gameObject;
        }

        public override string ToString()
        {
            return $"{type} | {gameObject.name}";
        }
    }
}