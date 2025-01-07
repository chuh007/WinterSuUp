using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class StatEditorWindow : EditorWindow
{
    [SerializeField] private VisualTreeAsset _viewUI = default;
    [SerializeField] private VisualTreeAsset _statViewUI = default;
    [SerializeField] private StatDatabase _statDatabase = default;

    private ScrollView _listScrollView;
    private VisualElement _inspector;
    private Editor _cachedEditor;

    [MenuItem("Tools/StatEditor")]
    public static void ShowWindow()
    {
        StatEditorWindow wnd = GetWindow<StatEditorWindow>();
        wnd.titleContent = new GUIContent("StatEditorWindow");
        wnd.minSize = new Vector2(800, 600);
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        _viewUI.CloneTree(root);

        InitializeTable(root);
        AddListener(root);
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

        string savePath = $"{_statDatabase.assetPath}/Stat";
        CreateIfNotExists(savePath);
        AssetDatabase.CreateAsset(newStat, $"{savePath}/{guid}.asset");

        if (_statDatabase.Table == null)
            _statDatabase.Table = new List<StatSO>();
        _statDatabase.Table.Add(newStat);

        EditorUtility.SetDirty(_statDatabase);
        AssetDatabase.SaveAssets();
    }

    private void CreateIfNotExists(string savePath)
    {
        if (!System.IO.Directory.Exists(savePath))
        {
            System.IO.Directory.CreateDirectory(savePath);
        }
    }

    private void InitializeTable(VisualElement root)
    {
        _listScrollView = root.Q<ScrollView>("ListScrollView");
        _listScrollView.Clear();

        _inspector = root.Q<VisualElement>("Inspector");
        _inspector.Clear();

        foreach (StatSO stat in _statDatabase.Table)
        {
            TemplateContainer statViewUI = _statViewUI.Instantiate();
            _listScrollView.Add(statViewUI);

            StatViewUI statView = new StatViewUI(statViewUI, stat);

            statView.OnSelect += HandleItemSelect;
        }
    }

    private void HandleItemSelect(StatViewUI selectUI)
    {
        Editor.CreateCachedEditor(selectUI.targetStat, null, ref _cachedEditor);
        VisualElement statInspector = _cachedEditor.CreateInspectorGUI(); // Create VisualElement

        SerializedObject so = new SerializedObject(selectUI.targetStat);
        statInspector.Bind(so);

        _inspector.Clear();
        _inspector.Add(statInspector);
    }
}
