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
		this.CreateBullet(this.NormalBullet);
	}

	protected override void Update () {
		base.Update();
		if (this.Trans.position.y > this.ordinaryYForwardBorder)
		{
			this.YForwardEnemy(this.ordinaryYForwardBorder);
		}
		else
		{
			this.SinMove();
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
