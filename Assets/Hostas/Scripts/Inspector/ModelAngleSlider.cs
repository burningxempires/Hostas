using UnityEngine;
using UnityEngine.UI;

public class ModelAngleSlider : BaseSlider {
    public override float defaultValue {
        get {
            return LiveHub.defCon.angle;
        }
    }
    float angle;
    public Text showAngle;
    // Start is called before the first frame update
    void Start () {
        base.Init ();
        //read value
        slider.value = LiveHub.config.angle;
        angle = slider.value;
        showAngle.text = "∠" + angle.ToString ("000") + "°";
    }

    public override void OnValueChanged (float value) {
        angle = value;
        StoreValue ();
    }

    public override void OnResetClick () {
        slider.value = defaultValue;
        angle = slider.value;
        StoreValue ();
    }

    public override void StoreValue () {
        showAngle.text = "∠" + angle.ToString ("000") + "°";
        LiveHub.MyHub.lastModel.transform.localEulerAngles = new Vector3 (0, 0, angle);
        LiveHub.config.angle = angle;
    }

}