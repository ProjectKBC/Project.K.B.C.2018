using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // 定数はstatic readonlyに
    // public と protected はアクセサーに
    // gameobject と transform はキャッシュをとる by flanny
    protected static readonly Vector3 SpownPos = new Vector3(0, 0, 200);
    protected static readonly float Bottom = -80.0f;
	protected static readonly float Right = 120.0f;
	protected static readonly float Left = -120.0f;
    protected static readonly float Top = 120.0f;

    public int HitPoint { get; protected set; }
    public bool IsBurstAttack = false;
    public GameObject Go { get; protected set; }
    public Transform Trans { get; protected set; }

    public float MoveSpeedRate
    {
        get { return this.moveSpeedRate; }
        protected set { this.moveSpeedRate = value; }
    }

    public float NomalBulletSpeed
    {
        get { return this.nomalBulletSpeed; }
        protected set { this.nomalBulletSpeed = value; }
    }

    public GameObject NormalBullet
    {
        get { return this.nomalBullet; }
        protected set { this.nomalBullet = value; }
    }

    public float PassInterval
    {
        get { return this.passInterval; }
        protected set { this.passInterval = value; }
    }

    protected int FixHitPoint
    {
        get { return this.fixHitPoint; }
        set { this.fixHitPoint = value; }
    }

    protected List<GameObject> NormalBullets;
    protected float ElapsedTime { get; set; }
    protected float Pass { get; set; }

    [SerializeField] private int fixHitPoint;
    [SerializeField] private float moveSpeedRate;
    [SerializeField] private float nomalBulletSpeed;
    [SerializeField] private GameObject nomalBullet;
    [SerializeField] private float passInterval = 1.5f;
    [SerializeField] protected int BurstBulletNumber = 3;

    protected int bulletPool = 20;
    protected float ordinaryYForwardBorder = 35.0f;
    protected float ordinaryForwardSpeed = 40.0f;
    protected int BurstCount;
    protected float burstBulletInterval = 0.2f;
    private float rightEnd;
    private float leftEnd;

    protected virtual void Awake()
    {
        this.Go = this.gameObject;
        this.Trans = this.Go.transform;

        this.HitPoint = this.fixHitPoint;
        this.ElapsedTime = 0.0f;

        this.Pass = this.PassInterval;
        this.BurstCount = this.BurstBulletNumber;
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        this.ElapsedTime += Time.deltaTime;
	    this.ShotForm();
        this.Dead();
        this.BeyondLine();
    }

    protected virtual void OnDisable()
    {
        this.HitPoint = this.fixHitPoint;
        this.ElapsedTime = 0.0f;
        this.Pass = this.PassInterval;
    }

    public void Dead()
    {
        if (this.HitPoint <= 0)
        {
            this.HideEnemy();
        }
    }

    public void BeyondLine()
    {
        if (this.Trans.position.y < Bottom)
        {
            this.HideEnemy();
        }

	    if (this.Trans.position.x < Left)
	    {
		    this.HideEnemy();
	    }
	    
	    if (Right < this.Trans.position.x)
	    {
		    this.HideEnemy();
	    }

	    /*
        if (this.Trans.position.y > Top)
        {
            this.HideEnemy();
        }
        */
	    
	    
    }

    public void CreateBullet(GameObject _obj)
    {
        this.NormalBullets = new List<GameObject>();
        
        for (int i = 0; i < this.bulletPool; i++)
        {
            var bullet = Instantiate(_obj);
            bullet.tag = this.tag;
            this.HideBullet(bullet);
            this.NormalBullets.Add(bullet);
        }
    }

    public GameObject SearchAvailableBullet()
    {
        
        for (int i = 0; i < this.NormalBullets.Count; i++)
        {
            if (!this.NormalBullets[i].gameObject.activeSelf)
            {
                return this.NormalBullets[i];
            }
        }

        return null;
    }

    public GameObject SearchAvailableBullet(List<GameObject> _bulletPools)
    {
        for (int i = 0; i < _bulletPools.Count; i++)
        {
            if (!_bulletPools[i].gameObject.activeSelf)
            {
                return _bulletPools[i];
            }
        }

        return null;
    }

    public void BulletAppear(GameObject _bullet)
    {
        //GameObject bullet = this.SearchAvailableBullet();
        _bullet.transform.position = this.Trans.position;
        _bullet.gameObject.SetActive(true);
    }

    public void YForwardEnemy(float _borderY)
    {
        Vector3 pos = this.Trans.position;
        pos.y += -this.ordinaryForwardSpeed * Time.deltaTime;
        this.Trans.position = pos;
    }
	
    public void NormalAtack()
    {
        this.BulletAppear(this.SearchAvailableBullet());
        this.Pass += PassInterval;
    }

	public void ShotForm()
	{
		if (this.ElapsedTime >= this.Pass)
		{
			if (this.IsBurstAttack)
			{
				this.BurstAttack ();
			}
			else
			{
				this.NormalAtack ();
			}
		}
	}

    public void BackMove()
    {
        var pos = this.Trans.position;
        pos.y += this.ordinaryForwardSpeed * Time.deltaTime;
        this.Trans.position = pos;
    }

    public void BurstAttack()
    {
        this.BulletAppear(this.SearchAvailableBullet());
        this.BurstCount -= 1;
        if (this.BurstCount <= 0)
        {
            this.Pass += this.PassInterval + burstBulletInterval * this.BurstBulletNumber;
            this.BurstCount = this.BurstBulletNumber;
        }
        else
        {
            this.Pass += burstBulletInterval;
        }
    }

    public void HideEnemy()
    {
        this.Go.SetActive(false);
        this.Trans.position = SpownPos;
    }

    public void HideBullet(GameObject _bullet)
    {
        _bullet.gameObject.SetActive(false);
        _bullet.transform.position = SpownPos;
    }

    public bool IsHit()
    {
        return false;
    }
}