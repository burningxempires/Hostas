using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
//-80~20
public class VolumeSlider : BaseSlider {

    public override float defaultValue {
        get {
            return LiveHub.defCon.volume;
        }
    }
    float volume;
    public Text showVolume;
    public AudioMixer target { get { return LiveHub.MyHub.mixerGroup.audioMixer; } }
    // Start is called before the first frame update
    void Start () {
        base.Init ();
        //read value
        slider.value = LiveHub.config.volume;
        volume = slider.value;
        //target.SetFloat ("Volume", volume);
        showVolume.text = volume.ToString ("0.00") + "dB";
    }

    public override void OnValueChanged (float value) {
        volume = value;
        StoreValue ();
    }

    public override void OnResetClick () {
        slider.value = defaultValue;
        volume = slider.value;
        StoreValue ();
    }

    public override void StoreValue () {
        target.SetFloat ("Volume", volume);
        LiveHub.config.volume = volume;
        showVolume.text = volume.ToString ("0.00") + "dB";
    }
}