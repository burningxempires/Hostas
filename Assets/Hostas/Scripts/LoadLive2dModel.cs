using System;
using System.IO;
using Live2D.Cubism.Framework.Json;
using UnityEngine;
[DefaultExecutionOrder (-10)]
public class LoadLive2dModel : MonoBehaviour {

    //public string debugModelPath = Path.Combine (@"D:\Unity\Project2021\RisingHostas\Live2D Model\fire_rain_v2_vts\firerain-x.model3.json");
    //public string modelPath { get { return debugModelPath; } }
    public ModelAvatar modelAvatar;

    // Start is called before the first frame update
    public void Create (string modelPath) {
        var path = modelPath;
        var model3Json = CubismModel3Json.LoadAtPath (path, BuiltinLoadAssetAtPath);
        LiveHub.MyHub.Log ("load model", path);
        var model = model3Json.ToModel (true);
        var avatar = GetModelAvatar ();
        avatar.BuildModel (model, model3Json);

        //modelAvatar = avatar;

        //不知道為甚麼複製出來的model才能夠正常play表情,總之先暫時這樣處理
        var new_avatar = Instantiate (avatar.gameObject).GetComponent<ModelAvatar> ();
        new_avatar.transform.SetParent (this.transform);
        //Destroy (avatar.gameObject);

        avatar.transform.Translate (Vector3.up * 1000);
        avatar.gameObject.SetActive (false);

        modelAvatar = new_avatar;

        /*new_avatar = Instantiate (avatar.gameObject).GetComponent<ModelAvatar> ();
        new_avatar.transform.SetParent (this.transform);
        Destroy (modelAvatar);

        modelAvatar = new_avatar;*/

        //MotionButtonsManager.BindModel (modelAvatar);
    }

    ModelAvatar GetModelAvatar () {
        var go = new GameObject ("ModelAvatar");
        var ma = go.AddComponent<ModelAvatar> ();
        var trans = ma.transform;
        trans.SetParent (this.transform);
        trans.localPosition = Vector3.zero;
        return ma;
    }

    /// <summary>
    /// Load asset.
    /// </summary>
    /// <param name="assetType">Asset type.</param>
    /// <param name="absolutePath">Path to asset.</param>
    /// <returns>The asset on succes; <see langword="null"> otherwise.</returns>
    public static object BuiltinLoadAssetAtPath (Type assetType, string absolutePath) {
        if (assetType == typeof (byte[])) {
            return File.ReadAllBytes (absolutePath);
        } else if (assetType == typeof (string)) {
            return File.ReadAllText (absolutePath);
        } else if (assetType == typeof (Texture2D)) {
            var texture = new Texture2D (1, 1);
            texture.LoadImage (File.ReadAllBytes (absolutePath));

            return texture;
        }

        throw new NotSupportedException ();
    }

    // Update is called once per frame
    void Update () {

    }
}