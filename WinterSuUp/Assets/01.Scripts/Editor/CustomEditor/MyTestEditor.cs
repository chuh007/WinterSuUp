using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using UnityEditor.UIElements;


[CustomEditor(typeof(MyTest))]
public class MyTestEditor : Editor
{
    [SerializeField] private VisualTreeAsset rootTreeAeest = default;

    private TextField _nameField;
    private VisualElement _imgView;

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        rootTreeAeest.CloneTree(root);
        MyTest myTest = target as MyTest;

        _nameField = root.Q<TextField>("MyNameInput");
        _imgView = root.Q<ObjectField>("Image");
        

        Toggle hasNameToggle = root.Q<Toggle>("HasNameToggle");
        hasNameToggle.RegisterValueChangedCallback(HandleValueChanged);

        ObjectField spriteField = root.Q<ObjectField>("SpriteField");
        spriteField.RegisterValueChangedCallback(HandleSpriteChanged);

        return root;
    }

    private void HandleSpriteChanged(ChangeEvent<UnityEngine.Object> evt)
    {
        Sprite newSprite = evt.newValue as Sprite;
        _imgView.style.backgroundImage = new StyleBackground(newSprite);
    }

    private void HandleValueChanged(ChangeEvent<bool> evt)
    {
        DisplayStyle style = evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
        _nameField.style.display = style;
    }
}
