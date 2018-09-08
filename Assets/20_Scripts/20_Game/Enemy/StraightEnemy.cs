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
        StraightMove();
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
