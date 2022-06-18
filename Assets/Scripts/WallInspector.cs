using UnityEditor;
using UnityEngine;

namespace Game
{
    [CustomEditor(typeof(Wall))]
    public class WallInspector : Editor
    {
        private Wall targetClass;

        private void OnEnable()
        {
            targetClass = (Wall)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create")) GeneratePrefab();
            if (GUILayout.Button("X", GUILayout.Width(20))) ClearElements();
            EditorGUILayout.EndHorizontal();
        }

        private void CorrectPosition()
        {
            var position = targetClass.transform.position;
            var correctedPosition = new Vector3(Mathf.RoundToInt(position.x),
                Mathf.RoundToInt(position.y),
                Mathf.RoundToInt(position.z));
            targetClass.transform.position = correctedPosition;
            targetClass.baseElementPosition = correctedPosition;
        }

        private void ClearElements()
        {
            foreach (var element in targetClass.elements)
            {
                var position = element.transform.position;
                Grid.RemoveElement(new Vector2Int((int)position.x, (int)position.y));
                DestroyImmediate(element);
            }
            targetClass.elements.Clear();
        }

        private void GeneratePrefab()
        {
            CorrectPosition();
            ClearElements();
            for (int x = 0; x < targetClass.size.x; x++)
            {
                for (int z = 0; z < targetClass.size.y; z++)
                {
                    var position = targetClass.baseElementPosition + new Vector3(x, 0, z);
                    var gameObject = Instantiate(targetClass.prefab, position, new Quaternion(), targetClass.transform);
                    targetClass.elements.Add(gameObject);
                    Grid.AddElement(targetClass.type, gameObject, new Vector2Int((int)position.x, (int)position.y));
                }
            }
        }
    }
}