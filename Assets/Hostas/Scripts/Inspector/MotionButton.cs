public class MotionButton : BaseButton {
    public ModelAvatar.MotionData motionData;
    void Start () {
        base.Init ();
        this.propName = motionData.moName;
        this.showPropName.text = this.propName;
    }

    public override void OnClick () {
        LiveHub.MyHub.lastModel.motionPreview.PlayAnimation (motionData.anim);
    }
}