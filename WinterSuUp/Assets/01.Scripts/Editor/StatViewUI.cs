using System;
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

    public StatViewUI(TemplateContainer root, StatSO stat)
    {
        root.RegisterCallback<ClickEvent>(HandleItemSelect);
        targetStat = stat;

        sprite = root.Q<VisualElement>("Image");
        titleLabel = root.Q<Label>("StatTitle");
        deleteBtn = root.Q<Button>("DeleteBtn");
        deleteBtn.clicked += HandleDelete;

        RefreshUI();
    }

    private void RefreshUI()
    {
        sprite.style.backgroundImage = new StyleBackground(targetStat.icon);
        titleLabel.text = targetStat.statName;
    }

    private void HandleDelete()
    {
        
    }

    private void HandleItemSelect(ClickEvent evt)
    {
        OnSelect?.Invoke(this);
    }
}
