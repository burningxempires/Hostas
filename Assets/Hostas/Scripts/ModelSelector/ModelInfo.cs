using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class ModelInfo : MonoBehaviour {
    public RawImage modelImage;
    public Text modelName;
    public Button m_Button;
    ModelSelector _modelSelector;
    string fullModelPath;
    public void Bind (ModelSelector modelSelector, string modelPath) {
        fullModelPath = modelPath;
        _modelSelector = modelSelector;

        m_Button.onClick.AddListener (OnClick);

        var mn = Path.GetFileName (modelPath);
        modelName.text = mn;

        var pmn = Path.GetFileNameWithoutExtension (modelPath);
        var f = Path.GetDirectoryName (modelPath);

        var png = Path.Combine (ModelSelector.modelPath, f, pmn + ".png");
        var jpg = Path.Combine (ModelSelector.modelPath, f, pmn + ".jpg");

        Texture2D tex = null;
        byte[] fileData;
        try {
            if (File.Exists (png)) {
                fileData = File.ReadAllBytes (png);
                tex = new Texture2D (2, 2);
                tex.LoadImage (fileData);
                modelImage.color = Color.white;
            } else if (File.Exists (jpg)) {
                fileData = File.ReadAllBytes (jpg);
                tex = new Texture2D (2, 2);
                tex.LoadImage (fileData);
                modelImage.color = Color.white;
            } else {
                modelImage.color = Color.gray;
            }
        } catch (Exception e) {
            Debug.LogError (e.Message);
        }
        modelImage.texture = tex;

    }

    void OnClick () {
        _modelSelector.Select (this.fullModelPath);
    }
}