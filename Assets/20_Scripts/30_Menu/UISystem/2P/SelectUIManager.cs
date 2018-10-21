using UnityEngine;

public class SelectUIManager : SingletonMonoBehaviour<SelectUIManager>
{
    public static void SelectedData (
        out PlayerCharacterEnum _pc1,
        out PlayerCharacterEnum _pc2,
        out StageEnum _stage)
    {
        var self = SelectUIManager.Instance;

        _pc1 = self.pc1;
        _pc2 = self.pc2;
        _stage = self.stage;
    }

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
        this.pc1 = _pc1;
        this.pc2 = _pc2;

		this.currentState = State.StageSelect;
		this.stateManager.SetState(this.currentState);
	}

    public void TransitionToCharactetSelect()
	{
		this.currentState = State.CharacterSelect;
		this.stateManager.SetState(this.currentState);
	}

    public void TransitionToGameScene(StageEnum _stage)
    {
        this.stage = _stage;
        // todo: シーン遷移と値の受け渡し
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
