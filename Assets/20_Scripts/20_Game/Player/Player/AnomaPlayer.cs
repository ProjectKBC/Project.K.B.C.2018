using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomaPlayer : PlayerData {
	[SerializeField]
	private GameObject normalBulletPrefab = null;
	[SerializeField]
	private int normalShotInterval = 2;

	private int normalShotTimeCount;

	private void Update()
	{
		base.Move();
		NormalShot();
	}

	private void NormalShot()
	{
		if (Input.GetKey(NormalShotKey))
		{
			CreateBullet();
		}
	}

	private void CreateBullet()
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
