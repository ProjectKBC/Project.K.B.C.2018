using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToLeftSideEnemy : Enemy
{
	private float ordinaryXForwardBorder;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		this.CreateBullet(this.NormalBullet);
		if (this.tag.Equals("Enemy1"))
		{
			this.ordinaryXForwardBorder = -20.0f;
		}
		else if (this.tag.Equals("Enemy2"))
		{
			this.ordinaryXForwardBorder = 67.0f;
		}

	}

	protected override void Update()
	{
		base.Update();
		if (this.Trans.position.x > this.ordinaryXForwardBorder)
		{
			Debug.Log(this.ordinaryXForwardBorder);
			this.XForwardEnemy(this.ordinaryXForwardBorder);
		}
		else
		{
			this.ToSideMove();
		}
	}

	public void XForwardEnemy(float _borderX)
	{
		Vector3 pos = this.Trans.position;
		pos.x -= this.ordinaryForwardSpeed * Time.deltaTime;
		this.Trans.position = pos;
	}

	protected void ToSideMove()
	{
		var pos = this.Trans.position;
		pos.x -= this.MoveSpeedRate * Time.deltaTime;
		this.Trans.position = pos;
	}
}