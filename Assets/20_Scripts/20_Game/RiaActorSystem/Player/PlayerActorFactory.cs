/* Author: flanny7
 * Update: 2018/10/22
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	[System.Serializable]
	public sealed class PlayerActorFactory
	{
		[SerializeField]
		private PlayerCharacterScriptCatalog catalog = null;

		public void Create(PlayerNumber _playerNumber, RiaActor _actor,
				Vector3 _position, Quaternion? _rotation = null, Vector3? _scale = null)
		{
			var playerCharacter = (_playerNumber == PlayerNumber.player1) ?
				GameManager.Instance.CommonData.playerCharacter1 :
				GameManager.Instance.CommonData.playerCharacter2;

			RiaCharacter character = null;
			RiaCharacterScript script = null;

			switch (playerCharacter)
			{
				case PlayerCharacterEnum.airos:
					script = this.catalog.airosScript;
					character = new AirosPlayer(_actor.gameObject, script, _playerNumber);
					break;

				case PlayerCharacterEnum.anoma:
					script = this.catalog.anomaScript;
					character = new AnomaPlayer(_actor.gameObject, script, _playerNumber);
					break;

				case PlayerCharacterEnum.emilia:
					break;

				case PlayerCharacterEnum.held:
					break;

				case PlayerCharacterEnum.kaito:
					break;

				case PlayerCharacterEnum.kaoru:
					script = this.catalog.kaoruScript;
					character = new KaoruPlayer(_actor.gameObject, script, _playerNumber);
					break;

				case PlayerCharacterEnum.laxa:
					break;

				case PlayerCharacterEnum.twist:
					break;

				case PlayerCharacterEnum.vega_al:
					break;

				case PlayerCharacterEnum.veronica:
					break;

				default:
					Debug.LogError("存在しないキャラクターが選択されています");
					break;
			}

			if (character == null || !script)
			{
				Debug.LogError("Player[ " + playerCharacter + " ]が正常に生成されませんでした");
				return;
			}

			_actor.WakeUp(character, script, _position, _rotation, _scale);
		}
	}
}