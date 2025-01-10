using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(RampImageGeneratorSO))]
public class CustomRampGenerator : Editor
{
    [SerializeField] private VisualTreeAsset viewAsset;

    private RampImageGeneratorSO _generator;
    private VisualElement _preView;

    public override VisualElement CreateInspectorGUI()
    {
        return base.CreateInspectorGUI();
    }

    private void InitializeContent(TemplateContainer contest)
    {
        _preView = contest.Q<VisualElement>("TexturePreView");
        contest.Q<Button>("").clicked += HandleGer;
        contest.Q<Button>("").clicked += HandleSave;
    }

    private void UpdatePreview()
    {

    }

    private void HandleSave()
    {
        byte[] bytes = _generator.dynamicTexture.EncodeToPNG();
        string savePath = Application.dataPath + _generator.savePath;

        if(System.IO.Directory.Exists(savePath))
        {
            System.IO.Directory.CreateDirectory(savePath);
        }

        System.IO.File.WriteAllBytes($"{savePath}/R_{Random.Range(0, 10000)}.png", bytes);


        AssetDatabase.Refresh();
    }



    private void HandleGer()
    {
        _generator.GenerateTexture();

    }
}
