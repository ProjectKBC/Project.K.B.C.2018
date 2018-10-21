public class CharacterSelectState : StateAction
{
    public override void Start()
    {
        SelectUIManager.Instance.CharaCanvas.SetActive(true);
        SelectUIManager.Instance.StageCanvas.SetActive(false);
        CharaSelectManager.Instance.Init();
    }

    public override void Update()
    {
        CharaSelectManager.Instance.Run();
    }

    public override void End()
    {
    }
}