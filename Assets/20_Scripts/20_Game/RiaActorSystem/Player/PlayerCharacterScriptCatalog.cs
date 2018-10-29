/* Author: flanny7
 * Update: 2018/10/22
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Player
{
	[CreateAssetMenu(menuName = "RiaCharacterScript/CharacterCatalog/Player", fileName = "PlayerCharacterCatalog")]
	public sealed class PlayerCharacterScriptCatalog : ScriptableObject
	{
		[SerializeField]
		public AirosPlayerScript airosScript = null;

	}
}