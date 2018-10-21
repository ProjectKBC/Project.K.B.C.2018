using UnityEngine;

namespace Game.Stage
{
	public sealed class RiaStageManager : MonoBehaviour
	{
		private RiaStageFactory factory = null;
		private StageEnum stage = StageEnum.length_empty;
		private float mainElapsedTime = 0;
		private float bossElapsedTime = 0;

		public void Init()
		{
			this.factory = new RiaStageFactory();
			this.stage = GameManager.Instance.CommonData.stage;
			this.mainElapsedTime = 0;
			this.bossElapsedTime = 0;

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
			// 
		}

		public void BossLoop()
		{
			this.bossElapsedTime += Time.deltaTime;
			//
		}
	}
}