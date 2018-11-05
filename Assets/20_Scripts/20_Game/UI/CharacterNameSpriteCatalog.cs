/* Author: flanny7
 * Update: 2018/11/2
*/

using UnityEngine;

[CreateAssetMenu(menuName = "SpriteCatalog/CharacterName", fileName = "CharacterNameSpriteCatalog")]
public class CharacterNameSpriteCatalog : ScriptableObject
{
	[System.Serializable]
	public struct Charas
	{
		public Sprite airos;
		public Sprite anoma;
		public Sprite emilia;
		public Sprite held;
		public Sprite kaito;
		public Sprite kaoru;
		public Sprite laxa;
		public Sprite twist;
		public Sprite vegaAl;
		public Sprite veronica;
	}
	
	[SerializeField]
	private Charas kana;
	[SerializeField]
	private Charas alp;
	
	public Charas Kana { get { return this.kana; } }
	public Charas Alp { get { return this.alp; } }
}
