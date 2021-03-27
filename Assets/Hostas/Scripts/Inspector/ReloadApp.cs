using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadApp : BaseButton {
    void Start () {
        base.Init ();
    }

    public override void OnClick () {
        LiveHub.MyHub.SaveIni ();
        Scene scene = SceneManager.GetActiveScene ();
        SceneManager.LoadScene (scene.name);
    }
}