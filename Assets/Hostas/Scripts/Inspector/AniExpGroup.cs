using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder (101)]
public class AniExpGroup : PropGroup {
    public Transform buttonsRoot { get { return this.transform; } }
    public ExpButton expButtonPrefab;
    public MotionButton motionButtonPrefab;
    public override void Start () {
        base.Start ();
        bindModel (LiveHub.MyHub.lastModel);
        List<GameObject> _props = new List<GameObject> ();
        for (int i = 1; i < buttonsRoot.childCount; i++) {
            _props.Add (buttonsRoot.GetChild (i).gameObject);
        }
        props = _props.ToArray ();
    }

    void bindModel (ModelAvatar modelAvatar) {

        var exps = modelAvatar.ExpressionDatas;
        foreach (var exp in exps) {
            var btn = Instantiate<ExpButton> (expButtonPrefab, buttonsRoot);
            btn.expressionData = exp;
            btn.gameObject.SetActive (true);
        }

        var datas = modelAvatar.MotionDatas;
        foreach (var data in datas) {
            var btn = Instantiate<MotionButton> (motionButtonPrefab, buttonsRoot);
            btn.motionData = data;
            btn.gameObject.SetActive (true);
        }
        //expButtonPrefab.gameObject.SetActive (false);
        //motionButtonPrefab.gameObject.SetActive (false);
    }
}