using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class ConfigIni {
    public float uiScale = 1;
    public int fps = 30;
    public bool dockRight = true;
    public bool doubleClickCall = false;
    public float positionX = 0;
    public float positionY = 0;
    public float scale = 1;
    public float angle = 0;
    public float backgroundColorSliderValue = 0.5f;
    public string backgroundColorHex = "#ffeeeeff";
    public bool showLook = false;
    public bool followMouse = true;
    public Vector3 centerPosition = Vector3.zero;
    public float pitch = 1f;
    public float volume = 0f;
    public float lookScale = 1f;
    //public bool animatorEnabled = false;
    public bool lookControllerEnabled = true;
    public bool mouthControllerEnabled = true;
    public bool blinkControllerEnabled = true;
    public bool breathControllerEnabled = true;
    //prop groups
    /*public bool appGroupFolded = false;
    public bool transformGroupFolded = false;
    public bool lookFolded = false;
    public bool controllerGroupFolded = false;
    public bool aniExpGroupFolded = false;
    public bool micVoGroupFolded = false;*/

    //目前6個prop group,如果要擴充group的話請手動擴充
    public List<bool> propGroupFoldeds = new List<bool> (6);

    public string ToJson () {
        return JsonUtility.ToJson (this, true);
    }
}