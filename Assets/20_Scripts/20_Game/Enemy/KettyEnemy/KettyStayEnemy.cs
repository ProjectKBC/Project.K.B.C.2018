using UnityEngine;

namespace RiaBehaviorSystem
{
    public sealed class KettyStayEnemy : KettyEnemy
    {
        [SerializeField] private float stayTime = 2.0f;

        //private float untilStayTime;

        protected override void OnInit()
        {
            base.OnInit();
        }

        protected override void Start()
        {
            CreateBullet(this.NomalBullet);
        }

        protected override void OnRun()
        {
            base.OnRun();
            float nowPass = Mathf.Floor(this.ElapsedTime * 10) / 10;

            if (nowPass >= this.stayTime)
            {
                base.BackMove();
            }
            else
            {
                if (this.Trans.position.y > ordinaryForwardBorder)
                {
                    ForwardEnemy(ordinaryForwardBorder);
                }

                if (nowPass >= this.Pass)
                {
                    if (IsBurstAttack)
                    {
                        BurstAttack();
                    }
                    else
                    {
                        NormalAtack();
                    }
                }
            }
        }

        protected override void OnWakeUp(Vector3 _position, Quaternion _rotation)
        {
        }
    }
}