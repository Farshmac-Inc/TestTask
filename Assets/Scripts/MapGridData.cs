using UnityEngine;
using Game;

namespace Tools
{
    public class MapGridData : ScriptableObject
    {
        private GridCell[,] grid;

        public GridCell[,] Grid
        {
            get => grid;
            set
            {
                grid = new GridCell[value.GetLength(0), value.GetLength(1)];
                grid = value;
            }
        }
    }
}