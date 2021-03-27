using UnityEngine;
using UnityEngine.UI;

public class ModelPositionSlider2D : BaseProp {
    Slider2D slider2D;
    protected Button resetBtn;
    public float rangeScale = 5f; //移動單位的倍率    
    public Vector2 defaultValue { get { return new Vector2 (LiveHub.defCon.positionX, LiveHub.defCon.positionY); } }
    public Vector2 purePosition;
    public Text showPos;
    // Start is called before the first frame update
    void Start () {
        slider2D = this.GetComponentInChildren<Slider2D> ();
        resetBtn = this.GetComponentInChildren<Button> ();
        slider2D.onValueChanged.AddListener (OnValueChanged);
        resetBtn.onClick.AddListener (OnResetClick);

        purePosition = new Vector2 (LiveHub.config.positionX, LiveHub.config.positionY);

        var p_store = purePosition;

        slider2D.Value = purePosition;

        LiveHub.MyHub.lastModel.transform.position = p_store * rangeScale;
        purePosition = p_store;
        //UpdateHint ();
        //InvokeRepeating ("onLateUpdate", 1f, 0.1f);
        StoreValue ();
    }

    void LateUpdate () {
        //var p_store = purePosition;
        var s_position = LiveHub.MyHub.lastModel.transform.position;
        purePosition = s_position / rangeScale;
        var store = purePosition;
        slider2D.Value = purePosition;

        LiveHub.MyHub.lastModel.transform.position = s_position;
        UpdateHint ();
        purePosition = store;
    }

    void UpdateHint () {
        showPos.text = "x:" +
            ((int) (purePosition.x * 100)) + "y:" +
            ((int) (purePosition.y * 100));
    }
    public void OnValueChanged (Vector2 value) {
        purePosition = value;
        StoreValue ();
    }

    public void OnResetClick () {
        slider2D.Value = defaultValue;
        purePosition = defaultValue;
        StoreValue ();
    }

    public void StoreValue () {
        UpdateHint ();
        LiveHub.MyHub.lastModel.transform.position = purePosition * rangeScale;
        LiveHub.config.positionX = purePosition.x;
        LiveHub.config.positionY = purePosition.y;
    }

    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate () {

        if (!base.validated) return;

        base.doValidate ();
    }
}