using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Grid : MonoBehaviour
    {
        public Tools.MapGridData TESTDATA_DELETE_ME;
        private GridCell[,] grid;

        private void Start()
        {
            if(TESTDATA_DELETE_ME!= null) SetGrid(TESTDATA_DELETE_ME);
        }

        private void SetGrid(Tools.MapGridData data)
        {
            Debug.Log(data.Grid);
            grid = new GridCell[data.Grid.GetLength(0), data.Grid.GetLength(1)];
            grid = data.Grid;
            for (var x = 0; x < grid.GetLength(0); x++)
            for (var z = 0; z < grid.GetLength(1); z++)
            {
                var cell = grid[x, z];
                var prefab = cell.gameObject;
                cell.gameObject = Instantiate(prefab, new Vector3(x, 0, z), new Quaternion());
            }
        }
    }
}

