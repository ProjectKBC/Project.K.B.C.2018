using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomaPlayer : PlayerData {
	[SerializeField]
	private GameObject normalBulletPrefab = null;
	[SerializeField]
	private int normalShotInterval = 2;
	[SerializeField]
	private GameObject spBulletPrefab = null;
	[SerializeField]
	private float spShotChargeTime = 3.0f;

	private int normalShotTimeCount;

	private void Update()
	{
		base.Move();
		NormalShot();
		SpecialShot();
	}

	private void NormalShot()
	{
		if (Input.GetKey(NormalShotKey)) { CreateNormalBullet(); }
	}

	private void SpecialShot()
	{
		if (Input.GetKey(SpecialShotKey))
		{ spShotChargeTime -= Time.deltaTime; }

		if (spShotChargeTime < 0 && Input.GetKeyUp(SpecialShotKey)) { CreateSpecialBullet(); }

		if (Input.GetKeyUp(SpecialShotKey)) { spShotChargeTime = 3.0f; }
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

	private void CreateSpecialBullet()
	{
		Vector3 playerPos = this.transform.position;
		Vector3 minePos = new Vector3(playerPos.x, playerPos.y += 20, playerPos.z);
		Instantiate(spBulletPrefab).transform.position = minePos;
	}
}
