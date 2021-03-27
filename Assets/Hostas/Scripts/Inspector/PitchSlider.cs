using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
//1~300
public class PitchSlider : BaseSlider {

    public override float defaultValue {
        get {
            return LiveHub.defCon.pitch;
        }
    }
    float pitch;
    public Text showPitch;
    public AudioMixer target { get { return LiveHub.MyHub.mixerGroup.audioMixer; } }
    // Start is called before the first frame update
    void Start () {
        base.Init ();
        //read value
        slider.value = LiveHub.config.pitch;
        pitch = slider.value;
        //target.SetFloat ("Pitch", pitch);
        showPitch.text = (pitch * 100).ToString ("0.0") + "%";
    }

    public override void OnValueChanged (float value) {
        pitch = value;
        StoreValue ();
    }

    public override void OnResetClick () {
        slider.value = defaultValue;
        pitch = slider.value;
        StoreValue ();
    }

    public override void StoreValue () {
        target.SetFloat ("Pitch", pitch);
        LiveHub.config.pitch = pitch;
        showPitch.text = (pitch * 100).ToString ("0.0") + "%";
    }

}