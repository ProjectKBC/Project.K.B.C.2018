public class StageSelectState : StateAction
{
    public override void Start()
    {
        SelectUIManager.Instance.CharaCanvas.SetActive(false);
        SelectUIManager.Instance.StageCanvas.SetActive(true);
    }

    public override void Update()
    {
        StageSelectManager.Instance.Run();
    }

    public override void End()
    {
    }
}