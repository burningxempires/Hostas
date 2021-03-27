using UnityEngine;
using UnityEngine.UI;

public abstract class BaseProp : MonoBehaviour {
    public string propName = "prop name";
    public Text showPropName;
    [SerializeField] bool validate = false;
    public bool validated {
        get {
            if (!validate) return false;

            validate = false;
            return true;
        }
    }

    public virtual void doValidate () {
        if (showPropName == null) {
            showPropName = this.transform.Find ("Title Text").GetComponent<Text> ();
        }
        showPropName.text = propName;
    }
}