using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public sealed class StraightEnemy : Enemy
{
    [SuppressMessage("ReSharper", "StyleCop.SA1401")]
    [System.Serializable]
    public class StraightEnemyDebugParam
    {
    }

    // protected override void Awake()
    // {
    //     base.Awake();
    // }

    /// <summary>
    /// 
    /// </summary>
    protected override void Update()
    {
        base.Update();
        // this.ElapsedTime += UnityEngine.Time.deltaTime;

        // _
        float nowPass = Mathf.Floor (this.ElapsedTime * 10) / 10;
        if (this.Trans.position.y > ordinaryForwardBorder)
        {
            ForwardEnemy(ordinaryForwardBorder);
        }
        else
        {
            StraightMove();
            //NormalAtack();
        }
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
    }

    // protected override void OnDisable()
    // {
    //     base.OnDisable();
    // }

    /// <summary>
    /// 
    /// </summary>
    private void StraightMove()
    {
        var pos = this.Trans.position;
        pos.y += -this.MoveSpeedRate * Time.deltaTime;
        this.Trans.position = pos;
    }

}
