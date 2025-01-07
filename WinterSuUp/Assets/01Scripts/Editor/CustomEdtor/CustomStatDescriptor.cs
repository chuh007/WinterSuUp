using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(StatDescriptor))]
public class CustomStatDescriptor : PropertyDrawer
{
    private TextField _statNameField;
    [SerializeField] private VisualTreeAsset _statPropertyView = default;
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement root = new VisualElement();
        _statPropertyView.CloneTree(root);

        SerializedProperty isShowProp = property.FindPropertyRelative("isShow");

        PropertyField isShowField = root.Q<PropertyField>("IsShowField");
        PropertyField statNameField = root.Q<PropertyField>("StatNameField");

        isShowField.TrackPropertyValue(isShowProp, prop=>
        {
            statNameField.style.display = isShowProp.boolValue ? DisplayStyle.Flex : DisplayStyle.None;
        });

        return root;
    }

}
