using UnityEngine;
using System;
using UnityEngine.UIElements;


public class StatViewUI : MonoBehaviour
{
    public event Action<StatViewUI> onSelect;
    public event Action<StatViewUI> onDelete;

    public VisualElement sprite;
    public Label titleLabal;
    public Button deleteBtn;

    public StatSO targetStat;

    public StatViewUI(TemplateContainer root, StatSO stat)
    {
        root.RegisterCallback<ClickEvent>(HandleItemSelect);
        targetStat = stat;

        sprite = root.Q<VisualElement>("Image");
        titleLabal = root.Q<Label>("StatTitle");
        deleteBtn = root.Q<Button>("DeleteBtn");
        deleteBtn.clicked += HandleDelete;


    }

    private void RefreshUI()
    {
        sprite.style.backgroundImage = new StyleBackground(targetStat.icon);
        titleLabal.text = "";
    }

    private void HandleDelete()
    {

    }

    private void HandleItemSelect(ClickEvent evt)
    {

    }
}
