using UnityEngine;
public class UseMicButton : BaseButton {
    public MicVoiceGroup myGroup;
    // Start is called before the first frame update
    void Start () {
        base.Init ();
    }

    public override void OnClick () {
        if (Microphone.IsRecording (null))
            return;

        myGroup.StartCaptureVoice ();
    }
}