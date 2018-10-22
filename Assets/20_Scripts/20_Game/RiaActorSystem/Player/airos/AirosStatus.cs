/* Author close96
 * Transplant flanny7
 * Update 2018/10/21
*/

using UnityEngine;

namespace Game.Player
{
	public sealed class AirosStatus : PlayerStatus
	{
		// 通常ショット
		public class NormalShotParam
		{
			public float shotTime = 0;
			public int shotCount = 0;
		}

		// 特殊ショット
		public class SpecialShotParam
		{
			public float shotTime = 0;

			public RiaActor[] targetEnemys = new RiaActor[0];
			public float searchTime = 0;
			public int searchEnemyCount = 0;
		}

		// スキル
		public class SkilParam
		{
			public bool isUsing = false;

			public float timeCount = 0;

			public float rivalPlayerMoveSpeed;

			public float speedChangeRate = 0;
			public float alphaValue = 0;
		}

		public NormalShotParam nsParam;
		public SpecialShotParam ssParam;
		public SkilParam skilParam;

		public AirosStatus(GameObject _go, PlayerNumber _playerNumber, PlayerActorManager _parentManager) : base(_go, _playerNumber, _parentManager)
		{
			nsParam = new NormalShotParam();
			ssParam = new SpecialShotParam();
			skilParam = new SkilParam();
		}

	}
}