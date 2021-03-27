using UnityEngine;
using UnityEngine.UI;

public class AppBgColorSlider : BaseSlider {
    public Gradient gradient;
    public Image showColor;
    Camera _camera { get { return LiveHub.MyHub.mainCamera; } }
    public override float defaultValue {
        get {
            return LiveHub.defCon.backgroundColorSliderValue;
        }
    }
    // Start is called before the first frame update
    void Start () {
        base.Init ();
        //read value
        var hex = LiveHub.config.backgroundColorHex;
        Color color = _camera.backgroundColor;
        var result = ColorUtility.TryParseHtmlString (hex, out color);
        if (result) {
            _camera.backgroundColor = color;
        } else {
            var value = LiveHub.config.backgroundColorSliderValue;
            var bgColor = gradient.Evaluate (value);
            slider.value = value;
            _camera.backgroundColor = bgColor;
        }
        showColor.color = _camera.backgroundColor;
    }
    public override void OnValueChanged (float value) {
        _camera.backgroundColor = gradient.Evaluate (value);
        showColor.color = _camera.backgroundColor;
        StoreValue ();
    }

    public override void OnResetClick () {
        slider.value = defaultValue;
        _camera.backgroundColor = gradient.Evaluate (slider.value);
        showColor.color = _camera.backgroundColor;
        StoreValue ();
    }

    public override void StoreValue () {
        var value = slider.value;
        Color color = _camera.backgroundColor;
        var result = ColorUtility.ToHtmlStringRGBA (color);

        LiveHub.config.backgroundColorSliderValue = value;
        LiveHub.config.backgroundColorHex = result;
    }

}