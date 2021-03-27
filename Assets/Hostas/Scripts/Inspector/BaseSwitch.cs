using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSwitch : BaseProp {
    protected Toggle propToggle;
    public GameObject checkMark;
    public virtual bool defaultValue { get; }
    // Start is called before the first frame update
    public virtual void Init () {
        propToggle = this.GetComponentInChildren<Toggle> ();
        propToggle.onValueChanged.AddListener (OnValueChanged);
        if (checkMark == null)
            checkMark = this.transform.Find ("switch element/Background/Checkmark").gameObject;
    }

    // Update is called once per frame
    public virtual void OnValueChanged (bool value) {
        checkMark.SetActive (value);
    }
    public virtual void StoreValue () { }
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate () {

        if (!base.validated) return;

        base.doValidate ();
        if (checkMark == null)
            checkMark = this.transform.Find ("switch element/Background/Checkmark").gameObject;
    }
}