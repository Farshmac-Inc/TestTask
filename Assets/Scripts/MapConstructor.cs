using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEditor;


namespace MyTools
{
    public class MapConstructor : EditorWindow
    {
        private static MapConstructor window;
        private static GridCell[] prefabs = new GridCell[10];
        private static bool[] isPrefabSettingOpen = new bool[10];
        private static GridCell[,] map; 
        
        private Vector2 prefabListScrollBarValue = Vector2.zero;
        private GameObject[,] editingGameObjects;
        private GameObject editingGameObjectPrefab;
        private GridCellType typeEditingGameObject;
        private Vector2Int mapSize = new Vector2Int(30, 20);
        private Vector2Int size = Vector2Int.one;
        private Vector2Int lastSizeValue = Vector2Int.zero;

       


        public static void InitWindow()
        {
            window = GetWindow<MapConstructor>("Map Constructor");
            window.Show();
            Debug.Log(isPrefabSettingOpen.Length);
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            mapSize = EditorGUILayout.Vector2IntField("Map size ", mapSize);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Prefabs List");
            PrefabList();
            EditorGUILayout.Space();
            EditingObjectInfo();
            EditorGUILayout.EndVertical();
        }

        private void EditingObjectInfo()
        {
            if (editingGameObjects == null) return;
            
            size = EditorGUILayout.Vector2IntField("Size ", size);
            if (size != lastSizeValue) UpdateObject();
            
            var pos = editingGameObjects[0, 0].transform.position;
            var newPosition = EditorGUILayout.Vector3IntField("Position ",
                new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z));
            editingGameObjects[0, 0].transform.position = newPosition;
            
            ButtonApply();
            lastSizeValue = size;
        }

        private void ButtonApply()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if(GUILayout.Button("Apply", GUILayout.Width(70))) ApplyObject();
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
        }
        

        private void PrefabList()
        {
            prefabListScrollBarValue = EditorGUILayout.BeginScrollView(prefabListScrollBarValue, GUILayout.Height(140));
            for (int i = 0; i < prefabs.Length; i++) Prefab(i);
            EditorGUILayout.EndScrollView();
        }


        private void Prefab(int i)
        {
            ref var prefab = ref prefabs[i];
            ref var isOpen = ref isPrefabSettingOpen[i];
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginHorizontal();
            isOpen = EditorGUILayout.BeginFoldoutHeaderGroup(isOpen, (i + 1) + ") " + prefab.type.ToString());
            if (prefab.gameObject != null && prefab.type != GridCellType.Empty)
                if (GUILayout.Button("Create", GUILayout.Width(70)))
                    CrateObject(prefab.type, prefab.gameObject);
            EditorGUILayout.EndHorizontal();
            if (isOpen) PrefabSetting(ref prefab);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void PrefabSetting(ref GridCell prefab)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Object type ", GUILayout.Width(100));
            prefab.type = (GridCellType)EditorGUILayout.EnumPopup(prefab.type);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Object prefab ", GUILayout.Width(100));
            prefab.gameObject = (GameObject)EditorGUILayout.ObjectField(prefab.gameObject, typeof(GameObject), false);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        private void CrateObject(GridCellType type, GameObject prefab)
        {
            var x = mapSize.x / 2;
            var z = mapSize.y / 2;
            editingGameObjects = new GameObject[1, 1];
            editingGameObjects[0, 0] = Instantiate(prefab, new Vector3(x, 0, z), new Quaternion());
            typeEditingGameObject = type;
            editingGameObjectPrefab = prefab;
            size = Vector2Int.one;
        }

        private void UpdateObject()
        {
            var currentPos = editingGameObjects[0, 0].transform.position;
            foreach (var editObject in editingGameObjects) DestroyImmediate(editObject);
            editingGameObjects = new GameObject[size.x, size.y];
            for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
            {
                var x = currentPos.x + i;
                var z = currentPos.z + j;
                if (x < 0 && x > mapSize.x) break;
                if (z < 0 && z > mapSize.y) break;
                editingGameObjects[i, j] = Instantiate(editingGameObjectPrefab, new Vector3(x, 0, z), new Quaternion());
            }

            foreach (var obj in editingGameObjects)
            {
                if (obj != editingGameObjects[0,0]) obj.transform.SetParent(editingGameObjects[0,0].transform);
            }

            size.x = editingGameObjects.GetLength(0);
            size.y = editingGameObjects.GetLength(1);
        }

        private void ApplyObject()
        {
            if (map == null) map = new GridCell[mapSize.x, mapSize.y];
            else ClearMap();

            for (var i = 0; i < editingGameObjects.GetLength(0); i++)
            for (var j = 0; j < editingGameObjects.GetLength(1); j++)
            {
                var obj = editingGameObjects[i, j];
                obj.transform.parent = null;
                var pos = obj.transform.position;
                map[(int)pos.x, (int)pos.z] = new GridCell(typeEditingGameObject, obj);
            }
            
            typeEditingGameObject = GridCellType.Empty;
            editingGameObjectPrefab = null;
            editingGameObjects = null;
        }

        private void ClearMap()
        {
            for (var i = 0; i < map.GetLength(0); i++)
            for (var j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = new GridCell(GridCellType.Empty, null);
            }
        }
    }
}