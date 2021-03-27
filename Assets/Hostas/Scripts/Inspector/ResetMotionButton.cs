public class ResetMotionButton : BaseButton {
    // Start is called before the first frame update
    void Start () {

        base.Init ();
    }

    public override void OnClick () {
        if (LiveHub.MyHub.lastModel.MotionDatas.Count == 0)
            return;
        LiveHub.MyHub.lastModel.motionController.StopAllAnimation ();
    }
}