using UnityEngine;

namespace Game.Player
{
	public abstract class PlayerStatus : RiaCharacterStatus
	{
		public PlayerNumber PlayerNumber { get; private set; }
		public PlayerActorManager ParentManager { get; private set; }
		public SpriteRenderer SpriteRenderer { get; set; }
		public PlayerScript rivalPlayerScript;
		public PlayerStatus rivalPlayerStatus;

		// ゲームに関わるパラメータ―
		public float HitPoint { get;  set; }
		public float MoveSpeedRate { get; private set; }

		private bool isSecondInit;

		public PlayerStatus(
			GameObject _go,
			PlayerNumber _playerNumber, 
			PlayerActorManager _parentManager)
			: base(_go)
		{
			this.SpriteRenderer = this.go_.GetComponent<SpriteRenderer>();
			if (!this.SpriteRenderer) { Debug.LogWarning("SpriteRendererがありません", this.go_); }

			this.PlayerNumber = _playerNumber;
			this.ParentManager = _parentManager;

			// 一応タグ付け
			this.go_.tag = (_playerNumber == PlayerNumber.player1) ?
				TagEnum.Player1.ToDescription() :
				TagEnum.Player2.ToDescription();

			this.isSecondInit = false;
		}

		public void SecondInit()
		{
			if (isSecondInit) { return; }

			// 相手プレイヤーの取得
			var rival = this.ParentManager.RivalPlayerActorManager.GetActiveActors()[0];
			this.rivalPlayerScript = rival.Script as PlayerScript;
			this.rivalPlayerStatus = rival.Status as PlayerStatus;

			this.isSecondInit = true;
		}

		public void Damage(float _damage)
		{
			this.HitPoint -= _damage;
		}

		public void SetMoveSpeedRate(float _value)
		{
			this.MoveSpeedRate = _value;
		}
	}

}