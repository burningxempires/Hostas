using UnityEngine;
using UnityEngine.UI;
public abstract class BaseButton : BaseProp {
    protected Button propButton;
    public Image buttonIcon;
    public Sprite icon;
    public virtual void Init () {
        propButton = this.GetComponentInChildren<Button> ();
        propButton.onClick.AddListener (OnClick);
    }
    public virtual void OnClick () {

    }
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate () {

        if (!base.validated) return;

        base.doValidate ();
        if (buttonIcon == null) {
            buttonIcon = this.transform.Find ("button element/Image").GetComponent<Image> ();
        }

        if (icon != null && buttonIcon != null) {
            buttonIcon.sprite = icon;
        }

    }
}