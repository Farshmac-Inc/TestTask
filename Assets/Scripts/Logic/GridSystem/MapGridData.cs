using System;
using UnityEngine;
using Game;

namespace Tools
{
    public class MapGridData : ScriptableObject
    {
        [SerializeField] private GridCell[] grid;
        [SerializeField] private int width = 0;
        public GridCell[,] Grid
        {
            get
            {
                var result = new GridCell[grid.Length / width, width];
                for (int i = 0; i < grid.Length; i++)
                {
                    result[i / width, i % width] = grid[i];
                }
                return result;
            }
            set
            {
                grid = new GridCell[value.GetLength(0) * value.GetLength(1)];
                width = value.GetLength(1);

                int xMax = value.GetLength(0);
                int yMax = value.GetLength(1);

                for (int x = 0; x < xMax; x++)
                for (int y = 0; y < yMax; y++)
                {
                    GridCell gridCell = value[x, y];
                    grid[x * width + y] = gridCell;
                }
            }
        }
    }
}