using System.Collections;
using UnityEngine;

namespace Game.Player
{
	[System.Serializable]
	public sealed class PlayerActorFactory
	{
		[SerializeField]
		private PlayerCharacterCatalog catalog = null;

		public void Create(PlayerNumber _playerNumber, RiaActor _actor, PlayerActorManager _manager,
			Vector3 _position, Quaternion? _quaternion = null, Vector3? _scale = null)
		{
			var playerCharacter = (_playerNumber == PlayerNumber.player1) ?
				GameManager.Instance.CommonData.playerCharacter1 :
				GameManager.Instance.CommonData.playerCharacter2;

			PlayerStatus status = null;
			PlayerScript script = null;

			switch (playerCharacter)
			{
				case PlayerCharacterEnum.airos :
					status = new AirosStatus(
						_actor.gameObject,
						_playerNumber,
						_manager);
					script = this.catalog.airosScript;
					break;

				case PlayerCharacterEnum.anoma :
					break;

				case PlayerCharacterEnum.emilia :
					break;

				case PlayerCharacterEnum.held :
					break;

				case PlayerCharacterEnum.kaito :
					break;

				case PlayerCharacterEnum.kaoru :
					break;

				case PlayerCharacterEnum.laxa :
					break;

				case PlayerCharacterEnum.twist :
					break;

				case PlayerCharacterEnum.vega_al :
					break;

				case PlayerCharacterEnum.veronica :
					break;

				default :
					Debug.LogError("存在しないキャラクターが選択されています");
					break;
			}

			if (status == null || script == null)
			{
				Debug.LogError("Player[ " + playerCharacter + " ]が正常に生成されませんでした");
				return;
			}
			
			_actor.WakeUp(status, script, _position);
		}
	}
}