using UnityEngine;
using UnityEditor;

namespace ColorFolder
{
    public class MenuItems
    {
        private const int _priority = 10000;

        [MenuItem("Assets/GGM/Red",false,_priority)]
        private static void Red()
        {
            ColoredFolderEditor.SetIconName("Red");
        }
        [MenuItem("Assets/GGM/Blue", false, _priority)]
        private static void Blue()
        {
            ColoredFolderEditor.SetIconName("Blue");
        }
        [MenuItem("Assets/GGM/Green", false, _priority)]
        private static void Green()
        {
            ColoredFolderEditor.SetIconName("Green");
        }
        [MenuItem("Assets/GGM/Custom", false, _priority+11)]
        private static void Custom()
        {
            //IconFolderEditor.ChooseCustomIcon();
        }
        [MenuItem("Assets/GGM/ResetIcon", false, _priority+21)]
        private static void ResetIcon()
        {

        }

        [MenuItem("Assets/GGM/Red", true)]
        [MenuItem("Assets/GGM/Blue", true)]
        [MenuItem("Assets/GGM/Green", true)]
        [MenuItem("Assets/GGM/Custom", true)]
        [MenuItem("Assets/GGM/ResetIcon", true)]
        private static bool ValidataFolder()
        {
            if(Selection.activeObject == null)
                return false;

            Object selectObj = Selection.activeObject;

            string path = AssetDatabase.GetAssetPath(selectObj);
            return AssetDatabase.IsValidFolder(path);
        }
    }
}

