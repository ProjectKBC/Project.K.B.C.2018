using UnityEngine;

namespace Game.Stage
{
	public sealed class RiaStageManager : MonoBehaviour
	{
		[SerializeField]
		private PlayerNumber playerNumber = PlayerNumber.player1;
		[SerializeField]
		private RiaStageRollController rollController = null;
		[SerializeField]
		private RiaStageFactory factory = null;

		private StageEnum stage = StageEnum.length_empty;
		private float mainElapsedTime = 0;
		private float bossElapsedTime = 0;

		public void Init()
		{
			this.stage = GameManager.Instance.CommonData.stage;

			if (this.stage == StageEnum.length_empty)
			{
				Debug.LogError("CommonDate.stageが異常です。");
				Debug.Break();
			}

			this.mainElapsedTime = 0;
			this.bossElapsedTime = 0;

			this.rollController.ChangeMaterial(this.stage, false);

			this.factory.CreateStage(this.stage);
		}

		public void ReadyLoop()
		{
			// todo: 待機状態
			// アニメーションはせず、静止描画とか？
		}

		public void MainLoop()
		{
			this.mainElapsedTime += Time.deltaTime;

			this.rollController.Run();
			// 
		}

		public void BossLoop()
		{
			this.bossElapsedTime += Time.deltaTime;
			//
		}
	}
}