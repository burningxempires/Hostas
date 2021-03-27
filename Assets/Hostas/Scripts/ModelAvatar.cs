using Object = UnityEngine.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Live2D.Cubism;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;
using Live2D.Cubism.Framework.Expression;
using Live2D.Cubism.Framework.HarmonicMotion;
using Live2D.Cubism.Framework.Json;
using Live2D.Cubism.Framework.LookAt;
using Live2D.Cubism.Framework.Motion;
using Live2D.Cubism.Framework.MotionFade;
using Live2D.Cubism.Framework.MouthMovement;
using Live2D.Cubism.Framework.Pose;
using Live2D.Cubism.Samples.OriginalWorkflow.Expression;
using Live2D.Cubism.Samples.OriginalWorkflow.Motion;
using UnityEngine;

public class ModelAvatar : MonoBehaviour {

    private CubismModel m_model;
    public CubismModel cubismModel { get { return m_model; } }
    public List<Object> loadedObjects = new List<Object> (); //用來測試
    public List<ExpressionData> ExpressionDatas = new List<ExpressionData> ();
    public List<MotionData> MotionDatas = new List<MotionData> ();
    public Animator modelAnimator;
    public CubismAutoEyeBlinkInput autoBlink;
    public CubismMouthController mouthController;
    public CubismAudioMouthInput autoMouth;
    public CubismLookController autoLook;
    public CubismHarmonicMotionController autoBreath;

    #region live2d controllers
    public CubismUpdateController updateController;
    //parameter store
    public CubismParameterStore parameterStore;
    //pose controller
    public CubismPoseController poseController;
    //fade controller
    public CubismFadeController fadeController;
    #endregion
    #region live2d motion
    //expression controller
    public CubismExpressionController expressionController;
    public CubismMotionController motionController;
    public CubismExpressionPreview expressionPreview;
    public MotionPreview motionPreview;
    #endregion

    static LiveHub myHub { get { return LiveHub.MyHub; } }

    #region key parameters
    readonly string kParamBreath = "ParamBreath";

    readonly string kParamAngleX = "ParamAngleX";
    readonly string kParamAngleY = "ParamAngleY";
    readonly string kParamAngleZ = "ParamAngleZ";

    readonly string kParamEyeBallX = "ParamEyeBallX";
    readonly string kParamEyeBallY = "ParamEyeBallY";

    readonly string kParamBodyAngleX = "ParamBodyAngleX";
    readonly string kParamBodyAngleY = "ParamBodyAngleY";
    readonly string kParamBodyAngleZ = "ParamBodyAngleZ";
    #endregion

    // Start is called before the first frame update
    void Awake () {
        myHub.lastModel = this;
    }

    // Update is called once per frame
    void Update () {

    }

    public void BuildModel (CubismModel model, CubismModel3Json model3Json) {
        m_model = model;
        model.transform.SetParent (this.transform);
        model.transform.localPosition = Vector3.zero;

        //由json載入的模型是沒有這些自動controller的
        //所以要用腳本把這些controller綁上去

        //要注意的是載入的模型已經在標記為sync的部分將
        //parameter的blink和mouth都標記上去了
        //注意不需要重複標記(多綁似乎也不會有什麼影響)

        //取得參數的方法
        //var parameters = m_model.Parameters;

        //animator       
        BindAnimator (model, model3Json);

        //--//shouldImportAsOriginalWorkflow
        //update controller
        //parameter store
        //pose controller
        //expression controller
        //fade controller
        BindLive2d (model);

        //expression
        BindExpression (model, model3Json);

        //blink
        BindBlink (model);

        //mouth
        BindMouth (model);

        //look
        BindLook (model);

        //breath
        BindBreath (model);

        //可以用滑鼠移動模型
        BindMove (model);

        // Make sure model is 'fresh'
        //BindFinal ();

    }

    void BindMove (CubismModel model) {
        var move = BindController<MoveModelAvatar> (this.gameObject);
        move.Bind (model);
    }

    void BindFinal () { //本來要用這個方法處理表情不能正確顯示的問題,但是現在用複製的方法解決了,這個方法暫時沒有用了
        // Make sure model is 'fresh'        
        //m_model.ForceUpdateNow ();
        //parameterStore.Refresh ();
        parameterStore.SaveParameters ();
        //updateController.Refresh ();
        //model.ForceUpdateNow ();
    }

