using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayEnemy : Enemy
{

	[SerializeField]
	private float stayTime = 2.0f;

	//private float untilStayTime;
	
	protected override void Update()
	{
		base.Update();
		float nowPass = Mathf.Floor(this.ElapsedTime * 10) / 10;
		
		if (nowPass >= this.stayTime)
		{
			base.BackMove();
		}
		else
		{
			if (this.Trans.position.y > ordinaryForwardBorder)
			{
				ForwardEnemy(ordinaryForwardBorder);
			}
			if (nowPass >= this.Pass)
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
