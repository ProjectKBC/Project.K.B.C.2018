/* Author: flanny7
 * Updata: 2018/10/29 
*/

using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeSceneManager : MonoBehaviour
{
    [SerializeField]
    private string commonSceneName = "CommonScene";
    [SerializeField]
    private SceneEnum firstLoadScene = SceneEnum.Title;
	[SerializeField]
	private int screenWidth = 1920;
	[SerializeField]
	private int screenHeight = 1080;

	private bool isFirstUpdate;
	
	private void Awake()
	{
		if (Application.platform == RuntimePlatform.WindowsPlayer ||
			Application.platform == RuntimePlatform.OSXPlayer ||
			Application.platform == RuntimePlatform.LinuxPlayer)
		{
			//Screen.fullScreen = true;
			//Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
			//Screen.SetResolution(screenWidth, screenHeight, false);
		}

		SceneManager.LoadScene(this.commonSceneName, LoadSceneMode.Additive);
        isFirstUpdate = true;
    }

    private void Update()
    {
        if (isFirstUpdate) { this.isFirstUpdate = false; return; }

        SceneManager.LoadScene(this.firstLoadScene.ToDescription());
    }
}