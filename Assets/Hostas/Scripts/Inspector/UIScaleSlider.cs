using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScaleSlider : BaseSlider {
    public override float defaultValue {
        get {
            return LiveHub.defCon.uiScale;
        }
    }
    float uiScale;
    public Text showScale;
    // Start is called before the first frame update
    void Start () {
        base.Init ();
        //read value
        slider.value = LiveHub.config.uiScale;
        uiScale = slider.value;
        showScale.text = "×" + uiScale.ToString ("0.0");
    }

    public override void OnValueChanged (float value) {
        uiScale = value;
        StoreValue ();
    }

    public override void OnResetClick () {
        slider.value = defaultValue;
        uiScale = slider.value;
        StoreValue ();
    }

    public override void StoreValue () {
        LiveHub.config.uiScale = uiScale;
        showScale.text = "×" + uiScale.ToString ("0.0");
        LiveHub.MyHub.canvasScaler.scaleFactor = uiScale;
    }
}