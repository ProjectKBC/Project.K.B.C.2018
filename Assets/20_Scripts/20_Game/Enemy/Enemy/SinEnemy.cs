using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinEnemy : Enemy {

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		CreateBullet(this.NomalBullet);
	}

	protected override void Update () {
		base.Update();
		
		float nowPass = Mathf.Floor (this.ElapsedTime * 10) / 10;
		if (this.Trans.position.y > ordinaryForwardBorder)
		{
			ForwardEnemy(ordinaryForwardBorder);
		}
		else
		{
			SinMove();
		}
		if (nowPass >= this.Pass)
		{
			if (IsBurstAttack)
			{
				BurstAttack ();
			}
			else
			{
				NormalAtack ();
			}
		}
	}

	private void SinMove()
	{
		Vector3 pos = this.Trans.position;
		pos.x += this.MoveSpeedRate * Time.deltaTime;
		pos.y = Mathf.Sin(pos.x / this.MoveSpeedRate) * this.MoveSpeedRate;
		this.Trans.position = pos;
	}
}
