using UnityEngine;

public class SelectUIManager : SingletonMonoBehaviour<SelectUIManager>
{
    public enum State
    {
        CharacterSelect,
        StageSelect,
    }

    public GameObject CharaCanvas { get { return this.charaCanvas; } }
    public GameObject StageCanvas { get { return this.stageCanvas; } }
    public CharacterSelectState ChatAct { get { return this.charaAct; } }
    public StageSelectState StageAct { get { return this.stageAct; } }

    [SerializeField]
    private GameObject charaCanvas = null;

    [SerializeField]
    private GameObject stageCanvas = null;

	[Space(16)]

	[SerializeField]
	private CommonData commonData = null;

    // ステートマシン系
    private StateManager<State> stateManager = new StateManager<State>();
    private State currentState;
    private CharacterSelectState charaAct = new CharacterSelectState();
    private StageSelectState stageAct = new StageSelectState();

    private PlayerCharacterEnum pc1;
    private PlayerCharacterEnum pc2;
    private StageEnum stage;

    public void TransitionToStageSelect(PlayerCharacterEnum _pc1, PlayerCharacterEnum _pc2)
    {
		if (_pc1 == PlayerCharacterEnum.length_empty || _pc1 == PlayerCharacterEnum.random)
		{
			Debug.Log("PlayerCharacter1 の値が不正です");
			_pc1 = PlayerCharacterEnum.airos;
		}

		if (_pc2 == PlayerCharacterEnum.length_empty || _pc2 == PlayerCharacterEnum.random)
		{
			Debug.Log("PlayerCharacter2 の値が不正です");
			_pc2 = PlayerCharacterEnum.airos;
		}

		this.commonData.playerCharacter1 = _pc1;
		this.commonData.playerCharacter2 = _pc2;

		this.currentState = State.StageSelect;
		this.stateManager.SetState(this.currentState);
	}

	public void TransitionToTitleScene()
	{
		this.commonData.playerCharacter1 = PlayerCharacterEnum.length_empty;
		this.commonData.playerCharacter2 = PlayerCharacterEnum.length_empty;
		this.commonData.stage = StageEnum.length_empty;

		FadeManager.Instance.LoadScene(2.0f, SceneEnum.Title.ToDescription(), () => {
			Destroy(this);
		});
	}

	public void TransitionToCharactetSelect()
	{
		this.commonData.playerCharacter1 = PlayerCharacterEnum.length_empty;
		this.commonData.playerCharacter2 = PlayerCharacterEnum.length_empty;

		this.currentState = State.CharacterSelect;
		this.stateManager.SetState(this.currentState);
	}

	public void TransitionToGameScene(StageEnum _stage)
    {
		this.commonData.stage = _stage;
		FadeManager.Instance.LoadScene(2.0f, SceneEnum.Game.ToDescription(), () => {
			Destroy(this);
		});
    }

    protected override void OnInit()
    {
        this.stateManager.Add(State.CharacterSelect, this.charaAct);
        this.stateManager.Add(State.StageSelect, this.stageAct);
		
		this.currentState = State.CharacterSelect;
		this.stateManager.SetState(this.currentState);
	}

    private void Update()
    {
        this.stateManager.Update();
    }
}
