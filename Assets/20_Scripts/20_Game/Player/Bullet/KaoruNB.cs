using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaoruNB : MonoBehaviour {
	[SerializeField]
	private float shotSpeed = 4.0f;
	private float screenTopPoint = 65.0f;

	private void Update()
	{
		this.transform.position += Vector3.up * shotSpeed * Time.deltaTime * 60;

		// 画面外処理
		if (screenTopPoint < this.transform.position.y) { Destroy(gameObject); }
	}
}
