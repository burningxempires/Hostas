using UnityEngine;
using UnityEngine.UI;

public class LookScaleSlider : BaseSlider {
    public override float defaultValue {
        get {
            return LiveHub.defCon.lookScale;
        }
    }
    float lookScale;
    public Text showLookScale;
    // Start is called before the first frame update
    void Start () {
        base.Init ();
        //read value
        slider.value = LiveHub.config.lookScale;
        lookScale = slider.value;
        showLookScale.text = "×" + lookScale.ToString ("0.0");
    }

    public override void OnValueChanged (float value) {
        lookScale = value;
        StoreValue ();
    }

    public override void OnResetClick () {
        slider.value = defaultValue;
        lookScale = slider.value;
        StoreValue ();
    }

    public override void StoreValue () {
        showLookScale.text = "×" + lookScale.ToString ("0.0");
        LiveHub.config.lookScale = lookScale;
    }
}