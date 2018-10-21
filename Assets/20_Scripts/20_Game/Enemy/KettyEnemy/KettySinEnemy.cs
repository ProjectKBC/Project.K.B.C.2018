using UnityEngine;

namespace RiaBehaviorSystem
{
    public sealed class KettySinEnemy : KettyEnemy
    {
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
            if (this.Trans.position.y > ordinaryForwardBorder)
            {
                ForwardEnemy(ordinaryForwardBorder);
            }
            else
            {
                SinMove();
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

        private void SinMove()
        {
            Vector3 pos = this.Trans.position;
            pos.x += this.MoveSpeedRate * Time.deltaTime;
            pos.y = Mathf.Sin(pos.x / this.MoveSpeedRate) * this.MoveSpeedRate;
            this.Trans.position = pos;
        }

        protected override void OnWakeUp(Vector3 _position, Quaternion _rotation)
        {
        }
    }
}