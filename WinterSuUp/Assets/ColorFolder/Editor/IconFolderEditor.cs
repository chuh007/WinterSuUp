using UnityEditor;
using UnityEngine;

namespace ColorFolder
{
    [InitializeOnLoad]
    public class IconFolderEditor
    {
        private static string _selectedFolderGUID;
        private static int _controlID;

        static IconFolderEditor()
        {
            EditorApplication.projectWindowItemOnGUI -= OnGuiCall;
            EditorApplication.projectWindowItemOnGUI += OnGuiCall;
        }

        private static void OnGuiCall(string guid, Rect selectionRect)
        {
            if (guid != _selectedFolderGUID) return;

            if(Event.current.commandName == "ObjectSelectorUpdated"&&
                EditorGUIUtility.GetObjectPickerControlID() == _controlID)
            {
                Object selectedObject = EditorGUIUtility.GetObjectPickerObject();

                string path = AssetDatabase.GetAssetPath(selectedObject);
                string textureGUID = AssetDatabase.GUIDFromAssetPath(path).ToString();

                //EditorPrefs.SetString();
            }
        }

        public static void ChooseCustomIcon()
        {
            _selectedFolderGUID = Selection.assetGUIDs[0];
            _controlID = EditorGUIUtility.GetControlID(FocusType.Passive);

            EditorGUIUtility.ShowObjectPicker<Sprite>(null, false, "", _controlID); 
        }
    }

}

