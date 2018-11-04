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
		
		[SerializeField]
		public RiaEnemyScript UFA1StayScript = null;
		
		[SerializeField]
		public RiaEnemyScript UAF1OutToInSinEnemyScript = null;
		
		[SerializeField]
		public RiaEnemyScript UAF1InToOutSinEnemyScript = null;
		
		[SerializeField]
		public RiaEnemyScript UFA1OutToInEnemy = null;
		
		[SerializeField]
		public RiaEnemyScript UFA1InToOutEnemy = null;
		
		[SerializeField]
		public RiaEnemyScript Boss = null;

	}
}