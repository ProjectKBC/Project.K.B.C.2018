using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomaSPB : MonoBehaviour {
	[SerializeField]
	private float shotLifeTime = 1.5f;

	private void Update()
	{
		shotLifeTime -= Time.deltaTime;

		if (shotLifeTime < 0) { Destroy(gameObject); }
	}
}
