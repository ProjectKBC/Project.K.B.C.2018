using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBee : Enemy
{
    public enum Barrage
    {
        Fan,
	    DoubleFan,
        Funnel,
	    Beam,
	    Divide,
	    Suicide
    }
	
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

    [SerializeField]
    private GameObject childBee;
	[SerializeField]
	private GameObject rightBee;
	[SerializeField]
	private GameObject leftBee;
	[SerializeField]
	private GameObject beamPrefab;
    [SerializeField]
    private int normalBulletPool;
	
	
	
	[SerializeField]
	private BossBarrageConfig[] barrageConfigs;

    private List<GameObject> childBees;
	private List<GameObject> beams;
    private int childBeePool = 10;
	private int beamPool = 3;

    private int barrageCount = 0;

    protected override void Awake()
    {
        base.Awake();
	    this.CreateNormalBullet(this.NormalBullet, out this.NormalBullets, this.normalBulletPool);
	    this.CreateNormalBullet(this.childBee, out this.childBees, this.childBeePool);
	    this.CreateNormalBullet(this.beamPrefab, out this.beams, this.beamPool);
	    this.rightBee = Instantiate(this.rightBee);
	    this.rightBee.SetActive(false);
	    this.rightBee.transform.position = SpownPos;
	    this.leftBee = Instantiate(this.leftBee);
	    this.leftBee.SetActive(false);
	    this.leftBee.transform.position = SpownPos;
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
	    
    }
    
    protected void CreateNormalBullet(GameObject _obj, out List<GameObject> _normalBullets, int _bulletNum)
    {
        _normalBullets = new List<GameObject>();
        for (int i = 0; i < _bulletNum; i++)
        {
            var bullet = Instantiate(_obj);
            bullet.tag = this.tag;
            this.HideBullet(bullet);
            _normalBullets.Add(bullet);
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
				this.RightFan(this.fanNum);
				this.LeftFan(this.fanNum);
				break;

			case "Funnel":
				this.Funnel(this.rightBee);
				this.Funnel(this.leftBee);
				break;
			
			case "Beam":
				this.Beam();
				break;
			
			case "Divide":
				this.Beam();
				this.Fan(10);
				break;
			
			case "Suicide":
				this.Suicide(6, 30);
				break;
		}

		this.Pass += this.PassInterval;
		this.barrageCount++;
	}

    protected void Fan(int _fanNum)
    {
        int appearSpace = (this.maxRad - this.minRad) / _fanNum;
        for (float i = 0.0f; i <= _fanNum; i += 1.0f)
        {
            GameObject bullet = this.SearchAvailableBullet(NormalBullets);
            bullet.transform.rotation = new Quaternion(0.7f, 0.0f, 0.0f, 0.7f);
	        bullet.transform.Rotate(new Vector3(0, 1, 0), this.minRad + i * appearSpace);
            this.BulletAppear(bullet);
        }

    }

	protected void RightFan(int _fanNum)
	{
		Vector3 pos = this.Trans.position;
		pos.x += 10.5f;
		int appearSpace = (this.maxRad - this.minRad) / _fanNum;
		for (float i = 0.0f; i <= _fanNum; i += 1.0f)
		{
			GameObject bullet = this.SearchAvailableBullet(NormalBullets);
			bullet.transform.position = pos;
			bullet.transform.rotation = new Quaternion(0.7f, 0.0f, 0.0f, 0.7f);
			bullet.transform.Rotate(new Vector3(0, 1, 0), this.minRad + i * appearSpace);
			bullet.gameObject.SetActive(true);
		}

	}
	
	protected void LeftFan(int _fanNum)
	{
		Vector3 pos = this.Trans.position;
		pos.x -= 10.5f;
		Debug.Log(pos);
		int appearSpace = (this.maxRad - this.minRad) / _fanNum;
		for (float i = 0.0f; i <= _fanNum; i += 1.0f)
		{
			GameObject bullet = this.SearchAvailableBullet(NormalBullets);
			bullet.transform.position = pos;
			bullet.transform.rotation = new Quaternion(0.7f, 0.0f, 0.0f, 0.7f);
			bullet.transform.Rotate(new Vector3(0, 1, 0), this.minRad + i * appearSpace);
			bullet.gameObject.SetActive(true);
		}

	}

	protected void Beam()
	{
		Vector3 pos;
		GameObject beam = this.SearchAvailableBullet(this.beams);
		//beam.transform.position = this.Trans.position;
		this.BulletAppear(beam);
	}

    protected void Funnel(GameObject _funnel)
    {
        //_funnel.transform.position = this.Trans.position;
        this.BulletAppear(_funnel);
    }

	protected void Suicide(int _fanNum, float _radius)
	{
		float angle = 360.0f / _fanNum * Mathf.Deg2Rad;
		for (int i = 0; i < _fanNum; i += 1)
		{
			Vector3 pos = this.Trans.position;
			GameObject bullet = this.SearchAvailableBullet(this.childBees);
			pos.x += _radius * Mathf.Cos (angle * i);
			pos.y += _radius * Mathf.Sin (angle * i);
			bullet.transform.position = pos;
			bullet.gameObject.SetActive(true);
		}
	}
	//protected void 
	
}