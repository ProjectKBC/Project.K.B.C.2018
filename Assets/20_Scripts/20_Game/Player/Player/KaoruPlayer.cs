using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaoruPlayer : PlayerData {
	[SerializeField]
	private GameObject normalBulletPrefab = null;
	[SerializeField]
	private float normalShotInterval = 0.5f;

	private int normalShotTimeCount;

	private void Update()
	{
		base.Move();
		NormalShot();
	}

	private void NormalShot()
	{
		if (Input.GetKey(NormalShotKey)) { CreateNormalBullet(); }
	}

	private void CreateNormalBullet()
	{
		normalShotTimeCount++;
		if (normalShotInterval < normalShotTimeCount)
		{
			normalShotTimeCount = 0;

			GameObject normalBullets = Instantiate(normalBulletPrefab);
			normalBullets.transform.position = this.transform.position;
		}
	}
}
