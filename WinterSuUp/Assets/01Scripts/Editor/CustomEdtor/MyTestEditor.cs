using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

[CustomEditor(typeof(MyTest))]
public class MyTestEditor : Editor
{
    [SerializeField] private VisualTreeAsset rootTreeAsset = default;

    private TextField _nameField;
    private VisualElement _imageView;

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();
        
        rootTreeAsset.CloneTree(root);
        MyTest myTest = target as MyTest;

        _nameField = root.Q<TextField>("MyNameInput");
        _imageView = root.Q<VisualElement>("Image");

        Toggle hasNameToggle = root.Q<Toggle>("HasNameToggle");
        hasNameToggle.RegisterValueChangedCallback(HandlePointerDownEvent);

        ObjectField spriteField = root.Q<ObjectField>("SpriteField");
        spriteField.RegisterValueChangedCallback(HandleSpriteChange);

        return root;
    }

    private void HandleSpriteChange(ChangeEvent<UnityEngine.Object> evt)
    {
        Debug.Log("a");
        if(evt.newValue is Sprite sprite)
        {
            _imageView.style.backgroundImage = new StyleBackground(sprite);
        }
    }

    private void HandlePointerDownEvent(ChangeEvent<bool> evt)
    {
        DisplayStyle style = evt.newValue ? DisplayStyle.Flex : DisplayStyle.None;
        _nameField.style.display = style;
    }
}