    void BindAnimator (CubismModel model, CubismModel3Json model3Json) {

        modelAnimator = model.GetComponent<Animator> ();

        var logString = "[bind log]\n";

        var path = model3Json.AssetPath;
        logString += myHub.AsLog ("model3Json.AssetPath", path);
        var dpath = Path.GetDirectoryName (path);
        logString += myHub.AsLog ("GetDirectoryName", dpath);
        var motions = model3Json.FileReferences.Motions.Motions;
        if (motions == null) {
            //no motion
            return;
        }

        List<CubismMotion3Json> motion3Jsons = new List<CubismMotion3Json> ();
        List<string> motion3Names = new List<string> ();
        List<AnimationClip> motionClips = new List<AnimationClip> ();

        for (int i = 0; i < motions.Length; i++) {
            for (int j = 0; j < motions[i].Length; j++) {
                var mo = motions[i][j];
                logString += myHub.AsLog ("motion", $"[{i}][{j}] " + mo.File);
                var mp = Path.Combine (dpath, mo.File);

                //參考
                //https://docs.live2d.com/cubism-sdk-manual/json-unity/

                //load text
                var json = File.ReadAllText (mp);
                var motion3Json = CubismMotion3Json.LoadFrom (json);
                motion3Jsons.Add (motion3Json);
                // Initialize
                //var animationClip = motion3Json.ToAnimationClip ();
                // Original Workflow
                var animationClipForOW = motion3Json.ToAnimationClip (true, false, true, null); //no pose3Json
                animationClipForOW.legacy = false;

                var fn = Path.GetFileNameWithoutExtension (mo.File);
                animationClipForOW.name = fn;

                //把clip託管到清單,銷毀整個物件的話這裡的清單也會跟著消除
                //應該是沒有洩漏問題
                loadedObjects.Add (animationClipForOW);
                motionClips.Add (animationClipForOW);
                motion3Names.Add (fn);
                MotionDatas.Add (
                    new MotionData () {
                        motionI = i, motionJ = j, moName = fn, anim = animationClipForOW
                    }
                );
            }
        }
        Debug.Log (logString);

        BindFadeList (model, model3Json, motion3Jsons, motion3Names, motionClips);

        motionPreview = BindController<MotionPreview> (model.gameObject);
        motionController = BindController<CubismMotionController> (model.gameObject);
        //modelAnimator = model.GetComponent<Animator> ();
        //modelAnimator.runtimeAnimatorController = myHub.defaultAnimatorController;
    }

    void BindLive2d (CubismModel model) {

        var go = model.gameObject;

        //update controller
        updateController = BindController<CubismUpdateController> (go);
        //parameter store
        parameterStore = BindController<CubismParameterStore> (go);
        //pose controller
        poseController = BindController<CubismPoseController> (go);
    }

    T BindController<T> (GameObject go) where T : MonoBehaviour {
        var com = go.GetComponent<T> ();

        if (com == null)
            com = go.AddComponent<T> ();

        return com;
    }

