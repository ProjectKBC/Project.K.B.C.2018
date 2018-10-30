using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Barrage
{
	Fan,
	DoubleFan,
	Funnel,
	Beam,
	Divide,
	Suicide
}

public class Ufa3QueenBee : Enemy
{
	[System.Serializable]
	public class BossBarrageConfig
	{
		public Barrage Barrage;
		public float Second;
	}

	protected Vector3[] FanPosition;
	private int fanNum;
	private int minRad = -60;
	private int maxRad = 60;

	[SerializeField] private GameObject childBeePrefab;
	[SerializeField] private GameObject rightBee;
	[SerializeField] private GameObject leftBee;
	[SerializeField] private GameObject beamPrefab;
	[SerializeField] private int normalBulletPool;

	[SerializeField] private BossBarrageConfig[] barrageConfigs;

	private int needlePoolNum = 100;
	private int childBeePoolNum = 10;
	private int beamPoolNum = 3;

	private QueenBeeBullet[] needles;
	private QueenBeeBullet[] childBees;
	private QueenBeeBullet[] beams;

	private int barrageCount = 0;

	protected override void Awake()
	{
		base.Awake();
		this.needles = new QueenBeeBullet[this.needlePoolNum];
		for (int i = 0; i < this.needles.Length; i++)
		{
			this.needles[i] = new QueenBeeBullet();
			this.needles[i].Bullet = Instantiate(this.NormalBullet);
			this.needles[i].TagType = this.tag;
			this.needles[i].OnInit();
		}

		this.childBees = new QueenBeeBullet[this.childBeePoolNum];
		for (int i = 0; i < this.childBees.Length; i++)
		{
			this.childBees[i] = new QueenBeeBullet();
			this.childBees[i].Bullet = Instantiate(this.childBeePrefab);
			this.childBees[i].TagType = this.tag;
			this.childBees[i].OnInit();
		}

		this.beams = new QueenBeeBullet[this.beamPoolNum];
		for (int i = 0; i < this.beams.Length; i++)
		{
			this.beams[i] = new QueenBeeBullet();
			this.beams[i].Bullet = Instantiate(this.beamPrefab);
			this.beams[i].TagType = this.tag;
			this.beams[i].OnInit();
		}
	}

	protected override void Start()
	{
	}

	protected override void Update()
	{
		this.ElapsedTime += Time.deltaTime;
		this.Dead();
		this.BeyondLine();
		if (this.Trans.position.y > this.ordinaryYForwardBorder)
		{
			this.YForwardEnemy(this.ordinaryYForwardBorder);
		}
		else
		{
			if (this.barrageCount < this.barrageConfigs.Length)
			{
				if (this.ElapsedTime >= this.barrageConfigs[this.barrageCount].Second)
				{
					this.QueenAttack();
				}
			}
		}

		this.RunningMethods();
	}

	protected void RunningMethods()
	{
		for (int i = 0; i < this.needles.Length; i++)
		{
			this.needles[i].Run();
		}

		for (int i = 0; i < this.beams.Length; i++)
		{
			this.beams[i].Run();
		}

		for (int i = 0; i < this.childBees.Length; i++)
		{
			this.childBees[i].Run();
		}
	}

	protected void QueenAttack()
	{
		switch (this.barrageConfigs[this.barrageCount].Barrage.ToString())
		{
			case "Fan":
				this.Fan(11);
				break;

			case "DoubleFan":
				this.fanNum = UnityEngine.Random.Range(8, 10);
				this.RightFan(this.fanNum, this.barrageConfigs[this.barrageCount].Barrage);
				this.LeftFan(this.fanNum, this.barrageConfigs[this.barrageCount].Barrage);
				break;

			case "Funnel":
				this.Funnel(this.barrageConfigs[this.barrageCount].Barrage);
				this.Funnel(this.barrageConfigs[this.barrageCount].Barrage);
				break;

			case "Beam":
				this.Beam();
				break;

			case "Divide":
				this.Beam();
				this.Fan(10);
				break;

			case "Suicide":
				this.Suicide(6, 25, this.barrageConfigs[this.barrageCount].Barrage);
				break;
		}

		this.Pass += this.PassInterval;
		this.barrageCount++;
	}

