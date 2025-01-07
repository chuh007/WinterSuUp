using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class StatEditorWindow : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset visualTreeAsset = default;

    StatDatabase _statDatabase;

    [MenuItem("Window/UI Toolkit/StatEditorWindow")]
    public static void ShowExample()
    {
        StatEditorWindow wnd = GetWindow<StatEditorWindow>();
        wnd.titleContent = new GUIContent("StatEditorWindow");
        wnd.minSize = new Vector2(800, 600);
    }
    

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

    }

    private void AddListener(VisualElement root)
    {
        Button createBtn = root.Q<Button>("CreateBtn");
        createBtn.clicked += HandleCreateStat;
    }

    private void HandleCreateStat()
    {
        StatSO newStat = CreateInstance<StatSO>();
        Guid guid = Guid.NewGuid();
        newStat.statName = guid.ToString();

        string savePath = $"/Stat";
        CreateIfNotExist(savePath);
        AssetDatabase.CreateAsset(newStat, $"{savePath}/{guid}.asset");

        //if (_statDatabase.table == null)
        //    _statDatabase.table = new List<StatSO>();

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }

    private void CreateIfNotExist(string savePath)
    {
        if(!System.IO.File.Exists(savePath))
            System.IO.Directory.CreateDirectory(savePath);
    }

    private void InitialzeTable()
    {
        

        //foreach(StatSO stat in _statDatabase.table)
        //{
        //    TemplateContainer statViewUI = _statViewUI;

        //    StatViewUI statView = new StatViewUI(statViewUI,stat);
        //}
    }

    private Editor _cachedEditor;
    private VisualElement _inspector;

    private void HandleItemSelect(StatViewUI selectUI)
    {
        Editor.CreateCachedEditor(selectUI.targetStat, null, ref _cachedEditor);
        VisualElement statInspector = _cachedEditor.CreateInspectorGUI();
        _inspector.Clear();
        _inspector.Add(statInspector);

    }
}
