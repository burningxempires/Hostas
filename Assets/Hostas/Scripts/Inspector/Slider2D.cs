using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slider2D : MonoBehaviour, IPointerDownHandler, IDragHandler {
    public class Slider2DEvent : UnityEvent<Vector2> {

    }
    public RectTransform m_Rect { get { return (RectTransform) this.transform; } }
    public RectTransform handle;
    public float width { get { return m_Rect.sizeDelta.x; } }
    public float height { get { return m_Rect.sizeDelta.y; } }
    private Vector2 _value;
    public Vector2 Value {
        set {
            if (value != _value) {
                _value = value;
                NotifyPureValue (_value);
            }
        }
        get {
            return _value;
        }
    }
    public Slider2DEvent onValueChanged = new Slider2DEvent ();

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void OnPointerDown (PointerEventData eventData) {
        NotifyValue (eventData.position);
    }

    public void OnDrag (PointerEventData eventData) {
        NotifyValue (eventData.position);
    }

    void NotifyPureValue (Vector2 point) {
        var ap = point;
        ap.x = ap.x * (width * 0.5f);
        ap.y = ap.y * (height * 0.5f);
        if (ap.x > m_Rect.anchoredPosition.x + width / 2f) {
            ap.x = m_Rect.anchoredPosition.x + width / 2f;
        } else if (ap.x < m_Rect.anchoredPosition.x - width / 2f) {
            ap.x = m_Rect.anchoredPosition.x - width / 2f;
        }

        if (ap.y > m_Rect.anchoredPosition.y + height / 2f) {
            ap.y = m_Rect.anchoredPosition.y + height / 2f;
        } else if (ap.y < m_Rect.anchoredPosition.y - height / 2f) {
            ap.y = m_Rect.anchoredPosition.y - height / 2f;
        }
        handle.anchoredPosition = ap;

        if (onValueChanged != null) {
            onValueChanged.Invoke (point);
        }
    }
    void NotifyValue (Vector2 point) {

        handle.position = point;
        var ap = handle.anchoredPosition;
        if (ap.x > m_Rect.anchoredPosition.x + width / 2f) {
            ap.x = m_Rect.anchoredPosition.x + width / 2f;
        } else if (ap.x < m_Rect.anchoredPosition.x - width / 2f) {
            ap.x = m_Rect.anchoredPosition.x - width / 2f;
        }

        if (ap.y > m_Rect.anchoredPosition.y + height / 2f) {
            ap.y = m_Rect.anchoredPosition.y + height / 2f;
        } else if (ap.y < m_Rect.anchoredPosition.y - height / 2f) {
            ap.y = m_Rect.anchoredPosition.y - height / 2f;
        }
        handle.anchoredPosition = ap;
        _value = new Vector2 (
            handle.anchoredPosition.x / (width * 0.5f),
            handle.anchoredPosition.y / (height * 0.5f)
        );

        if (onValueChanged != null)
            onValueChanged.Invoke (_value);
    }
}