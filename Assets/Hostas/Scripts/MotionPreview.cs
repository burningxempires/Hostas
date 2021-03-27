using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Motion;
using UnityEngine;

public class MotionPreview : MonoBehaviour {
    /// <summary>
    ///
    /// </summary>
    // public AnimationClip Animation;

    /// <summary>
    /// MotionController to be operated.
    /// </summary>
    CubismMotionController _motionController;

    /// <summary>
    /// Get motion controller.
    /// </summary>
    private void Start () {
        var model = this.FindCubismModel ();

        _motionController = model.GetComponent<CubismMotionController> ();

        _motionController.AnimationEndHandler += AnimationEnd;

        // if (Animation == null) {
        //     return;
        // }

        // PlayIdleAnimation ();
    }

    private void AnimationEnd (float index = 0.0f) {
        // _motionController.PlayAnimation (Animation, isLoop : false, priority : CubismMotionPriority.PriorityIdle);
        Debug.Log ("AnimationEnd " + index);
    }

    /// <summary>
    /// Play specified animation.
    /// </summary>
    /// <param name="animation">Animation clip to play.</param>
    public void PlayAnimation (AnimationClip animation) {
        // _motionController.PlayAnimation (animation);
        // _motionController.PlayAnimation (animation, isLoop : true, priority : CubismMotionPriority.PriorityForce);
        _motionController.PlayAnimation (animation, layerIndex : 0, isLoop : false, priority : CubismMotionPriority.PriorityForce);
    }
}