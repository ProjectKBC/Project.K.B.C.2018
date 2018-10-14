using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBee : Enemy
{
    protected Vector3[] FanPosition;
    private int FanCount = 9;

    protected override void Update()
    {
        base.Update();
        float nowPass = Mathf.Floor (this.ElapsedTime * 10) / 10;
        if (this.Trans.position.y > ordinaryForwardBorder)
        {
            ForwardEnemy(ordinaryForwardBorder);
        }
        else
        {
            if (nowPass >= this.Pass)
            {
                this.Fan();
            }
        }
    }

    protected void Fan()
    {
        
        for (int i = 0; i < this.FanCount; i++)
        {
            GameObject bullet = this.SearchAvailableBullet();
            bullet.transform.Rotate(new Vector3(0, 1, 0), -40 + i * 10);
            this.BulletAppear(bullet);
        }
        this.Pass += this.PassInterval;
    }
}