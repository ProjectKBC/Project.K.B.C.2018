using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNeedle
{

	protected static readonly Vector3 SpownPos = new Vector3(0, 0, 200);
	protected static readonly float Top = 80.0f;
	protected static readonly float Bottom = -80.0f;
	protected float LeftPosX;
	protected float RightPosX;
	
	public Barrage BulletType { get; set; }
	public float MaxRadius { get; set; }

	public string TagType;

	public GameObject Bullet;
	public float BulletSpeed = 20.0f;

	public void OnInit()
	{
		this.Bullet.SetActive(false);
		this.Bullet.transform.position = SpownPos;
		this.Bullet.tag = this.TagType;
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
			this.Fan();
			this.BeyondLine();
		}

	}
	
	private void Fan()
	{
		this.Bullet.transform.Translate(Vector3.forward * this.BulletSpeed * Time.deltaTime);
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
	}
}
