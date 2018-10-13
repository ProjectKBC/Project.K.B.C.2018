using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    // 定数はstatic readonlyに
    // public と protected はアクセサーに
    // gameobject と transform はキャッシュをとる by flanny
    protected static readonly Vector3 SpownPos = new Vector3 (0, 0, 200);
    protected static readonly int BulletPool = 60;
    protected static readonly float Bottom = -80.0f;

    public int HitPoint { get; protected set; }
    public bool IsBurstAttack = false;
    public GameObject Go { get; protected set; }
    public Transform Trans { get; protected set; }

    public float MoveSpeedRate { get { return this.moveSpeedRate; } protected set { this.moveSpeedRate = value; } }
    public float NomalBulletSpeed { get { return this.nomalBulletSpeed; } protected set { this.nomalBulletSpeed = value; } }
    public GameObject NomalBullet { get { return this.nomalBullet; } protected set { this.nomalBullet = value; } }
    public float PassInterval { get { return this.passInterval; } protected set { this.passInterval = value; } }

    protected int FixHitPoint { get { return this.fixHitPoint; } set { this.fixHitPoint = value; } }
    protected List<GameObject> NomalBullets { get; set; }
    protected float ElapsedTime { get; private set; }
    protected float Pass { get; set; }

    [SerializeField]
    private int fixHitPoint;
    [SerializeField]
    private float moveSpeedRate;
    [SerializeField]
    private float nomalBulletSpeed;
    [SerializeField]
    private GameObject nomalBullet;
    [SerializeField]
    private float passInterval = 1.5f;
    [SerializeField]
    private int burstBulletNumber = 3;

    protected float ordinaryForwardBorder = 35.0f;
    protected float ordinaryForwardSpeed = 30.0f;
    private int burstCount;
    private float burstBulletInterval = 0.2f;
    private float rightEnd;
    private float leftEnd;

    protected virtual void Awake ()
    {
        
        this.Go = this.gameObject;
        this.Trans = this.Go.transform;

        this.HitPoint = this.fixHitPoint;
        this.ElapsedTime = 0.0f;
        this.NomalBullets = new List<GameObject> ();
        CreateBullet(this.NomalBullet);

        this.Pass = this.PassInterval;
        burstCount = burstBulletNumber;
    }

    protected virtual void Update ()
    {
        ElapsedTime += Time.deltaTime;
        float nowPass = Mathf.Floor (this.ElapsedTime * 10) / 10;
        //if (nowPass.Equals(this.Pass)) // ==やEqualsだと値も型も同じなのに挙動がおかしい、見えない小数がある？今はまだ原因不明
        /*
        if (nowPass >= this.Pass)
        {
            if (IsBurstAttack)
            {
                BurstAttack ();
            }
            else
            {
                NormalAtack ();
            }
        }
        */
        Dead ();
        //NomalAttack ();
        BeyondLine ();
    }

    protected virtual void OnDisable ()
    {
        this.HitPoint = this.fixHitPoint;
        this.ElapsedTime = 0.0f;
        this.Pass = this.PassInterval;
    }

    public void Dead ()
    {
        if (this.HitPoint <= 0)
        {
            HideEnemy ();
        }
    }

    public void BeyondLine ()
    {
        if (this.Trans.position.y < Bottom)
        {
            HideEnemy ();
        }
    }

    public void CreateBullet (GameObject _obj)
    {
        for (int i = 0; i < BulletPool; i++)
        {
            var bullet = Instantiate(_obj);
            //bullet.transform.SetParent(this.transform, true);
            HideBullet(bullet);
            this.NomalBullets.Add(bullet);
        }
    }

    public GameObject SearchAvailableBullet ()
    {
        for (int i = 0; i < this.NomalBullets.Count; i++)
        {
            if (!this.NomalBullets [i].gameObject.activeSelf)
            {
                return this.NomalBullets[i];
            }
        }

        return null;
    }

    public void BulletAppear(GameObject _bullet)
    {
        //GameObject bullet = this.SearchAvailableBullet();
        _bullet.transform.position = this.Trans.position;
        _bullet.gameObject.SetActive (true);
    }

    public void ForwardEnemy (float _borderY)
    {
        // ある程度まで前進する
        /*
        if (this.Trans.position.y > _borderY)
        {
            Vector3 pos = this.Trans.position;
            pos.y -= this.ordinaryForwardSpeed + Time.deltaTime;
            this.Trans.position = pos;
        }
        */
        Vector3 pos = this.Trans.position;
        pos.y += -this.ordinaryForwardSpeed * Time.deltaTime;
        this.Trans.position = pos;
    }

    public void NormalAtack ()
    {
      BulletAppear (SearchAvailableBullet());
      this.Pass += PassInterval;
    }

    public void BurstAttack ()
    {
        BulletAppear (SearchAvailableBullet());
        burstCount -= 1;
        if (burstCount <= 0)
        {
            this.Pass += this.PassInterval + burstBulletInterval * burstBulletNumber;
            this.burstCount = burstBulletNumber;
        }
        else
        {
            this.Pass += burstBulletInterval;
        }
    }

    public void HideEnemy ()
    {
        this.Go.SetActive (false);
        this.Trans.position = SpownPos;
    }

    public void HideBullet (GameObject _bullet)
    {
        _bullet.gameObject.SetActive (false);
        _bullet.transform.position = SpownPos;
    }

    public bool IsHit ()
    {
        return false;
    }
}