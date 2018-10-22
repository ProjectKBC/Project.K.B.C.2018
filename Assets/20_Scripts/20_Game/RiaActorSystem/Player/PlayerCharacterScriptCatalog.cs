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