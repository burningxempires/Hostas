using UnityEngine;
using UnityEngine.UI;

public class PropGroup : MonoBehaviour {
    public int GroupIndex;
    RectTransform m_Rect;
    public Button foldButton;
    public GameObject[] props;
    public bool isFolded = false;
    float fd_height;
    //public float ex_height;
    ContentSizeFitter sizeFitter;

    // Start is called before the first frame update
    public virtual void Start () {
        m_Rect = this.transform as RectTransform;
        foldButton.onClick.AddListener (OnClick);
        isFolded = false; //init value
        /*var last = props[props.Length - 1];
        var lr = last.transform as RectTransform;
        var yp = lr.anchoredPosition.y + lr.sizeDelta.y;

        var fr = foldButton.transform as RectTransform;
        var ft = fr.anchoredPosition.y - fr.sizeDelta.y;
        fd_height = fr.sizeDelta.y;
        ex_height = yp - ft;*/
        //要在這邊計算的話就去看ContentSizeFitter source code吧
        sizeFitter = GetComponent<ContentSizeFitter> ();
        var fr = foldButton.transform as RectTransform;
        var ft = fr.anchoredPosition.y - fr.sizeDelta.y;
        fd_height = fr.sizeDelta.y;
        //ex_height = m_Rect.sizeDelta.y;
        //Destroy (sizeFitter);
        InitIni ();
        //SetFolded (LiveHub.config.propGroupFoldeds[GroupIndex]);
        Invoke ("ReadIni", 0.1f);
    }

    void ReadIni () {
        SetFolded (LiveHub.config.propGroupFoldeds[GroupIndex]);
    }

    // Update is called once per frame
    void OnClick () {
        isFolded = !isFolded;
        SetFolded (isFolded);
    }

    void SetFolded (bool folded) {

        foreach (var item in props) {
            item.SetActive (!folded);
        }
        var size = m_Rect.sizeDelta;
        if (folded) {
            size.y = fd_height;
            sizeFitter.enabled = false;
        } else {
            size.y = 0;
            sizeFitter.enabled = true;
        }
        m_Rect.sizeDelta = size;
        LiveHub.config.propGroupFoldeds[GroupIndex] = folded;
    }

    void InitIni () {

        if (LiveHub.config.propGroupFoldeds.Count < GroupIndex + 1) {
            var ar = new bool[GroupIndex + 1 - LiveHub.config.propGroupFoldeds.Count];
            LiveHub.config.propGroupFoldeds.AddRange (ar);
        }
    }
}