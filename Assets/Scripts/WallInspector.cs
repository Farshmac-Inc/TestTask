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
            if (GUILayout.Button("Create")) GeneratePrefab();
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

        private void GeneratePrefab()
        {
            CorrectPosition();
            foreach (var element in targetClass.elements) DestroyImmediate(element);
            targetClass.elements.Clear();

            for (int x = 0; x < targetClass.size.x; x++)
            {
                for (int z = 0; z < targetClass.size.y; z++)
                {
                    targetClass.elements.Add(Instantiate(targetClass.prefab,
                        targetClass.baseElementPosition + new Vector3(x, 0, z), new Quaternion()));
                }
            }
        }
    }
}