using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class KettyPlayer1Key : MonoBehaviour {

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
		if (Input.GetAxis("Player1_X") > 0)
		{
			Debug.Log("Player1_XのJoystickが右に倒れた");
		}
		
		if (Input.GetAxis("Player1_X") < 0)
		{
			Debug.Log("Player1_XのJoystickが左に倒れた");
		}
		
		if (Input.GetAxis("Player1_Y") > 0)
		{
			Debug.Log("Player1_XのJoystickが上に倒れた");
		}
		
		if (Input.GetAxis("Player1_Y") < 0)
		{
			Debug.Log("Player1_YのJoystickが下に倒れた");
		}

	}

	private void ButtonTest()
	{
		/*
		if (Input.GetButton("Player1_X"))
		{
			Debug.Log("Player1_Xが押された");
		}

		if (Input.GetButton("Player1_Y"))
		{
			Debug.Log("Player1_Yが押された");
		}
		*/
		
		
		if (Input.GetButtonDown("Player1_△"))
		{
			Debug.Log("Player1_△が押された");
		}
		
		if (Input.GetButtonDown("Player1_○"))
		{
			Debug.Log("Player1_○が押された");
		}
		
		if (Input.GetButtonDown("Player1_×"))
		{
			Debug.Log("Player1_×が押された");
		}
		
		if (Input.GetButtonDown("Player1_□"))
		{
			Debug.Log("Player1_□が押された");
		}
		
		if (Input.GetButtonDown("Player1_R1"))
		{
			Debug.Log("Player1_R1が押された");
		}
		
		if (Input.GetButtonDown("Player1_R2"))
		{
			Debug.Log("Player1_R2が押された");
		}
		
		if (Input.GetButtonDown("Player1_L1"))
		{
			Debug.Log("Player1_L1が押された");
		}
		
		if (Input.GetButtonDown("Player1_L2"))
		{
			Debug.Log("Player1_L2が押された");
		}
		
		if (Input.GetButtonDown("Player1_Select"))
		{
			Debug.Log("Player1_Selectが押された");
		}
		
		if (Input.GetButtonDown("Player1_Start"))
		{
			Debug.Log("Player1_Startが押された");
		}
		
		if (Input.GetButtonDown("Player1_PS"))
		{
			Debug.Log("Player1_PSが押された");
		}
	}
}
