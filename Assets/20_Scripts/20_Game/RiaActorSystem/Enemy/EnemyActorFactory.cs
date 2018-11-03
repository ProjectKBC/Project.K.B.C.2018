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
				
				case EnemyCharacterEnum.UAF1StayEnemy:
					script = this.catalog.UFA1StayScript;
					character = new UFA1StayEnemy(_actor.gameObject, script, _playerNumber);
					break;
				
				case EnemyCharacterEnum.UAF1SinEnemy:
					script = this.catalog.UFA1SinScript;
					character = new UFA1SinEnemy(_actor.gameObject, script, _playerNumber);
					break;
				
				case EnemyCharacterEnum.UAF1ToLeftSideEnemy:
					script = this.catalog.UFA1ToLeftSideEnemy;
					character = new UFA1ToLeftSideEnemy(_actor.gameObject, script, _playerNumber);
					break;
				
				case EnemyCharacterEnum.UAF1ToRightSideEnemy:
					script = this.catalog.UFA1ToRightSideEnemy;
					character = new UFA1ToRightSideEnemy(_actor.gameObject, script, _playerNumber);
					break;
				
				case EnemyCharacterEnum.UAF1OutToInEnemy:
					Debug.Log("いいい");

					script = this.catalog.UFA1OutToInEnemy;
					character = new UFA1OutToIn(_actor.gameObject, script, _playerNumber);
					break;
				
				case EnemyCharacterEnum.UAF1InToOutEnemy:
					script = this.catalog.UFA1InToOutEnemy;
					character = new UFA1InToOut(_actor.gameObject, script, _playerNumber);
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