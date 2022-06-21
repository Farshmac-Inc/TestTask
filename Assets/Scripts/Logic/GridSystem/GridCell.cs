using System;
using Game.GridSystem;
using UnityEngine;

namespace Game.GridSystem
{
    [Serializable]
    public struct GridCell
    {
        #region Fields 

        [SerializeField] public GridCellType type;
        [SerializeField] public GameObject gameObject;
        [SerializeField] public bool isAvailableForMove;

        #endregion
       
        /// <summary>
        /// Structure constructor. Selects the value isAvailableForMove automatically depending on the selected cell type.
        /// </summary>
        /// <param name="type">Type of grid cell</param>
        /// <param name="gameObject">The prefab of the game object used to create
        /// an instance when assembling the map.</param>
        public GridCell(GridCellType type, GameObject gameObject)
        {
            this.type = type;
            this.gameObject = gameObject;
            isAvailableForMove = (type != GridCellType.StoneWall && type != GridCellType.WoodWall);
        }
    }
}