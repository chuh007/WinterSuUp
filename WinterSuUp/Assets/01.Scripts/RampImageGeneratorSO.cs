using UnityEngine;

[CreateAssetMenu(fileName = "RampImageGeneratorSO", menuName = "SO/RampImageGeneratorSO")]
public class RampImageGeneratorSO : ScriptableObject
{
    public Gradient rampColor;
    public Texture2D dynamicTexture;
    public string savePath;

    public void GenerateTexture()
    {
        if(dynamicTexture == null)
        {
            dynamicTexture = new Texture2D(256,1, TextureFormat.ARGB32, false);
        }

        Color[] colorBuffer = dynamicTexture.GetPixels();

        for(int i = 0; i < 256; i++)
        {
            colorBuffer[i].r = rampColor.Evaluate(i / 255f).r;
            colorBuffer[i].g = rampColor.Evaluate(i / 255f).g;
            colorBuffer[i].b = rampColor.Evaluate(i / 255f).b;
            colorBuffer[i].a = rampColor.Evaluate(i / 255f).a;
        }

        dynamicTexture.SetPixels(colorBuffer);
        dynamicTexture.Apply();
        dynamicTexture.wrapMode = TextureWrapMode.Clamp;
    }
}
