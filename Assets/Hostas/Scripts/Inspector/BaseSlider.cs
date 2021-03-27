using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSlider : BaseProp {

    protected Slider slider;
    protected Button resetBtn;
    public virtual float defaultValue { get; }
    public RectTransform defaultKnob;
    public float minValue = 0;
    public float maxValue = 1;
    public bool wholeNumbers = false;
    public virtual void Init () {
        //bind listener
        slider = this.GetComponentInChildren<Slider> ();
        resetBtn = this.GetComponentInChildren<Button> ();

        slider.onValueChanged.AddListener (OnValueChanged);
        resetBtn.onClick.AddListener (OnResetClick);
    }
    public virtual void OnValueChanged (float value) { }

    public virtual void OnResetClick () { }

    public virtual void StoreValue () { }

    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate () {

        if (!base.validated) return;

        base.doValidate ();

        if (slider == null) {
            slider = this.GetComponentInChildren<Slider> ();
        }

        if (defaultKnob == null) {
            var trans = this.transform.Find ("slider element/Slider/default value pos");
            defaultKnob = trans as RectTransform;
        }

        slider.minValue = this.minValue;
        slider.maxValue = this.maxValue;

        slider.value = defaultValue;

        defaultKnob.position = slider.handleRect.position;

        slider.wholeNumbers = this.wholeNumbers;
    }
}