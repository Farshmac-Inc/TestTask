using System;

namespace Game.GridSystem
{
    [Serializable]
    public enum GridCellType
    {
        Empty,
        WoodWall,
        StoneWall,
        Player,
        Enemy,
        Spawner
    }
}