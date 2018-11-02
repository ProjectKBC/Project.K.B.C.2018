/* Author: flanny7
 * Update: 2018/11/2
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CharacterNameSpriteDict
{
	private Dictionary<PlayerCharacterEnum, Sprite> kanaDict;
	private Dictionary<PlayerCharacterEnum, Sprite> alpDict;

	public CharacterNameSpriteDict(CharacterNameSpriteCatalog _catalog)
	{
		this.kanaDict = new Dictionary<PlayerCharacterEnum, Sprite>
		{
			{PlayerCharacterEnum.airos, _catalog.Kana.airos},
			{PlayerCharacterEnum.anoma, _catalog.Kana.anoma},
			{PlayerCharacterEnum.emilia, _catalog.Kana.emilia},
			{PlayerCharacterEnum.held, _catalog.Kana.held},
			{PlayerCharacterEnum.kaito, _catalog.Kana.kaito},
			{PlayerCharacterEnum.kaoru, _catalog.Kana.kaoru},
			{PlayerCharacterEnum.laxa, _catalog.Kana.laxa},
			{PlayerCharacterEnum.twist, _catalog.Kana.twist},
			{PlayerCharacterEnum.vega_al, _catalog.Kana.vegaAl},
			{PlayerCharacterEnum.veronica, _catalog.Kana.veronica}
		};
		
		this.alpDict = new Dictionary<PlayerCharacterEnum, Sprite>
		{
			{PlayerCharacterEnum.airos, _catalog.Alp.airos},
			{PlayerCharacterEnum.anoma, _catalog.Alp.anoma},
			{PlayerCharacterEnum.emilia, _catalog.Alp.emilia},
			{PlayerCharacterEnum.held, _catalog.Alp.held},
			{PlayerCharacterEnum.kaito, _catalog.Alp.kaito},
			{PlayerCharacterEnum.kaoru, _catalog.Alp.kaoru},
			{PlayerCharacterEnum.laxa, _catalog.Alp.laxa},
			{PlayerCharacterEnum.twist, _catalog.Alp.twist},
			{PlayerCharacterEnum.vega_al, _catalog.Alp.vegaAl},
			{PlayerCharacterEnum.veronica, _catalog.Alp.veronica}
		};
	}

	public Sprite GetKana(PlayerCharacterEnum _chara)
	{
		return this.kanaDict[_chara];
	}
	
	public Sprite GetAlp(PlayerCharacterEnum _chara)
	{
		return this.alpDict[_chara];
	}
}