using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class KettyPlayer2Key : MonoBehaviour {

	void Update () {
		this.JoystickTest();
		this.ButtonTest();
		
		/*
		if (Input.anyKeyDown) {
			foreach (KeyCode code in Enum.GetValues(typeof(KeyCode))) {
				if (Input.GetKeyDown (code)) {
					//処理を書く
					Debug.Log (code);
					break;
				}
			}
		}
		*/
		
	}
	
	private void JoystickTest()
	{
		
		if (Input.GetAxis("Player2_X") > 0)
		{
			Debug.Log("Player2_XのJoystickが右に倒れた");
		}
		
		if (Input.GetAxis("Player2_X") < 0)
		{
			Debug.Log("Player2_XのJoystickが左に倒れた");
		}
		
		if (Input.GetAxis("Player2_Y") > 0)
		{
			Debug.Log("Player2_XのJoystickが上に倒れた");
		}
		
		if (Input.GetAxis("Player2_Y") < 0)
		{
			Debug.Log("Player2_YのJoystickが下に倒れた");
		}
		
		
	}
	
	private void ButtonTest()
	{
		if (Input.GetButtonDown("Player2_X"))
		{
			Debug.Log("Player2_Xが押された");
		}
		
		if (Input.GetButtonDown("Player2_Y"))
		{
			Debug.Log("Player2_Yが押された");
		}
		
		if (Input.GetButtonDown("Player2_△"))
		{
			Debug.Log("Player2_△が押された");
		}
		
		if (Input.GetButtonDown("Player2_○"))
		{
			Debug.Log("Player2_○が押された");
		}
		
		if (Input.GetButtonDown("Player2_×"))
		{
			Debug.Log("Player2_×が押された");
		}
		
		if (Input.GetButtonDown("Player2_□"))
		{
			Debug.Log("Player2_□が押された");
		}
		
		if (Input.GetButtonDown("Player2_R1"))
		{
			Debug.Log("Player2_R1が押された");
		}
		
		if (Input.GetButtonDown("Player2_R2"))
		{
			Debug.Log("Player2_R2が押された");
		}
		
		if (Input.GetButtonDown("Player2_L1"))
		{
			Debug.Log("Player2_L1が押された");
		}
		
		if (Input.GetButtonDown("Player2_L2"))
		{
			Debug.Log("Player2_L2が押された");
		}
	}
}
