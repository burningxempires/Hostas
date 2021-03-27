using UnityEngine;
using UnityEngine.UI;

public class FpsSlider : BaseSlider {
    public override float defaultValue {
        get {
            return LiveHub.defCon.fps;
        }
    }
    int fps;
    public Text showFps;
    // Start is called before the first frame update
    void Start () {
        base.Init ();
        //read value
        slider.value = LiveHub.config.fps;
        fps = (int) slider.value;
        showFps.text = fps.ToString ();
    }

    public override void OnValueChanged (float value) {
        fps = (int) value;
        StoreValue ();
    }

    public override void OnResetClick () {
        slider.value = defaultValue;
        fps = (int) slider.value;
        StoreValue ();
    }

    public override void StoreValue () {
        showFps.text = fps.ToString ();
        Application.targetFrameRate = fps;
        LiveHub.config.fps = fps;
    }

}