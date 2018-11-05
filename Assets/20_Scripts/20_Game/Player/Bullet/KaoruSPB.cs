using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaoruSPB : MonoBehaviour {
	[SerializeField]
	private float shotSpeed = 0.5f;
	private float screenTopPoint = 65.0f;
	private float explosionScaleX = 70;
	private float explosionScaleY = 35;
	// 完成版はアニメーションの再生時間による調整に変更するかも？
	private float shotLifeTime = 1;
	private Animator explosion;

	private void Start()
	{
		explosion = this.GetComponent<Animator>();
	}

	private void Update()
	{
		this.transform.position += Vector3.up * shotSpeed * Time.deltaTime * 60;

		// 画面外処理
		if (screenTopPoint < this.transform.position.y) { explosion.enabled = true; }

		// 爆発
		if (explosion.enabled)
		{
			this.transform.localScale = new Vector3(explosionScaleX, explosionScaleY, this.transform.localScale.z);
			this.shotSpeed = 0;
			this.shotLifeTime -= Time.deltaTime;

			if (this.shotLifeTime <= 0) { Destroy(gameObject); }
		}

	}

}
