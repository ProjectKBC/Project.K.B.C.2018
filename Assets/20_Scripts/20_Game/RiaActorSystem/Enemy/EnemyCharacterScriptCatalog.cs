/* Author: flanny7
 * Update: 2018/10/28
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Enemy
{
	[CreateAssetMenu(menuName = "RiaCharacterScript/CharacterCatalog/Enemy", fileName = "EnemyCharacterCatalog")]
	public sealed class EnemyCharacterScriptCatalog : ScriptableObject
	{
		[SerializeField]
		public RiaEnemyScript UFA1StraightScript = null;

	}
}