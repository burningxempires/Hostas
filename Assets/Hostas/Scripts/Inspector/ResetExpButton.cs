public class ResetExpButton : BaseButton {
    // Start is called before the first frame update
    void Start () {

        base.Init ();
    }

    public override void OnClick () {
        if (LiveHub.MyHub.lastModel.ExpressionDatas.Count == 0)
            return;
        LiveHub.MyHub.lastModel.expressionPreview.ChangeExpression (-1);
    }
}