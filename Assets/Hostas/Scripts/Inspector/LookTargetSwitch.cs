using UnityEngine;
using UnityEngine.UI;

public class LookTargetSwitch : BaseSwitch {
    public override bool defaultValue { get { return LiveHub.defCon.followMouse; } }

    // Start is called before the first frame update
    void Start () {
        base.Init ();
        var value = LiveHub.config.followMouse;
        this.propToggle.isOn = value;
        /*if (value) {
            LiveHub.MyHub.lastModel.autoLook.enabled = true;
        } else {
            LiveHub.MyHub.lastModel.autoLook.enabled = false;
        }*/
    }

    public override void OnValueChanged (bool value) {
        base.OnValueChanged (value);
        this.propToggle.isOn = value;

        //讓LookTarget控制,這裡只更新config參數
        /*if (value) {
            LiveHub.MyHub.lastModel.autoLook.enabled = true;
        } else {
            LiveHub.MyHub.lastModel.autoLook.enabled = false;
        }*/
        StoreValue ();
    }

    public override void StoreValue () {
        LiveHub.config.followMouse = this.propToggle.isOn;
    }
}