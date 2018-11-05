/* Author: flanny7
 * Update: 2018/11/2
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CharacterStandSpriteDict
{
	private Dictionary<PlayerCharacterEnum, Sprite> dict;

	public CharacterStandSpriteDict(CharacterStandSpriteCatalog _catalog)
	{
		this.dict = new Dictionary<PlayerCharacterEnum, Sprite>
		{
			{PlayerCharacterEnum.airos, _catalog.Airos},
			{PlayerCharacterEnum.anoma, _catalog.Anoma},
			{PlayerCharacterEnum.emilia, _catalog.Emilia},
			{PlayerCharacterEnum.held, _catalog.Held},
			{PlayerCharacterEnum.kaito, _catalog.Kaito},
			{PlayerCharacterEnum.kaoru, _catalog.Kaoru},
			{PlayerCharacterEnum.laxa, _catalog.Laxa},
			{PlayerCharacterEnum.twist, _catalog.Twist},
			{PlayerCharacterEnum.vega_al, _catalog.VegaAl},
			{PlayerCharacterEnum.veronica, _catalog.Veronica}
		};

	}

	public Sprite Get(PlayerCharacterEnum _chara)
	{
		return this.dict[_chara];
	}
}