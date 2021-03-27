using UnityEngine;
using UnityEngine.UI;

public class DoubleClickCall : BaseSwitch {

    public override bool defaultValue { get { return LiveHub.defCon.doubleClickCall; } }
    // Start is called before the first frame update
    void Start () {
        base.Init ();
        var value = LiveHub.config.doubleClickCall;
        this.propToggle.isOn = value;
        LiveHub.MyHub.inspectorButton.interactable = !value;
    }

    public override void OnValueChanged (bool value) {
        base.OnValueChanged (value);
        this.propToggle.isOn = value;
        LiveHub.MyHub.inspectorButton.interactable = !value;
        StoreValue ();
    }
    public override void StoreValue () {
        LiveHub.config.doubleClickCall = this.propToggle.isOn;
    }
}