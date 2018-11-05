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
		[SerializeField]
		public AnomaPlayerScript anomaScript = null;
		[SerializeField]
		public HeldPlayerScript heldScript = null;
		// 緊急リリース用の偽物
		[SerializeField]
		public FakeKaitoPlayerScript kaitoScript = null;
		// [SerializeField]
		// public KaitoPlayerScript kaitoScript = null;
		[SerializeField]
		public KaoruPlayerScript kaoruScript = null;
		[SerializeField]
		public TwistPlayerScript twistScript = null;
		[SerializeField]
		public VeronicaPlayerScript veronicaScript = null;
	}
}
