/* Author : flanny7
 * Update : 2018/10/29
*/

using UnityEngine;

namespace RiaActorSystem
{
	/// <summary>
	/// Actorが演じるCharacterクラス
	/// </summary>
	[System.Serializable]
	public abstract class RiaCharacter
	{
		// プレイヤー番号
		public PlayerNumber PlayerNumber { get; protected set; }
		public PlayerNumber RivalPlayerNumber { get; protected set; }
		
		// キャッシュ
		public RiaCharacterScript Script { get; protected set; }
		public GameObject Go { get; protected set; }
		public Transform Trans { get; protected set; }
		public RiaActor Actor { get; protected set; }
		protected float playElapsedTime;

		public RiaCharacter(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber)
		{
			this.Go = _go;
			this.Trans = this.Go.transform;
			this.Actor = this.Go.GetComponent<RiaActor>();
			this.Script = _script;

			this.PlayerNumber = _playerNumber;
			this.RivalPlayerNumber = (this.PlayerNumber == PlayerNumber.player1) ? PlayerNumber.player2 : PlayerNumber.player1;

			this.playElapsedTime = 0;
		}

		public void Init()
		{
			this.playElapsedTime = 0;
			this.OnInit();
		}

		public void Wait()
		{
			this.OnWait();
		}

		public void Play()
		{
			this.playElapsedTime += Time.deltaTime;
			this.OnPlay();
		}

		public void End()
		{
			this.OnEnd();
		}

		protected abstract void OnInit();
		protected abstract void OnWait();
		protected abstract void OnPlay();
		protected abstract void OnEnd();

		public static bool operator! (RiaCharacter a)
		{
			if (a == null) { return false; }
			else { return true; }
		}
	}
}