	protected void Fan(int _fanNum)
	{
		Vector3 pos = this.Trans.position;
		int appearSpace = (this.maxRad - this.minRad) / _fanNum;
		for (int i = 0; i < _fanNum; i += 1)
		{
			int needleNumber = this.SearchBullet(this.needles);
			this.needles[needleNumber].Bullet.transform.position = pos;
			this.needles[needleNumber].Bullet.transform.rotation = new Quaternion(0.7f, 0.0f, 0.0f, 0.7f);
			this.needles[needleNumber].Bullet.transform.Rotate(new Vector3(0, 1, 0), this.minRad + i * appearSpace);
			this.needles[needleNumber].Bullet.SetActive(true);
		}
	}

	protected void RightFan(int _fanNum, Barrage _barrage)
	{
		Vector3 pos = this.Trans.position;
		pos.x += 10.5f;
		int appearSpace = (this.maxRad - this.minRad) / _fanNum;
		for (int i = 0; i < _fanNum; i += 1)
		{
			int needleNumber = this.SearchBullet(this.needles);
			QueenBeeBullet bullet = this.needles[needleNumber];
			bullet.BulletType = _barrage;
			bullet.Bullet.transform.position = pos;
			bullet.Bullet.transform.rotation = new Quaternion(0.7f, 0.0f, 0.0f, 0.7f);
			bullet.Bullet.transform.Rotate(new Vector3(0, 1, 0), this.minRad + i * appearSpace);
			bullet.Bullet.SetActive(true);
		}
	}

	protected void LeftFan(int _fanNum, Barrage _barrage)
	{
		Vector3 pos = this.Trans.position;
		pos.x -= 10.5f;
		int appearSpace = (this.maxRad - this.minRad) / _fanNum;
		for (int i = 0; i < _fanNum; i += 1)
		{
			int needleNumber = this.SearchBullet(this.needles);
			QueenBeeBullet bullet = this.needles[needleNumber];
			bullet.BulletType = _barrage;
			bullet.Bullet.transform.position = pos;
			bullet.Bullet.transform.rotation = new Quaternion(0.7f, 0.0f, 0.0f, 0.7f);
			bullet.Bullet.transform.Rotate(new Vector3(0, 1, 0), this.minRad + i * appearSpace);
			bullet.Bullet.SetActive(true);
		}
	}

	protected void Beam()
	{
		int beamNumber = this.SearchBullet(this.beams);
		QueenBeeBullet bullet = this.beams[beamNumber];
		bullet.Bullet.transform.position = this.Trans.position;
		bullet.Bullet.gameObject.SetActive(true);
	}

	protected void Funnel(Barrage _barrage)
	{
		Vector3 pos = this.Trans.position;
		int childBeeNumber = this.SearchBullet(this.childBees);
		QueenBeeBullet bullet = this.childBees[childBeeNumber];
		bullet.BulletType = _barrage;
		bullet.Bullet.transform.position = pos;
		bullet.Bullet.gameObject.SetActive(true);
	}

	protected void Suicide(int _fanNum, float _radius, Barrage _barrage)
	{
		Vector3 pos = this.Trans.position;
		float angle = 360.0f / (_fanNum - 1) * Mathf.Deg2Rad;
		for (int i = 0; i < _fanNum; i += 1)
		{
			/*
			int childBeeNumber = this.SearchBullet(this.childBees);
			this.childBees[childBeeNumber].Bullet.transform.position = pos;
			this.childBees[childBeeNumber].Angle = angle * i;
			this.childBees[childBeeNumber].StartPosition = pos;
			this.childBees[childBeeNumber].BulletType = _barrage;
			this.childBees[childBeeNumber].MaxRadius = _radius;
			this.childBees[childBeeNumber].Bullet.gameObject.SetActive(true);
			*/
			int childBeeNumber = this.SearchBullet(this.childBees);
			QueenBeeBullet bullet = this.childBees[childBeeNumber];
			bullet.Bullet.transform.position = pos;
			bullet.Angle = angle * i;
			bullet.StartPosition = pos;
			bullet.BulletType = _barrage;
			bullet.MaxRadius = _radius;
			bullet.Bullet.gameObject.SetActive(true);
		}
	}

	protected int SearchBullet(QueenBeeBullet[] _bullets)
	{
		for (int i = 0; i < _bullets.Length; i++)
		{
			if (!_bullets[i].Bullet.gameObject.activeSelf)
			{
				return i;
			}
		}

		return -1;
	}
}