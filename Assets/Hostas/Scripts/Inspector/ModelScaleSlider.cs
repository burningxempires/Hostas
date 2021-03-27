using UnityEngine;
using UnityEngine.UI;

public class ModelScaleSlider : BaseSlider {
    public override float defaultValue {
        get {
            return LiveHub.defCon.scale;
        }
    }
    float scale;
    public Text showScale;
    // Start is called before the first frame update
    void Start () {
        base.Init ();
        //read value
        slider.value = LiveHub.config.scale;
        scale = slider.value;
        showScale.text = "×" + scale.ToString ("0.0");
    }

    public override void OnValueChanged (float value) {
        scale = value;
        StoreValue ();
    }

    public override void OnResetClick () {
        slider.value = defaultValue;
        scale = slider.value;
        StoreValue ();
    }

    public override void StoreValue () {
        showScale.text = "×" + scale.ToString ("0.0");
        LiveHub.MyHub.lastModel.transform.localScale = Vector3.one * scale;
        LiveHub.config.scale = scale;
    }
}