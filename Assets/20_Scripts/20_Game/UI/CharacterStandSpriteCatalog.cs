/* Author: flanny7
 * Update: 2018/11/2
*/

using UnityEngine;

[CreateAssetMenu(menuName = "SpriteCatalog/CharacterStand", fileName = "CharacterStandSpriteCatalog")]
public sealed class CharacterStandSpriteCatalog : ScriptableObject
{
	[SerializeField] private Sprite airos;
	[SerializeField] private Sprite anoma;
	[SerializeField] private Sprite emilia;
	[SerializeField] private Sprite held;
	[SerializeField] private Sprite kaito;
	[SerializeField] private Sprite kaoru;
	[SerializeField] private Sprite laxa;
	[SerializeField] private Sprite twist;
	[SerializeField] private Sprite vegaAl;
	[SerializeField] private Sprite veronica;
	
	public Sprite Airos { get { return this.airos; } }
	public Sprite Anoma { get { return this.anoma; } }
	public Sprite Emilia { get { return this.emilia; } }
	public Sprite Held { get { return this.held; } }
	public Sprite Kaito { get { return this.kaito; } }
	public Sprite Kaoru { get { return this.kaoru; } }
	public Sprite Laxa { get { return this.laxa; } }
	public Sprite Twist { get { return this.twist; } }
	public Sprite VegaAl { get { return this.vegaAl; } }
	public Sprite Veronica { get { return this.veronica; } }
}