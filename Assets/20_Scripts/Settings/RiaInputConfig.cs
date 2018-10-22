using UnityEngine;

[CreateAssetMenu(menuName = "RiaInputConfig", fileName = "RiaInputConfig_PL?")]
public class RiaInputConfig : ScriptableObject
{
	[System.Serializable]
	public class KeyCodes
	{
		public KeyCode Return;
		public KeyCode Cancel;
		public KeyCode Up;
		public KeyCode Down;
		public KeyCode Right;
		public KeyCode Left;
		public KeyCode NormalShot;
		public KeyCode SpecialShot;
		public KeyCode Skil;
		public KeyCode LowSpeed;
		public KeyCode Pose;
	}

	[System.Serializable]
	public class ButtonString
	{
		public string Return;
		public string Cancel;
		public string Up;
		public string Down;
		public string Right;
		public string Left;
		public string NormalShot;
		public string SpecialShot;
		public string Skil;
		public string LowSpeed;
		public string Pose;
	}

	public KeyCodes keyCode = null;
	public ButtonString buttonString = null;
}