using UnityEngine;

public sealed class RiaStageManager : SingletonMonoBehaviour<RiaStageManager>
{
    private PlayerCharacterEnum pc1;
    private PlayerCharacterEnum pc2;
    private StageEnum stage;

    [SerializeField]
    private RiaStageFactory factory = null;

    protected override void OnInit()
    {
		SelectUIManager.SelectedData(out this.pc1, out this.pc2, out this.stage);

		if (this.pc1 == PlayerCharacterEnum.length_empty || this.pc1 == PlayerCharacterEnum.random ||
			this.pc2 == PlayerCharacterEnum.length_empty || this.pc2 == PlayerCharacterEnum.random ||
			this.stage == StageEnum.length_empty)
		{
            Debug.LogError("PlayerCharacter または Stage が以上です。", this.gameObject);
			Debug.Break();
        }

        this.factory.CreateStage(this.stage);
    }
}
