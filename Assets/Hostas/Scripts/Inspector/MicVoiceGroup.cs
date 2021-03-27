using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder (101)]

public class MicVoiceGroup : PropGroup {

    public int m_Frequency = 44100;
    public int m_Lenght = 4;
    public bool m_IsLoop = true;
    protected AudioSource m_AudioSource { get { return LiveHub.MyHub.audioSource; } }

    public Text deviceState;

    private void Awake () {
        //StartCaptureVoice ();
    }

    public void StartCaptureVoice () {
        if (Microphone.devices == null || Microphone.devices.Length == 0) {
            deviceState.text = "no device.";
            return;
        }

        if (m_AudioSource.clip != null)
            m_AudioSource.clip.UnloadAudioData ();

        m_AudioSource.clip = Microphone.Start (null, m_IsLoop, m_Lenght, m_Frequency);
        m_AudioSource.loop = m_IsLoop;
        while (Microphone.GetPosition (null) < 0) {

        }
        deviceState.text = "recording...";
        m_AudioSource.Play ();
    }

    public void StopCaptureVoice () {
        if (Microphone.IsRecording (null) == false)
            return;

        Microphone.End (null);
        m_AudioSource.Stop ();
    }
    /// <summary>
    /// Callback sent to all game objects before the application is quit.
    /// </summary>
    void OnApplicationQuit () {
        StopCaptureVoice ();
    }
}