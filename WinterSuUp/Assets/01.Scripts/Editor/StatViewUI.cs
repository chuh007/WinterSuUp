using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class StatViewUI
{
    public event Action<StatViewUI> OnSelect;
    public event Action<StatViewUI> OnDelete;

    public VisualElement sprite;
    public Label titleLabel;
    public Button deleteBtn;

    public StatSO targetStat;

    private VisualElement _container;

    public StatViewUI(TemplateContainer root, StatSO stat)
    {
        root.RegisterCallback<ClickEvent>(HandleItemSelect);
        targetStat = stat;

        sprite = root.Q<VisualElement>("Image");
        titleLabel = root.Q<Label>("StatTitle");
        deleteBtn = root.Q<Button>("DeleteBtn");
        _container = root.Q<VisualElement>("Container");
        deleteBtn.RegisterValueChangedCallback(HandleDelete);
        //deleteBtn.clicked += HandleDelete;

        RefreshUI();
    }

    

    public void SetSelection(bool isSelectd)
    {
        if (isSelectd)
            _container.AddToClassList("select");
        else
            _container.RemoveFromClassList("select");
    }

    public void RefreshUI()
    {
        sprite.style.backgroundImage = new StyleBackground(targetStat.icon);
        titleLabel.text = targetStat.statName;
    }

    private void HandleDelete(ChangeEvent<string> evt)
    {
        evt.StopPropagation();
        OnDelete?.Invoke(this);
    }

    private void HandleItemSelect(ClickEvent evt)
    {
        OnSelect?.Invoke(this);
    }
}
