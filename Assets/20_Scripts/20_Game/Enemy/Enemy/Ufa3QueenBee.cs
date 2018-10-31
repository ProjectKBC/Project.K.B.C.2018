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
	Suicide,
	GoPlayer
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
	[SerializeField] private GameObject childNeedlePrefab;
	[SerializeField] private GameObject beamPrefab;
	[SerializeField] private BossBarrageConfig[] barrageConfigs;

	private int needlePoolNum = 500;
	private int childBeePoolNum = 10;
	private int beamPoolNum = 3;

	private QueenBeeBullet[] needles;
	private QueenBeeBullet[] childBees;
	private QueenBeeBullet[] beams;

	private List<QueenBeeBullet> occupiedFunnel;

	private int barrageCount = 0;

	protected override void Awake()
	{
		base.Awake();
		this.AwakingMethods();
		this.occupiedFunnel = new List<QueenBeeBullet>();
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

		this.UpdatingMethods();
		this.CheckFunnel();
		
	}

	protected void AwakingMethods()
	{
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
			this.childBees[i].ChildNeedles = new QueenBeeBullet[10];
			for (int k = 0; k < this.childBees[i].ChildNeedles.Length; k++)
			{
				this.childBees[i].ChildNeedles[k] = new QueenBeeBullet();
				this.childBees[i].ChildNeedles[k].Bullet = Instantiate(this.childNeedlePrefab);
				this.childBees[i].ChildNeedles[k].TagType = this.tag;
				this.childBees[i].ChildNeedles[k].OnInit();
			}

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

	protected void UpdatingMethods()
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

	private void CheckFunnel()
	{
		for (int i = 0; i < this.occupiedFunnel.Count; i += 1)
		{
			if (!this.occupiedFunnel[i].Bullet.activeSelf)
			{
				this.occupiedFunnel.RemoveAt(i);
			}
		}
	}

	protected void LinkedFunnel()
	{
		for (int i = 0; i < this.occupiedFunnel.Count; i += 1)
		{
			if (this.occupiedFunnel[i].Bullet.activeSelf)
			{
				this.occupiedFunnel[i].Attack = true;
			}
		}
	}

	protected void QueenAttack()
	{
		switch (this.barrageConfigs[this.barrageCount].Barrage.ToString())
		{
			case "Fan":
				this.Fan(20, this.barrageConfigs[this.barrageCount].Barrage);
				this.LinkedFunnel();
				break;

			case "DoubleFan":
				this.fanNum = 12;
				this.RightFan(this.fanNum, this.barrageConfigs[this.barrageCount].Barrage);
				this.LeftFan(this.fanNum, this.barrageConfigs[this.barrageCount].Barrage);
				this.LinkedFunnel();
				break;

			case "Funnel":
				this.Funnel(new Vector2(-15.0f, 40.0f), 1.0f, this.barrageConfigs[this.barrageCount].Barrage);
				this.Funnel(new Vector2(-72.0f, 40.0f), 1.0f, this.barrageConfigs[this.barrageCount].Barrage);
				break;

			case "Beam":
				this.Beam(3.0f, this.barrageConfigs[this.barrageCount].Barrage);
				break;

			case "Divide":
				this.Beam(3.0f, this.barrageConfigs[this.barrageCount].Barrage);
				this.Fan(10, this.barrageConfigs[this.barrageCount].Barrage);
				break;

			case "Suicide":
				this.Suicide(6, 25, this.barrageConfigs[this.barrageCount].Barrage);
				break;
		}

		this.Pass += this.PassInterval;
		this.barrageCount++;
	}

	protected void Fan(int _fanNum, Barrage _barrage)
	{
		Vector3 pos = this.Trans.position;
		pos.y -= 10.0f;
		int appearSpace = (this.maxRad - this.minRad) / (_fanNum - 1);
		for (int i = 0; i < _fanNum; i += 1)
		{
			int needleNumber = this.SearchAvailableBullet(this.needles);
			QueenBeeBullet bullet = this.needles[needleNumber];
			bullet.BulletType = _barrage;
			bullet.Bullet.transform.position = pos;
			bullet.Bullet.transform.rotation = new Quaternion(0.7f, 0.0f, 0.0f, 0.7f);
			bullet.Bullet.transform.Rotate(new Vector3(0, 1, 0), this.minRad + i * appearSpace);
			bullet.Bullet.SetActive(true);
		}
	}

	protected void RightFan(int _fanNum, Barrage _barrage)
	{
		Vector3 pos = this.Trans.position;
		pos.x += 10.5f;
		int appearSpace = (this.maxRad - this.minRad) / (_fanNum - 1);
		for (int i = 0; i < _fanNum; i += 1)
		{
			int needleNumber = this.SearchAvailableBullet(this.needles);
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
		int appearSpace = (this.maxRad - this.minRad) / (_fanNum - 1);
		for (int i = 0; i < _fanNum; i += 1)
		{
			int needleNumber = this.SearchAvailableBullet(this.needles);
			QueenBeeBullet bullet = this.needles[needleNumber];
			bullet.BulletType = _barrage;
			bullet.Bullet.transform.position = pos;
			bullet.Bullet.transform.rotation = new Quaternion(0.7f, 0.0f, 0.0f, 0.7f);
			bullet.Bullet.transform.Rotate(new Vector3(0, 1, 0), this.minRad + i * appearSpace);
			bullet.Bullet.SetActive(true);
		}
	}

	protected void Beam(float _attackTime, Barrage _barrage)
	{
		Vector3 pos = this.Trans.position;
		int beamNumber = this.SearchAvailableBullet(this.beams);
		QueenBeeBullet bullet = this.beams[beamNumber];
		bullet.BulletType = _barrage;
		bullet.Bullet.transform.position = pos;
		bullet.AttackTime = _attackTime;
		bullet.Bullet.gameObject.SetActive(true);
	}

	protected void Funnel(Vector2 _toPos, float _moveSecond, Barrage _barrage)
	{
		Vector3 pos = this.Trans.position;
		float distance = Vector2.Distance(pos, _toPos);
		int childBeeNumber = this.SearchAvailableBullet(this.childBees);
		QueenBeeBullet bullet = this.childBees[childBeeNumber];
		bullet.BulletType = _barrage;
		bullet.Bullet.transform.position = pos;
		bullet.HitPoint = 5;
		bullet.ToPos = _toPos;
		bullet.ToMoveSpeed = distance / _moveSecond;
		bullet.VectorMyselfPosition = new Vector2(_toPos.x - pos.x, _toPos.y - pos.y).normalized;
		bullet.MovingFlag = true;
		bullet.Bullet.gameObject.SetActive(true);
		this.occupiedFunnel.Add(bullet);
	}

	protected void Suicide(int _fanNum, float _radius, Barrage _barrage)
	{
		Vector3 pos = this.Trans.position;
		float angle = 360.0f / (_fanNum - 1) * Mathf.Deg2Rad;
		for (int i = 0; i < _fanNum; i += 1)
		{
			int childBeeNumber = this.SearchAvailableBullet(this.childBees);
			QueenBeeBullet bullet = this.childBees[childBeeNumber];
			bullet.Bullet.transform.position = pos;
			bullet.Angle = angle * i;
			bullet.StartPosition = pos;
			bullet.BulletType = _barrage;
			bullet.MaxRadius = _radius;
			bullet.Bullet.gameObject.SetActive(true);
			this.occupiedFunnel.Add(bullet);
		}
	}

	protected int SearchAvailableBullet(QueenBeeBullet[] _bullets)
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

	protected int SeachUsedBullet(QueenBeeBullet[] _bullets)
	{
		for (int i = 0; i < _bullets.Length; i++)
		{
			if (_bullets[i].Bullet.gameObject.activeSelf)
			{
				return i;
			}
		}

		return -1;
	}
}