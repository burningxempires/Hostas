using UnityEngine;
using UnityEngine.UI;

public class BlinkControllerSwitch : BaseSwitch {
    public override bool defaultValue { get { return LiveHub.defCon.blinkControllerEnabled; } }
    // Start is called before the first frame update
    void Start () {
        base.Init ();
        var value = LiveHub.config.blinkControllerEnabled;
        this.propToggle.isOn = value;
        if (value) {
            LiveHub.MyHub.lastModel.autoBlink.enabled = true;
        } else {
            LiveHub.MyHub.lastModel.autoBlink.enabled = false;
        }
    }

    public override void OnValueChanged (bool value) {
        base.OnValueChanged (value);
        this.propToggle.isOn = value;

        if (value) {
            LiveHub.MyHub.lastModel.autoBlink.enabled = true;
        } else {
            LiveHub.MyHub.lastModel.autoBlink.enabled = false;
        }
        StoreValue ();
    }

    public override void StoreValue () {
        LiveHub.config.blinkControllerEnabled = this.propToggle.isOn;
    }
}