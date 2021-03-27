using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[DefaultExecutionOrder (-250)]
public class LiveHub : MonoBehaviour {
    public int frameRate = 30;
    public Camera mainCamera;
    public AudioSource audioSource;
    public AudioMixerGroup mixerGroup { get { return audioSource.outputAudioMixerGroup; } }

    public Transform lookTarget;
    public RuntimeAnimatorController defaultAnimatorController;
    private static LiveHub myHub;
    public static LiveHub MyHub { get { return myHub; } }

    [Multiline (45)]
    public string debugLog;
    public ModelAvatar lastModel;
    public static readonly ConfigIni defCon = new ConfigIni ();
    public static ConfigIni config = new ConfigIni ();
    static string configName { get { return "config.json"; } }
    static string configPath { get { return Path.Combine (Path.GetDirectoryName (Application.dataPath), configName); } }
    public RectTransform inspector;
    public CanvasGroup inspectorAlpha;
    public Button inspectorButton;
    public float lastClickTime;
    public float doubleClickGate = 0.3f;
    public LoadLive2dModel loadLive2DModel;
    public GameObject modelInspector;
    public CanvasScaler canvasScaler;
    //public ModelSelector modelSelector;
    // Start is called before the first frame update
    void Awake () {
        myHub = this;
        LoadConfig ();
    }
    void Start () {
        Application.targetFrameRate = config.fps;
        canvasScaler.scaleFactor = config.uiScale;
        inspectorButton.onClick.AddListener (FlipInspector);
    }
    void FlipInspector () {
        //inspector.gameObject.SetActive (!inspector.gameObject.activeSelf);
        if (inspectorAlpha.alpha > 0) {
            inspectorAlpha.alpha = 0;
            inspectorAlpha.interactable = false;
            inspectorAlpha.blocksRaycasts = false;
        } else {
            inspectorAlpha.alpha = 1;
            inspectorAlpha.interactable = true;
            inspectorAlpha.blocksRaycasts = true;
        }
    }
    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            if (config.doubleClickCall) {
                if (Time.time - lastClickTime < doubleClickGate) {
                    FlipInspector ();
                }
            }
            lastClickTime = Time.time;
        }

        if (lastModel != null && lastModel.ExpressionDatas != null) {
            if (Input.GetKey (KeyCode.RightControl)) { //KeyCode.Alpha1 //KeyCode.Keypad1
                if (lastModel.ExpressionDatas.Count >= 1) {
                    if (Input.GetKeyDown (KeyCode.Keypad1)) {
                        lastModel.expressionPreview.ChangeExpression (lastModel.ExpressionDatas[0].expIndex);
                    }
                }
                if (lastModel.ExpressionDatas.Count >= 2) {
                    if (Input.GetKeyDown (KeyCode.Keypad2)) {
                        lastModel.expressionPreview.ChangeExpression (lastModel.ExpressionDatas[1].expIndex);
                    }
                }
                if (lastModel.ExpressionDatas.Count >= 3) {
                    if (Input.GetKeyDown (KeyCode.Keypad3)) {
                        lastModel.expressionPreview.ChangeExpression (lastModel.ExpressionDatas[2].expIndex);
                    }
                }
                if (lastModel.ExpressionDatas.Count >= 4) {
                    if (Input.GetKeyDown (KeyCode.Keypad4)) {
                        lastModel.expressionPreview.ChangeExpression (lastModel.ExpressionDatas[3].expIndex);
                    }
                }
                if (lastModel.ExpressionDatas.Count >= 5) {
                    if (Input.GetKeyDown (KeyCode.Keypad5)) {
                        lastModel.expressionPreview.ChangeExpression (lastModel.ExpressionDatas[4].expIndex);
                    }
                }
                if (lastModel.ExpressionDatas.Count >= 6) {
                    if (Input.GetKeyDown (KeyCode.Keypad6)) {
                        lastModel.expressionPreview.ChangeExpression (lastModel.ExpressionDatas[5].expIndex);
                    }
                }
                if (lastModel.ExpressionDatas.Count >= 7) {
                    if (Input.GetKeyDown (KeyCode.Keypad7)) {
                        lastModel.expressionPreview.ChangeExpression (lastModel.ExpressionDatas[6].expIndex);
                    }
                }
                if (lastModel.ExpressionDatas.Count >= 8) {
                    if (Input.GetKeyDown (KeyCode.Keypad8)) {
                        lastModel.expressionPreview.ChangeExpression (lastModel.ExpressionDatas[7].expIndex);
                    }
                }
                if (lastModel.ExpressionDatas.Count >= 9) {
                    if (Input.GetKeyDown (KeyCode.Keypad9)) {
                        lastModel.expressionPreview.ChangeExpression (lastModel.ExpressionDatas[8].expIndex);
                    }
                }
                if (Input.GetKeyDown (KeyCode.Keypad0)) {

                    lastModel.expressionPreview.ChangeExpression (-1);

                }
            }
        }
        debugLog = config.ToJson ();
    }
    void LoadConfig () {
        bool isExists = File.Exists (configPath);
        if (isExists) {
            var json = File.ReadAllText (configPath);
            config = JsonUtility.FromJson<ConfigIni> (json);
            Debug.Log ("config found. load config file " + configPath);
            if (config.propGroupFoldeds == null || config.propGroupFoldeds.Count == 0) {
                //目前6個prop group,如果要擴充group的話請手動擴充
                config.propGroupFoldeds = new List<bool> (6);
            }
        } else {
            config.fps = 30;
            config.positionX = 0;
            config.positionY = 0;
            config.scale = 1;
            config.backgroundColorHex = ColorUtility.ToHtmlStringRGBA (mainCamera.backgroundColor);
            config.showLook = false;
            config.followMouse = true;
            config.centerPosition = Vector3.zero;
            var json = config.ToJson ();
            File.WriteAllText (configPath, json);
            Debug.Log ("config not found. write new config file. " + configPath);
        }
    }

    public void Log (string title, string message) {
        this.debugLog += $"[{title}] {message} \n";
    }

    //幫你把訊息弄成像log的格式方便debug
    public string AsLog (string title, string message) {
        return $"[{title}] {message} \n";
    }

    /// <summary>
    /// Callback sent to all game objects before the application is quit.
    /// </summary>
    void OnApplicationQuit () {
        var json = config.ToJson ();
        File.WriteAllText (configPath, json);
        Debug.Log ("ApplicationQuit. save config file. " + configPath + "\n" + json);
    }

    public void SaveIni () {
        var json = config.ToJson ();
        File.WriteAllText (configPath, json);
        Debug.Log ("Save Ini. save config file. " + configPath + "\n" + json);
    }
}