    void BindFadeList (CubismModel model, CubismModel3Json model3Json,
        List<CubismMotion3Json> motion3Jsons,
        List<string> montion3Names, List<AnimationClip> motionClips) {

        //List<CubismFadeMotionData> datas = new List<CubismFadeMotionData> ();

        var fadeMotions = ScriptableObject.CreateInstance<CubismFadeMotionList> ();
        fadeMotions.MotionInstanceIds = new int[0];

        for (int i = 0; i < motion3Jsons.Count; i++) {
            var m3j = motion3Jsons[i];
            var m3n = montion3Names[i];
            var mc = motionClips[i];

            var fadeMotion = CubismFadeMotionData.CreateInstance (
                m3j,
                m3n,
                mc.length,
                true, //ShouldImportAsOriginalWorkflow
                false); //ShouldClearAnimationCurves

            fadeMotion.name = Path.GetFileNameWithoutExtension (fadeMotion.MotionName);

            /**********/

            var instanceId = 0;
            var isExistInstanceId = false;
            var events = mc.events;
            for (var k = 0; k < events.Length; ++k) {
                if (events[k].functionName != "InstanceId") {
                    continue;
                }

                instanceId = events[k].intParameter;
                isExistInstanceId = true;
                break;
            }

            if (!isExistInstanceId) {
                instanceId = mc.GetInstanceID ();
            }

            var motionIndex = Array.IndexOf (fadeMotions.MotionInstanceIds, instanceId);
            if (motionIndex != -1) {
                fadeMotions.CubismFadeMotionObjects[motionIndex] = fadeMotion;
            } else {
                motionIndex = fadeMotions.MotionInstanceIds.Length;

                Array.Resize (ref fadeMotions.MotionInstanceIds, motionIndex + 1);
                fadeMotions.MotionInstanceIds[motionIndex] = instanceId;

                Array.Resize (ref fadeMotions.CubismFadeMotionObjects, motionIndex + 1);
                fadeMotions.CubismFadeMotionObjects[motionIndex] = fadeMotion;
            }

            /**********/
            // Add animation event
            //... 如果要加入event請參考importer
            var animationClip = mc;
            //
            {
                var sourceAnimationEvents = mc.events; //AnimationUtility.GetAnimationEvents (animationClip);
                var index = -1;

                for (var _i = 0; _i < sourceAnimationEvents.Length; ++_i) {
                    if (sourceAnimationEvents[_i].functionName != "InstanceId") {
                        continue;
                    }

                    index = _i;
                    break;
                }

                if (index == -1) {
                    index = sourceAnimationEvents.Length;
                    Array.Resize (ref sourceAnimationEvents, sourceAnimationEvents.Length + 1);
                    sourceAnimationEvents[sourceAnimationEvents.Length - 1] = new AnimationEvent ();
                }

                sourceAnimationEvents[index].time = 0;
                sourceAnimationEvents[index].functionName = "InstanceId";
                sourceAnimationEvents[index].intParameter = instanceId;
                sourceAnimationEvents[index].messageOptions = SendMessageOptions.DontRequireReceiver;

                //AnimationUtility.SetAnimationEvents (animationClip, sourceAnimationEvents);
                animationClip.events = sourceAnimationEvents;
            }
            //datas.Add (fadeMotion);
        }

        //fade controller
        fadeController = BindController<CubismFadeController> (model.gameObject);

        //fadeMotions.CubismFadeMotionObjects = new CubismFadeMotionData[0];
        //?fadeMotions.CubismFadeMotionObjects = datas.ToArray (); //不需要了 上面有賦值

        fadeController.CubismFadeMotionList = fadeMotions;
    }

    void BindExpression (CubismModel model, CubismModel3Json model3Json) {
        //expression controller
        expressionController = BindController<CubismExpressionController> (model.gameObject);

        //get expression json data path
        var exp3jsons = model3Json.Expression3Jsons;
        if (exp3jsons == null) {
            //no expression
            return;
        }

        //load json data to expression
        var expressionList = ScriptableObject.CreateInstance<CubismExpressionList> ();
        expressionList.name = "Expression List";
        loadedObjects.Add (expressionList);
        expressionList.CubismExpressionObjects = new CubismExpressionData[exp3jsons.Length];

        var logString = "[bind log]\n";
        int i = 0;
        foreach (var e3j in exp3jsons) {
            var edata = CubismExpressionData.CreateInstance (e3j);
            edata.name = model3Json.FileReferences.Expressions[i].Name;

            loadedObjects.Add (edata);

            ExpressionDatas.Add (
                new ExpressionData () {
                    expIndex = i, expName = edata.name
                }

            );

            var expName = model3Json.FileReferences.Expressions[i].Name;
            expressionList.CubismExpressionObjects[i] = edata;
            logString += myHub.AsLog ("exp", i + " : " + expName + " " + edata.FadeInTime + "-" + edata.FadeOutTime);
            i++;
        }

        Debug.Log (logString); //log的結果撥放的exp和index是正確的

        //bind into controller
        expressionController.ExpressionsList = expressionList;

        expressionPreview = BindController<CubismExpressionPreview> (model.gameObject);
    }

    void BindBlink (CubismModel model) {
        //var blinkController = model.GetComponent<CubismEyeBlinkController> ();
        var blinkController = BindController<CubismEyeBlinkController> (model.gameObject);
        autoBlink = model.gameObject.AddComponent<CubismAutoEyeBlinkInput> ();
        //blinkController.Refresh ();

        //不清楚為甚麼runtime載入眼睛會閉上,另外一個專案用同一個模型
        //把模型import之後再輸出卻是正常的,暫時用Override來取得效果
        //blinkController.BlendMode = CubismParameterBlendMode.Override;
        //採用複製方案後正常了,先不改吧
    }

