using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBee : Enemy {
	
	[SerializeField]
	// 何秒で移動するか、X座標、Y座標がループして入っている
	private float[] movePosData;
	
	// 上の配列を回すためのカウント変数
	private int movePosDataCount = 0;
	
	protected Vector3 VectorMyselfPosition;


	
	//private int bulletPool = 10;
	// データがあるかどうか（最新か）のフラグ、何秒で移動するか、X座標、Y座標が入ってる
	private float[] moveData;

	protected override void Awake()
	{
		base.Awake();
		this.moveData = new float[4];
	}

	protected override void Start()
	{
		this.CreateBullet(this.NomalBullet);
	}

	protected override void Update()
	{
		base.Update();
		float nowPass = Mathf.Floor (this.ElapsedTime * 10) / 10;


		//Debug.Log(this.movePosDataCount < this.movePosData.Length);
		
		// 移動しきってなかったら
		if (this.movePosDataCount < this.movePosData.Length)
		{
			SecondMovePosition(this.movePosData[this.movePosDataCount], this.movePosData[this.movePosDataCount + 1],
				this.movePosData[this.movePosDataCount + 2]);
		}
	}

	private void SecondMovePosition(float _time, float _posX, float _posY)
	{
		if (this.moveData[0] <= 0)
		{
			float d = (float) System.Math.Sqrt((_posX - this.Trans.position.x) * (_posX - this.Trans.position.x)
			                                   + (_posY - this.Trans.position.y) * (_posY - this.Trans.position.y));
			this.moveData[1] = d / _time;
			this.moveData[2] = _posX;
			this.moveData[3] = _posY;
			this.moveData[0] = 1;
			this.VectorMyselfPosition = new Vector3(this.moveData[2] - this.Trans.position.x,
				this.moveData[3] - this.Trans.position.y, this.Trans.position.z).normalized;
		}
		else
		{
			Vector3 myNowPos = this.Trans.position;
			
			myNowPos.x += (this.VectorMyselfPosition.x * this.moveData[1] * Time.deltaTime);
			myNowPos.y += (this.VectorMyselfPosition.y * this.moveData[1] * Time.deltaTime);
			
			this.Trans.position = myNowPos;

			if (this.VectorMyselfPosition.x >= 0 && this.VectorMyselfPosition.y >= 0)
			{
				if (this.Trans.position.x >= this.moveData[2] && this.Trans.position.y >= this.moveData[3])
				{
					this.moveData[0] = -1;
					this.movePosDataCount += 3;
				}
			}
			else if (this.VectorMyselfPosition.x < 0 && this.VectorMyselfPosition.y >= 0)
			{
				if (this.Trans.position.x < this.moveData[2] && this.Trans.position.y >= this.moveData[3])
				{
					this.moveData[0] = -1;
					this.movePosDataCount += 3;
				}
			}
			else if (this.VectorMyselfPosition.x >= 0 && this.VectorMyselfPosition.y < 0)
			{
				if (this.Trans.position.x >= this.moveData[2] && this.Trans.position.y < this.moveData[3])
				{
					this.moveData[0] = -1;
					this.movePosDataCount += 3;
				}
			}
			else if (this.VectorMyselfPosition.x < 0 && this.VectorMyselfPosition.y < 0)
			{
				if (this.Trans.position.x < this.moveData[2] && this.Trans.position.y < this.moveData[3])
				{
					this.moveData[0] = -1;
					this.movePosDataCount += 3;
				}
			}
		}
	}
}
