public class HideAppBg : BaseButton {
    // Start is called before the first frame update
    void Start () {
        base.Init ();
    }

    public override void OnClick () {
        var color = LiveHub.MyHub.mainCamera.backgroundColor;
        color.a = 0;
        LiveHub.MyHub.mainCamera.backgroundColor = color;
    }
}