    void BindMouth (CubismModel model) {
        //mouthController = model.GetComponent<CubismMouthController> ();
        mouthController = BindController<CubismMouthController> (model.gameObject);
        autoMouth = model.gameObject.AddComponent<CubismAudioMouthInput> ();
        autoMouth.AudioInput = myHub.audioSource;
        //mouthController.Refresh ();
        //啟用麥克風的話用Override效果會比較好
        //Addtive會導致和表情的嘴巴大小相加,嘴巴容易過大
        //Multiply因為嘴巴預設是關閉的,相乘嘴巴會打不開
        //最好的辦法是用一個Idle的表情張開嘴巴然後和麥克風相乘(大概)
        //如果要讓表情正常顯示就要把這個值做適當的修改
        mouthController.BlendMode = CubismParameterBlendMode.Override;

        //原本的數值嘴巴張不太開頻率也過高,因此用這個數值替代
        autoMouth.Smoothing = 0.7f;
        autoMouth.Gain = 7f;
    }

    void BindLook (CubismModel model) {

        var parameters = m_model.Parameters;

        var AngX = parameters.Where (data => data.Id == kParamAngleX).FirstOrDefault ();
        if (AngX != null) BindLookParameter (AngX, CubismLookAxis.X);
        var AngY = parameters.Where (data => data.Id == kParamAngleY).FirstOrDefault ();
        if (AngY != null) BindLookParameter (AngY, CubismLookAxis.Y);
        var AngZ = parameters.Where (data => data.Id == kParamAngleZ).FirstOrDefault ();
        if (AngZ != null) BindLookParameter (AngZ, CubismLookAxis.X); //Z軸的話會變成固定的數值

        var EyeBallX = parameters.Where (data => data.Id == kParamEyeBallX).FirstOrDefault ();
        if (EyeBallX != null) BindLookParameter (EyeBallX, CubismLookAxis.X);
        var EyeBallY = parameters.Where (data => data.Id == kParamEyeBallY).FirstOrDefault ();
        if (EyeBallY != null) BindLookParameter (EyeBallY, CubismLookAxis.Y);

        var BodyX = parameters.Where (data => data.Id == kParamBodyAngleX).FirstOrDefault ();
        if (BodyX != null) BindLookParameter (BodyX, CubismLookAxis.X);

        var BodyY = parameters.Where (data => data.Id == kParamBodyAngleY).FirstOrDefault ();
        if (BodyY != null) BindLookParameter (BodyY, CubismLookAxis.Y);

        var BodyZ = parameters.Where (data => data.Id == kParamBodyAngleZ).FirstOrDefault ();
        if (BodyZ != null) BindLookParameter (BodyZ, CubismLookAxis.X);

        autoLook = model.gameObject.AddComponent<CubismLookController> ();
        var lookTarget = model.gameObject.AddComponent<LookTarget> ();
        autoLook.Target = lookTarget;
        autoLook.Center = myHub.lookTarget;
        //不清楚為甚麼Mutiply會導致晃動還有addtive會導致移動過大
        //暫時用Override取得想要的效果
        //autolook.BlendMode = CubismParameterBlendMode.Override;
        //autolook.Refresh ();
    }

    void BindLookParameter (CubismParameter parameter, CubismLookAxis axis) {
        var tempP = parameter.gameObject.AddComponent<CubismLookParameter> ();
        tempP.Axis = axis;
        tempP.Factor = parameter.MaximumValue;
    }

    void BindBreath (CubismModel model) {
        autoBreath = BindController<CubismHarmonicMotionController> (model.gameObject);

        var parameters = m_model.Parameters;

        var breathQ = from data in parameters where data.Id == kParamBreath
        select data;

        if (breathQ.Count () != 0) {
            var parameter = breathQ.First ();
            var breathingParameter = parameter.gameObject.AddComponent<CubismHarmonicMotionParameter> ();
            breathingParameter.Direction = CubismHarmonicMotionDirection.Centric;
        }

        autoBreath.Refresh ();

        //從CubismHarmonicMotionController的Reset複製過來,因為那個Reset是private的不能直接拿來用
        var DefaultChannelCount = 1;
        // Reset/Initialize channel timescales.
        autoBreath.ChannelTimescales = new float[DefaultChannelCount];

        for (var s = 0; s < DefaultChannelCount; ++s) {
            autoBreath.ChannelTimescales[s] = 1f;
        }

    }

    [Serializable]
    public struct ExpressionData {
        public int expIndex;
        public string expName;
    }

    [Serializable]
    public struct MotionData {
        public int motionI;
        public int motionJ;
        public string moName;
        public AnimationClip anim;

    }
}