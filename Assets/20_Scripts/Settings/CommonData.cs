using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "CommonData", fileName = "CommonData")]
public sealed class CommonData : ScriptableObject
{
	public PlayerCharacterEnum playerCharacter1;
	public PlayerCharacterEnum playerCharacter2;
	public StageEnum stage;
}