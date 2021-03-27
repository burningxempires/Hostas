using UnityEngine;
using UnityEngine.UI;

public class DockRight : BaseSwitch {
    public RectTransform dockRight;
    public RectTransform dockLeft;
    public RectTransform inspector { get { return LiveHub.MyHub.inspector; } }
    public override bool defaultValue { get { return LiveHub.defCon.dockRight; } }
    public Toggle dockToggle { get { return base.propToggle; } }
    // Start is called before the first frame update
    void Start () {
        base.Init ();
        var value = LiveHub.config.dockRight;
        this.propToggle.isOn = value;
        if (value) {
            Dock (dockRight);
        } else {
            Dock (dockLeft);
        }
    }

    void Dock (RectTransform dir) {
        inspector.anchoredPosition = dir.anchoredPosition;
        inspector.anchorMin = dir.anchorMin;
        inspector.anchorMax = dir.anchorMax;
        inspector.pivot = dir.pivot;
    }

    public override void OnValueChanged (bool value) {
        base.OnValueChanged (value);
        this.propToggle.isOn = value;
        if (value) {
            Dock (dockRight);
        } else {
            Dock (dockLeft);
        }
        StoreValue ();
    }

    public override void StoreValue () {
        LiveHub.config.dockRight = this.propToggle.isOn;
    }
}