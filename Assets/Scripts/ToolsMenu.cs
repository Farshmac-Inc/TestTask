using UnityEditor;

namespace MyTools
{
    public static class ToolsMenu
    {
        [MenuItem("MyTools/MapConstructor")]
        public static void InitProjectSetupTools()
        {
            MapConstructor.InitWindow();
        }
    }
}