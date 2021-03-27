using UnityEngine;
using UnityEngine.UI;

public class BreathControllerSwitch : BaseSwitch {
    public override bool defaultValue { get { return LiveHub.defCon.breathControllerEnabled; } }
    // Start is called before the first frame update
    void Start () {
        base.Init ();
        var value = LiveHub.config.breathControllerEnabled;
        this.propToggle.isOn = value;
        if (value) {
            LiveHub.MyHub.lastModel.autoBreath.enabled = true;
        } else {
            LiveHub.MyHub.lastModel.autoBreath.enabled = false;
        }
    }

    public override void OnValueChanged (bool value) {
        base.OnValueChanged (value);
        this.propToggle.isOn = value;

        if (value) {
            LiveHub.MyHub.lastModel.autoBreath.enabled = true;
        } else {
            LiveHub.MyHub.lastModel.autoBreath.enabled = false;
        }
        StoreValue ();
    }

    public override void StoreValue () {
        LiveHub.config.breathControllerEnabled = this.propToggle.isOn;
    }
}