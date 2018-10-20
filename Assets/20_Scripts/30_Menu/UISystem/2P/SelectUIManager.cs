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

    // ステートマシン系
    private StateManager<State> stateManager = new StateManager<State>();
    private State currentState = State.CharacterSelect;
    private CharacterSelectState charaAct = new CharacterSelectState();
    private StageSelectState stageAct = new StageSelectState();

    private PlayerCharacterEnum pc1 = PlayerCharacterEnum.length_empty;
    private PlayerCharacterEnum pc2 = PlayerCharacterEnum.length_empty;
    private StageEnum stage = StageEnum.length_empty;

    public void TransitionToStageSelect(PlayerCharacterEnum _pc1, PlayerCharacterEnum _pc2)
    {
        this.pc1 = _pc1;
        this.pc2 = _pc2;

        this.SetState(State.StageSelect);
    }

    public void TransitionToCharactetSelect()
    {
        this.SetState(State.CharacterSelect);
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

        this.SetState(State.CharacterSelect);
    }

    private void Update()
    {
        this.stateManager.Update();
    }

    private void SetState(State _state)
    {
        this.currentState = _state;
        this.stateManager.SetState(_state);
    }

}
