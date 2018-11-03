/* Author: flanny7
 * Update: 2018/11/3
*/ 

using UnityEngine;

public class BGMTrigger : MonoBehaviour
{
	[SerializeField]
	private BackGroundMusicEnum bgm;

	private bool isStartBGM = false;

	private void Awake()
	{
		this.isStartBGM = false;
	}

	private void Update()
	{
		if (isStartBGM) { return; }
		
		AudioManager.Instance.PlayBgm(this.bgm);
		this.isStartBGM = true;
	}
}