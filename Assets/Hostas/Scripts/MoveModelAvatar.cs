using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Raycasting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class MoveModelAvatar : MonoBehaviour {
    CubismModel Model;
    /// <summary>
    /// <see cref="CubismRaycaster"/> attached to <see cref="Model"/>.
    /// </summary>
    public CubismRaycaster Raycaster;

    /// <summary>
    /// Buffer for raycast results.
    /// </summary>
    public CubismRaycastHit[] Results;
    // Start is called before the first frame update
    void Start () {
        //Raycaster = Model.GetComponent<CubismRaycaster> ();
        Results = new CubismRaycastHit[4];
    }

    bool dragging = false;
    //Vector2 lastMousePos;

    private Vector3 screenPoint;
    private Vector3 offset;
    void Update () {

        if (Input.GetMouseButtonUp (0)) {
            dragging = false;
        }

        if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject ()) {

            if (DoRaycast ()) {
                //start darg
                dragging = true;
                screenPoint = LiveHub.MyHub.mainCamera.WorldToScreenPoint (gameObject.transform.position);

                offset = this.transform.position - LiveHub.MyHub.mainCamera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            }
        }

        if (dragging) {
            //var dir = (Vector2) Input.mousePosition - lastMousePos;
            //this.transform.Translate (dir * 0.01f);
            Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = LiveHub.MyHub.mainCamera.ScreenToWorldPoint (curScreenPoint) + offset;
            transform.position = curPosition;
            //LiveHub.config.positionX = transform.position.x / 100f;
            //LiveHub.config.positionY = transform.position.y / 100f;
        }

        //lastMousePos = Input.mousePosition;
    }

    bool DoRaycast () {
        // Cast ray from pointer position.
        var ray = LiveHub.MyHub.mainCamera.ScreenPointToRay (Input.mousePosition);
        //Debug.Log (Raycaster);
        var hitCount = Raycaster.Raycast (ray, Results);

        if (hitCount == 0) {
            // Return early if nothing was hit.
            return false;
        }
        return true;
    }

    public void Bind (CubismModel model) {
        Model = model;

        var ds = GetComponentsInChildren<CubismDrawable> ();
        foreach (var d in ds) {
            var r = d.GetComponent<CubismRaycastable> ();
            if (r == null) {
                r = d.gameObject.AddComponent<CubismRaycastable> ();
            }

        }

        Raycaster = Model.gameObject.AddComponent<CubismRaycaster> ();
        //Debug.Log (Raycaster);
    }
}