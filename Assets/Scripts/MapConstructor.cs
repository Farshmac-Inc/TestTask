using System;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEditor;


namespace MyTools
{
    public class MapConstructor : EditorWindow
    {
        private static MapConstructor window;
        private GridCell[] prefabs = new GridCell[3];
        private Vector2 scrollPosition = Vector2.zero;
        private bool[] prefabsFadeGroupParam = new bool[3];
        private int size;
        private bool b1;
        

        public static void InitWindow()
        {
            window = GetWindow<MapConstructor>("Map Constructor");
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Test");
            
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            for (int i = 0; i < prefabs.Length; i++)
            {
                prefabsFadeGroupParam[i] = EditorGUILayout.BeginFoldoutHeaderGroup(prefabsFadeGroupParam[i], "");
               GridCellItem(i);
               EditorGUILayout.EndFoldoutHeaderGroup();
            }
            EditorGUILayout.EndScrollView();
        }
        public void GridCellItem(int i)
        {
            ref var cell = ref prefabs[i];
            
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(4);

            EditorGUILayout.LabelField(cell.type.ToString());
                
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Type", GUILayout.Width(60));
            cell.type = (GridCellType)EditorGUILayout.EnumPopup(cell.type);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Model", GUILayout.Width(60));
            cell.gameObject = (GameObject)EditorGUILayout.ObjectField(cell.gameObject, typeof(GameObject), false);
            EditorGUILayout.EndHorizontal();
                
            EditorGUILayout.Space(4);
            EditorGUILayout.EndVertical();
           
        }
    }


    public class ToolsMenu
    {
        [MenuItem("MyTools/MapConstructor")]
        public static void InitProjectSetupTools()
        {
            MapConstructor.InitWindow();
        }
    }
}