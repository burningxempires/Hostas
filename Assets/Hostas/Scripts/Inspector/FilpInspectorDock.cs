using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FilpInspectorDock : MonoBehaviour, IPointerClickHandler {
    DockRight dockRight;
    public void OnPointerClick (PointerEventData eventData) {
        if (dockRight == null) {
            dockRight = FindObjectOfType<DockRight> ();
        }

        if (dockRight == null) return;

        dockRight.dockToggle.isOn = !dockRight.dockToggle.isOn;

    }
}