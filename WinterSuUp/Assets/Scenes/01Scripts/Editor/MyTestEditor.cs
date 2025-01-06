using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(MyTest))]
public class MyTestEditor : Editor
{
    [SerializeField] private VisualTreeAsset rootTreeAsset = default;

    private TextField _nameField;

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();
        
        rootTreeAsset.CloneTree(root);
        MyTest myTest = target as MyTest;

        _nameField = root.Q<TextField>("MyNameInput");

        Toggle hasNameToggle = root.Q<Toggle>("HasNameToggle");
        hasNameToggle.RegisterValueChangedCallback(HandlePointerDownEvent);

        return root;
    }

    private void HandlePointerDownEvent(ChangeEvent<bool> evt)
    {
        DisplayStyle style = evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
        _nameField.style.display = style;
    }
}
