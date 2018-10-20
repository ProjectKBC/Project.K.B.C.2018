using System.Collections.Generic;
using UnityEngine;

public sealed class KettyBigBee : KettyEnemy {

    public enum Barrage
    {
        Fan,
        Funnel
    }

    protected Vector3[] FanPosition;
    private int fanNum;
    private int minRad = -60;
    private int maxRad = 60;

    [SerializeField]
    private GameObject childBee;
    [SerializeField]
    private int normalBulletPool;

    [SerializeField] private Barrage[] barrage;

    private List<GameObject> childBees;
    private int childBeePool = 5;

    private int barrageCount = 0;

    protected override void OnInit()
    {
        base.OnInit();
    }

    protected override void Start()
    {
        this.CreateNormalBullet(this.NomalBullet, out this.NormalBullets, this.normalBulletPool);
        this.CreateNormalBullet(this.childBee, out this.childBees, this.childBeePool);
    }

    protected override void OnRun()
    {
        base.OnRun();
        float nowPass = Mathf.Floor(this.ElapsedTime * 10) / 10;
        if (this.Trans.position.y > ordinaryForwardBorder)
        {
            ForwardEnemy(ordinaryForwardBorder);
        }
        else
        {
            if (nowPass >= this.Pass)
            {
                if (this.barrageCount < this.barrage.Length)
                {
                    switch (this.barrage[this.barrageCount].ToString())
                    {
                        case "Fan":
                            this.fanNum = UnityEngine.Random.Range(5, 13);
                            this.Fan(fanNum);
                            break;
                        
                        case "Funnel":
                            this.Funnel();
                            break;
                    }

                    this.Pass += this.PassInterval;
                    barrageCount++;
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
            HideBullet(bullet);
            _normalBullets.Add(bullet);
        }

    }

    protected void AppearChildBee()
    {
        
    }

    protected void Fan(int _fanNum)
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

    protected void Funnel()
    {
        for (int i = 0; i < this.childBees.Count; i ++)
        {
            GameObject funnel = this.SearchAvailableBullet(this.childBees);
            funnel.transform.position = this.Trans.position;
            this.BulletAppear(funnel);
        }
    }

    protected override void OnWakeUp(Vector3 _position, Quaternion _rotation)
    {
    }
}
