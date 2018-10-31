/* Author : flanny7
 * Update : 2018/10/27
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Enemy
{
	[System.Serializable]
	public class EnemyActorFactory
	{
		[SerializeField]
		private EnemyCharacterScriptCatalog catalog = null;

		public void Create(EnemyCharacterEnum _enemyCharacter, PlayerNumber _playerNumber, RiaActor _actor,
				Vector3 _position, Quaternion? _rotation = null, Vector3? _scale = null)
		{
			RiaEnemy character = null;
			RiaEnemyScript script = null;

			switch (_enemyCharacter)
			{
				case EnemyCharacterEnum.UAF1StraightEnemy:
					script = this.catalog.UFA1StraightScript;
					character = new UFA1StraightEnemy(_actor.gameObject, script, _playerNumber);
					break;

				default:
					Debug.LogError("存在しないキャラクターが選択されています");
					break;
			}

			if (character == null || !script)
			{
				Debug.LogError(string.Format("Enemy[ {0} ]が正常に生成されませんでした", _enemyCharacter));
				return;
			}

			_actor.WakeUp(character, script, _position, _rotation, _scale);
		}
	}
}