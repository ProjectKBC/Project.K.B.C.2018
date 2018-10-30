using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBeeBullet {

	protected static readonly Vector3 SpownPos = new Vector3(0, 0, 200);
	protected static readonly float Top = 80.0f;
	protected static readonly float Bottom = -80.0f;
	protected float LeftPosX;
	protected float RightPosX;
	protected float ElapsedTime { get; set; }

	public Transform Trans { get; protected set; }
	

	
	public Barrage BulletType { get; set; }
	public float MaxRadius { get; set; }

	public string TagType;

	public GameObject Bullet;
	public Vector3 StartPosition;
	public float Angle;
	public float SuicideSpeed = 3.0f;
	private float rotationRadian;
	public float BulletSpeed = 20.0f;

	public void OnInit()
	{
		this.Bullet.SetActive(false);
		this.Trans = this.Bullet.transform;
		this.Bullet.transform.position = SpownPos;
		this.Bullet.tag = this.TagType;
		this.ElapsedTime = 0.0f;
		this.rotationRadian = 0.0f;

		if (this.Bullet.tag.Equals("Enemy1"))
		{
			this.LeftPosX = -120.0f;
			this.RightPosX = 30.0f;
		}
		else if (this.Bullet.tag.Equals("Enemy2"))
		{
			this.LeftPosX = -30.0f;
			this.RightPosX = 120.0f;
		}
	}

	public void Run()
	{
		if (this.Bullet.activeSelf)
		{
			this.ElapsedTime += Time.deltaTime;
			this.BulletMode();
			this.BeyondLine();
		}

	}

	private void BulletMode()
	{
		switch (this.BulletType.ToString())
		{
			case "DoubleFan":
				this.Fan();
				break;
			
			case "Suicide":
				this.Suicide();
				break;
			
		}

	}
	
	private void Fan()
	{
		this.Bullet.transform.Translate(Vector3.forward * this.BulletSpeed * Time.deltaTime);
	}

	/*
	private void RightFan()
	{
		
	}
	
	private void LeftFan()
	{
		
	}
	*/

	private void Funnel()
	{
		
	}

	private void Beam()
	{
		
	}

	private void Divide()
	{
		
	}
	
	private void Suicide()
	{
		float distance = Vector3.Distance(this.StartPosition, this.Trans.position);
		if (distance < this.MaxRadius)
		{
			Vector3 pos = this.Trans.position;
			pos.x += Mathf.Cos(this.Angle * this.SuicideSpeed);
			pos.y += Mathf.Sin(this.Angle * this.SuicideSpeed);
			this.Trans.position = pos;
		}
		else
		{
			Vector3 pos = this.Trans.position;
			pos.x = this.StartPosition.x + Mathf.Cos(this.Angle * this.SuicideSpeed + this.rotationRadian) * this.MaxRadius * 1.1f;
			pos.y = this.StartPosition.y + Mathf.Sin(this.Angle * this.SuicideSpeed + this.rotationRadian) * this.MaxRadius * 1.1f;
			this.Trans.position = pos;
			this.rotationRadian += 0.05f;
		}
	}
	
	private void BeyondLine()
	{
		if (this.Bullet.transform.position.y < Bottom || this.Bullet.transform.position.y > Top
		                                   || this.Bullet.transform.position.x < this.LeftPosX ||
		                                   this.Bullet.transform.position.x > this.RightPosX)
		{
			this.HideBullet();
		}
	}
	
	private void HideBullet()
	{
		this.Bullet.SetActive(false);
		this.Bullet.transform.position = SpownPos;
		this.rotationRadian = 0.0f;
	}
}
