using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayEnemy : Enemy
{

	[SerializeField]
	private float stayTime = 2.0f;

	//private float untilStayTime;
	
	protected override void Awake()
	{
		base.Awake();
	}
	
	protected override void Start()
	{
		CreateBullet(this.NormalBullet);
	}
	
	protected override void Update()
	{
		ElapsedTime += Time.deltaTime;
		if (ElapsedTime >= this.stayTime)
		{
			base.BackMove();
		}
		else
		{
			if (this.Trans.position.y > ordinaryForwardBorder)
			{
				ForwardEnemy(ordinaryForwardBorder);
			}
			if (ElapsedTime >= this.Pass)
			{
				if (IsBurstAttack)
				{
					BurstAttack();
				}
				else
				{
					NormalAtack();
				}
			}
		}
	}
}
