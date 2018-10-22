using UnityEditor;
using UnityEngine;

namespace Game.Player
{
	[CreateAssetMenu(menuName = "RiaCharacterScript/CharacterCatalog/Player", fileName = "PlayerCharacterCatalog")]
	public sealed class PlayerCharacterCatalog : ScriptableObject
	{
		[SerializeField]
		public AirosScript airosScript = null;

	}
}