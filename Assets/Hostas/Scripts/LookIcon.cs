using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class LookIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform clip;
    public Transform worldSpaceTarget;
    public Camera worldCamera { get { return LiveHub.MyHub.mainCamera; } }
    RectTransform canvasRectTransform;
    RectTransform cachedRectTransform;
    Vector2 clickOffset;
    bool isDragging = false;

    public Image m_Image;
    public Image iconImage;
    public Gradient gradient;
    // Use this for initialization
    void Start () {
        cachedRectTransform = this.transform as RectTransform;
        canvasRectTransform = (RectTransform) canvas.transform;
        worldSpaceTarget.position = LiveHub.config.centerPosition;
    }

    Vector2 temp;
    // Update is called once per frame
    void LateUpdate () {

        if (isDragging)
            return;

        Vector3 pos = cachedRectTransform.localPosition;

        Vector3 minPosition = clip.rect.min - cachedRectTransform.rect.min;
        Vector3 maxPosition = clip.rect.max - cachedRectTransform.rect.max;

        if (minPosition.x < maxPosition.x) {
            pos.x = Mathf.Clamp (cachedRectTransform.localPosition.x, minPosition.x, maxPosition.x);
        } else {
            pos.x = Mathf.Clamp (cachedRectTransform.localPosition.x, maxPosition.x, minPosition.x);
        }

        if (minPosition.y < maxPosition.y) {
            pos.y = Mathf.Clamp (cachedRectTransform.localPosition.y, minPosition.y, maxPosition.y);
        } else {
            pos.y = Mathf.Clamp (cachedRectTransform.localPosition.y, maxPosition.y, minPosition.y);
        }

        cachedRectTransform.localPosition = Vector3.Lerp (cachedRectTransform.localPosition, pos, Time.deltaTime * 5f);

    }

    void Update () {
        if (LiveHub.config.showLook) {
            var t = Time.time / 1f;
            var i = (int) t;
            var color = gradient.Evaluate (t - i);
            m_Image.color = iconImage.color = color;
        } else {
            var transparent = new Color (1, 1, 1, 0);
            m_Image.color = iconImage.color = transparent;
        }
        LiveHub.config.centerPosition = worldSpaceTarget.position;
        UpdateWorldSpaceTarget ();
    }

    // void OnGUI () {
    //     GUILayout.Label("W: " + Screen.width+" H: "+Screen.height);
    //     GUILayout.Label(cachedRectTransform.anchoredPosition.ToString());
    //     GUILayout.Label(Input.mousePosition.ToString());
    // }

    void UpdateWorldSpaceTarget () {

        if (!isDragging)
            return;

        var wp = worldCamera.ScreenToWorldPoint (Input.mousePosition);
        wp.z = 0;

        wp.x = -wp.x;
        wp.y = -wp.y;

        worldSpaceTarget.position = wp;

    }

    public void OnBeginDrag (PointerEventData eventData) {

        isDragging = true;

        Vector2 temp;

        var mousePos = Input.mousePosition;
        //var mousePos = UnityEngine.InputSystem.Mouse.current.position.ReadValue ();

        /*bool r = */
        RectTransformUtility.ScreenPointToLocalPointInRectangle (
            canvasRectTransform,
            mousePos,
            canvas.worldCamera,
            out temp
        );

        clickOffset = cachedRectTransform.anchoredPosition - temp;
    }

    public void OnDrag (PointerEventData eventData) {

        //cachedRectTransform.anchoredPosition += eventData.delta;

        var mousePos = Input.mousePosition;
        //var mousePos = UnityEngine.InputSystem.Mouse.current.position.ReadValue ();

        /*bool r = */
        RectTransformUtility.ScreenPointToLocalPointInRectangle (
            canvasRectTransform,
            mousePos,
            canvas.worldCamera,
            out temp
        );

        cachedRectTransform.anchoredPosition = temp + clickOffset;

    }

    public void OnEndDrag (PointerEventData eventData) {
        isDragging = false;
    }
}