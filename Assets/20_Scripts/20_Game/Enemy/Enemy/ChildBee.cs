using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBee : Enemy {

	[System.Serializable]
	class GoPosData
	{
		public float Second;
		public float PosX;
		public float PosY;
		
		[HideInInspector]
		public float Speed;
		[HideInInspector]
		public bool MoveFlag = false;

		public GoPosData(float _second, float _posX, float _posY)
		{
			this.Second = _second;
			this.PosX = _posX;
			this.PosY = _posY;
		}
	}

	[SerializeField]
	private GoPosData[] movePosDatas;
	
	// 上の配列を回すためのカウント変数
	private int movePosDataCount = 0;
	
	protected Vector3 VectorMyselfPosition;
	protected Vector3 PlayerPosition;
	protected Vector3 MyselfPosition;
	protected Quaternion MyselfRotate;
	protected Vector3 RotateDistance;

	private int minRad = -60;
	private int maxRad = 60;

	private float t = 0;


	
	//private int bulletPool = 10;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		this.CreateBullet(this.NormalBullet);
	}

	protected override void Update()
	{
		base.Update();
		//float nowPass = Mathf.Floor (this.ElapsedTime * 10) / 10;


		//Debug.Log(this.movePosDataCount < this.movePosData.Length);
		
		// 移動しきってなかったら
		
		if (this.movePosDataCount < this.movePosDatas.Length)
		{
			SecondMovePosition(this.movePosDatas[this.movePosDataCount]);
		}
		else
		{
			if (this.PlayerPosition.Equals(null))
			{
				if (this.tag.Equals("Enemy1"))
				{
					this.PlayerPosition = PlayerManager.GameObjectPl1.transform.position;
					Debug.Log(this.PlayerPosition);
				}
				else if (this.tag.Equals("Enemy2"))
				{
					this.PlayerPosition = PlayerManager.GameObjectPl2.transform.position;
				}

				this.MyselfPosition = this.Trans.position;
				this.MyselfRotate = this.Trans.rotation;

			}
			
			transform.rotation = Quaternion.Slerp (this.Trans.rotation, Quaternion.LookRotation (PlayerManager.GameObjectPl1.transform.position - this.MyselfPosition), 0.3f);

			//LookPlayer();
		}

	}

	private void SecondMovePosition(GoPosData _movePosDatas)
	{
		if (_movePosDatas.MoveFlag)
		{
			Vector3 myNowPos = this.Trans.position;
			
			myNowPos.x += (this.VectorMyselfPosition.x * _movePosDatas.Speed * Time.deltaTime);
			myNowPos.y += (this.VectorMyselfPosition.y * _movePosDatas.Speed * Time.deltaTime);
			
			this.Trans.position = myNowPos;
			
			if (this.VectorMyselfPosition.x >= 0 && this.VectorMyselfPosition.y >= 0)
			{
				if (this.Trans.position.x >= _movePosDatas.PosX && this.Trans.position.y >= _movePosDatas.PosY)
				{
					_movePosDatas.MoveFlag = false;
					this.movePosDataCount += 1;
				}
			}
			else if (this.VectorMyselfPosition.x < 0 && this.VectorMyselfPosition.y >= 0)
			{
				if (this.Trans.position.x < _movePosDatas.PosX && this.Trans.position.y >= _movePosDatas.PosY)
				{
					_movePosDatas.MoveFlag = false;
					this.movePosDataCount += 1;
				}
			}
			else if (this.VectorMyselfPosition.x >= 0 && this.VectorMyselfPosition.y < 0)
			{
				if (this.Trans.position.x >= _movePosDatas.PosX && this.Trans.position.y < _movePosDatas.PosY)
				{
					_movePosDatas.MoveFlag = false;
					this.movePosDataCount += 1;
				}
			}
			else if (this.VectorMyselfPosition.x < 0 && this.VectorMyselfPosition.y < 0)
			{
				if (this.Trans.position.x < _movePosDatas.PosX && this.Trans.position.y < _movePosDatas.PosY)
				{
					_movePosDatas.MoveFlag = false;
					this.movePosDataCount += 1;
				}
			}
		}
		else
		{
			float d = (float) System.Math.Sqrt(System.Math.Pow(_movePosDatas.PosX - this.Trans.position.x, 2)
			                                   + System.Math.Pow(_movePosDatas.PosY - this.Trans.position.y, 2));
			_movePosDatas.Speed = d / _movePosDatas.Second;
			this.VectorMyselfPosition = new Vector3(_movePosDatas.PosX - this.Trans.position.x,
				_movePosDatas.PosY - this.Trans.position.y, this.Trans.position.z).normalized;
			_movePosDatas.MoveFlag = true;
		}
	}

	private void LookPlayer()
	{
		Debug.Log(this.RotateDistance.y);
		this.Trans.Rotate(new Vector3(0, 1, 0), this.RotateDistance.y);
	}
	
	private void Fan(int _fanNum)
	{
		int appearSpace = (this.maxRad - this.minRad) / (_fanNum + 1);
		for (float i = 1.0f; i <= _fanNum; i += 1.0f)
		{
			GameObject bullet = this.SearchAvailableBullet(NormalBullets);
			bullet.transform.rotation = new Quaternion(0.7f, 0.0f, 0.0f, 0.7f);
			bullet.transform.Rotate(new Vector3(0, 1, 0), this.minRad + i * appearSpace);
			this.BulletAppear(bullet);
		}

	}
}
