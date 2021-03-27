using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//test
public class MotionButtonsManager : MonoBehaviour {
    public Transform buttonsRoot;
    public Button prefabButton;
    static MotionButtonsManager Instance;
    void Awake () {
        Instance = this;
    }
    void bindModel (ModelAvatar modelAvatar) {
        if (buttonsRoot.childCount > 0) {
            for (int i = buttonsRoot.childCount - 1; i >= 0; i--) {
                var c = buttonsRoot.GetChild (i);
                Destroy (c.gameObject);
            }
        }

        var datas = modelAvatar.MotionDatas;
        foreach (var data in datas) {
            var btn = Instantiate<Button> (prefabButton, buttonsRoot);
            btn.GetComponentInChildren<Text> ().text = data.moName;
            btn.onClick.AddListener (() => {
                modelAvatar.motionPreview.PlayAnimation (data.anim);
            });
        }

        var exps = modelAvatar.ExpressionDatas;
        foreach (var exp in exps) {
            var btn = Instantiate<Button> (prefabButton, buttonsRoot);
            btn.GetComponentInChildren<Text> ().text = exp.expName;
            btn.onClick.AddListener (() => {
                modelAvatar.expressionPreview.ChangeExpression (exp.expIndex);
            });
        }

        var xbtn = Instantiate<Button> (prefabButton, buttonsRoot);
        xbtn.GetComponentInChildren<Text> ().text = "No Motion";
        xbtn.onClick.AddListener (() => {
            //modelAvatar.motionPreview.PlayAnimation (null);
            modelAvatar.motionController.StopAllAnimation ();
        });

        xbtn = Instantiate<Button> (prefabButton, buttonsRoot);
        xbtn.GetComponentInChildren<Text> ().text = "No Exp";
        xbtn.onClick.AddListener (() => {
            modelAvatar.expressionPreview.ChangeExpression (-1);
        });
    }

    public static void BindModel (ModelAvatar modelAvatar) {
        Instance.bindModel (modelAvatar);
    }
}