public class ExpButton : BaseButton {
    public ModelAvatar.ExpressionData expressionData;
    void Start () {
        base.Init ();
        this.propName = expressionData.expName;
        this.showPropName.text = this.propName;
    }

    public override void OnClick () {
        LiveHub.MyHub.lastModel.expressionPreview.ChangeExpression (expressionData.expIndex);
    }
}