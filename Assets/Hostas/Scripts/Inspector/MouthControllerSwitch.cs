using UnityEngine;
using UnityEngine.UI;

public class MouthControllerSwitch : BaseSwitch {
    public override bool defaultValue { get { return LiveHub.defCon.mouthControllerEnabled; } }
    // Start is called before the first frame update
    void Start () {
        base.Init ();
        var value = LiveHub.config.mouthControllerEnabled;
        this.propToggle.isOn = value;
        if (value) {
            LiveHub.MyHub.lastModel.mouthController.enabled = true;
            LiveHub.MyHub.lastModel.autoMouth.enabled = true;
        } else {
            LiveHub.MyHub.lastModel.mouthController.enabled = false;
            LiveHub.MyHub.lastModel.autoMouth.enabled = false;
        }
    }

    public override void OnValueChanged (bool value) {
        base.OnValueChanged (value);
        this.propToggle.isOn = value;

        if (value) {
            LiveHub.MyHub.lastModel.mouthController.enabled = true;
            LiveHub.MyHub.lastModel.autoMouth.enabled = true;
        } else {
            LiveHub.MyHub.lastModel.mouthController.enabled = false;
            LiveHub.MyHub.lastModel.autoMouth.enabled = false;
        }
        StoreValue ();
    }

    public override void StoreValue () {
        LiveHub.config.mouthControllerEnabled = this.propToggle.isOn;
    }
}