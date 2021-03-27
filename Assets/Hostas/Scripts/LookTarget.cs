using Live2D.Cubism.Framework.LookAt;
using UnityEngine;

public class LookTarget : MonoBehaviour, ICubismLookTarget {

    public bool useMouse { get { return LiveHub.config.followMouse; } }
    //角色模型視角跟隨模型的程度,因為1使用最大值會讓製作時偏移過大的模型出現
    //臉和眼球和頭髮在特殊角度的慘不忍睹現象
    public float lookScale { get { return LiveHub.config.lookScale; } }

    /// <summary>
    /// Get mouse coordinates while dragging.
    /// </summary>
    /// <returns>Mouse coordinates.</returns>
    public Vector3 GetPosition () {

        // return transform.position;

        // if (!Input.GetMouseButton (0)) {
        //     return Vector3.zero;
        // }

        if (!useMouse) {
            return Vector3.zero;
        }

        var targetPosition = Input.mousePosition;

        targetPosition = (Camera.main.ScreenToViewportPoint (targetPosition) * 2) - Vector3.one;

        targetPosition = targetPosition * lookScale;

        return targetPosition;
    }

    /// <summary>
    /// Gets whether the target is active.
    /// </summary>
    /// <returns><see langword="true"/> if the target is active; <see langword="false"/> otherwise.</returns>
    public bool IsActive () {
        return true;
        // return isActiveAndEnabled;
    }
}