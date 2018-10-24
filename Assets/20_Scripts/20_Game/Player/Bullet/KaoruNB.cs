using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaoruNB : MonoBehaviour {
	[SerializeField]
	private float shotSpeed = 100.0f;
	private Vector3 shotStartPos;
	private float screenTopPoint = 65.0f;

	private void Start()
	{
		shotStartPos = this.transform.position;
	}

	private void Update()
	{
		this.transform.position += Vector3.up * shotSpeed * Time.deltaTime;

		if (screenTopPoint < this.transform.position.y) { Destroy(gameObject); }
	}
}
