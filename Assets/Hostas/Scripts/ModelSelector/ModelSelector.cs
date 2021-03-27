using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ModelSelector : MonoBehaviour {
    public ModelInfo modelInfoPrefab;
    public RectTransform root;
    static string modelExtension { get { return ".model3.json"; } }
    public static string modelPath { get { return Path.Combine (Path.GetDirectoryName (Application.dataPath), "Live2D Model"); } }
    public Text folderState;

    //TODO:refresh button

    // Start is called before the first frame update
    void Start () {
        //check if model folder exists

        if (!Directory.Exists (modelPath)) {
            folderState.text = "model not found. please place your model in " + modelPath;
            Directory.CreateDirectory (modelPath);
            Debug.Log ("model not found. CreateDirectory " + modelPath);
            return;
        }

        //gather all model json file in tier 1
        var dirs = Directory.GetDirectories (modelPath);

        List<string> modelFiles = new List<string> ();
        foreach (var dir in dirs) {
            var fp = Directory.GetFiles (dir, "*" + modelExtension);
            modelFiles.AddRange (fp);
        }

        // creat buttons

        foreach (var mf in modelFiles) {
            //Debug.Log ("TODO:creat load model button");
            var btn = Instantiate<ModelInfo> (modelInfoPrefab, root);
            btn.Bind (this, mf);
        }

        folderState.text = modelFiles.Count + "models found.";
    }

    public void Select (string modelPath) {
        LiveHub.MyHub.loadLive2DModel.Create (modelPath);
        LiveHub.MyHub.modelInspector.SetActive (true);
        this.gameObject.SetActive (false);
    }
}