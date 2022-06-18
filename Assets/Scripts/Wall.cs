using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public sealed class Wall : MonoBehaviour
    {
        [SerializeField] internal Vector2Int size = Vector2Int.one;
        [SerializeField] internal GameObject prefab;
        internal List<GameObject> elements = new List<GameObject>();
        internal Vector3 baseElementPosition;
    }
}