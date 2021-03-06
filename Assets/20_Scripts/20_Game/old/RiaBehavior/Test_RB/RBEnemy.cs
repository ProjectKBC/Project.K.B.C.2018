﻿namespace RiaBehaviorSystem
{
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class RBEnemy : RiaBehavior
    {
        // 定数はstatic readonlyに
        // public と protected はアクセサーに
        // gameobject と transform はキャッシュをとる by flanny
        protected static readonly Vector3 SpownPos = new Vector3(0, 0, 200);

        protected static readonly int BulletPool = 20;
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
        protected float Pass { get; private set; }

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

        private int burstCount;
        private float burstBulletInterval = 0.1f;

        protected override void OnInit()
        {
            this.Go = this.gameObject;
            this.Trans = this.Go.transform;

            this.HitPoint = this.fixHitPoint;
            this.ElapsedTime = 0.0f;
            this.NomalBullets = new List<GameObject>();

            for (int i = 0; i < BulletPool; i++)
            {
                this.NomalBullets.Add(CreateBullet(nomalBullet));
            }

            this.Pass = this.PassInterval;
            burstCount = burstBulletNumber;
        }

        protected override void OnRun()
        {
            ElapsedTime += Time.deltaTime;
            float nowPass = Mathf.Floor(this.ElapsedTime * 10) / 10;
            //if (nowPass.Equals(this.Pass)) // ==やEqualsだと値も型も同じなのに挙動がおかしい、見えない小数がある？今はまだ原因不明
            if (nowPass >= this.Pass)
            {
                if (IsBurstAttack)
                {
                    BurstAttack(nowPass);
                }
                else
                {
                    BulletAppear();
                    this.Pass += PassInterval;
                }
            }
            Dead();
            NomalAttack();
            BeyondLine();
        }

        protected override void OnSleep()
        {
            this.HitPoint = this.fixHitPoint;
            this.ElapsedTime = 0.0f;
            this.Pass = this.PassInterval;
        }

        public void Dead()
        {
            if (this.HitPoint <= 0)
            {
                HideEnemy();
            }
        }

        public void BeyondLine()
        {
            if (this.Trans.position.y < Bottom)
            {
                HideEnemy();
            }
        }

        public GameObject CreateBullet(GameObject _obj)
        {
            var bullet = Instantiate(_obj);
            bullet.transform.parent = this.Trans;
            HideBullet(bullet);
            return bullet;
        }

        public void BulletAppear()
        {
            for (int i = 0; i < this.NomalBullets.Count; i++)
            {
                if (!this.NomalBullets[i].gameObject.activeSelf)
                {
                    this.NomalBullets[i].transform.position = this.Trans.position;
                    this.NomalBullets[i].gameObject.SetActive(true);
                    break;
                }
            }
        }

        public void NomalAttack()
        {
            for (int i = 0; i < this.NomalBullets.Count; i++)
            {
                if (this.NomalBullets[i].gameObject.activeSelf)
                {
                    Vector3 pos = this.NomalBullets[i].transform.position;
                    pos.y -= nomalBulletSpeed;
                    this.NomalBullets[i].transform.position = pos;

                    if (this.NomalBullets[i].transform.position.y < Bottom)
                    {
                        HideBullet(this.NomalBullets[i]);
                    }
                }
            }
        }

        public void BurstAttack(float nowPass)
        {
            BulletAppear();
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

        public void TurnPlayer()
        {
            // Playerの方へ攻撃してくれる予定
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
}