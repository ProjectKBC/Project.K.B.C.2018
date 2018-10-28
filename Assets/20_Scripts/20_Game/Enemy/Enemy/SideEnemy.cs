using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEnemy : Enemy
{
	[SerializeField]
	private bool toRightMove = false;
	
	private float ordinaryXForwardBorder;

	protected override void Awake()
	{
		if (this.tag.Equals("Enemy1"))
		{
			if (this.toRightMove)
			{
				this.ordinaryXForwardBorder = -67.0f;
			}
			else
			{
				this.ordinaryXForwardBorder = -20.0f;
			}
		}
		else if (this.tag.Equals("Enemy2"))
		{
			if (this.toRightMove)
			{
				this.ordinaryXForwardBorder = 20.0f;
			}
			else
			{
				this.ordinaryXForwardBorder = 67.0f;
			}
		}
		base.Awake();
	}
	
	protected override void Start()
	{
		this.CreateBullet(this.NormalBullet);
	}
	
	protected override void Update()
	{
		base.Update();
		if (this.Trans.position.x > this.ordinaryXForwardBorder)
		{
			this.XForwardEnemy(this.ordinaryXForwardBorder);
		}
		else
		{
			this.SideMove();
		}
	}

	protected void SideMove()
	{
		var pos = this.Trans.position;
		if (this.toRightMove)
		{
			pos.x += this.MoveSpeedRate * Time.deltaTime;
		}
		else
		{
			pos.x -= this.MoveSpeedRate * Time.deltaTime;
		}
		this.Trans.position = pos;
	}
	
}
