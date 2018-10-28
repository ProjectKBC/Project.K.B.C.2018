using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public sealed class StraightEnemy : Enemy
{
    [SuppressMessage("ReSharper", "StyleCop.SA1401")]
    [System.Serializable]
    public class StraightEnemyDebugParam
    {
    }

	
	[SerializeField]
	private float ordinaryForwardBorder;
	

    // protected override void Awake()
    // {
    //     base.Awake();
    // }

    protected override void Awake()
    {
        base.Awake();
    }
    
    protected override void Start()
    {
        this.CreateBullet(this.NormalBullet);
    }

    protected override void Update()
    {
        base.Update();
        // this.ElapsedTime += UnityEngine.Time.deltaTime;

        // _
        //float nowPass = Mathf.Floor (this.ElapsedTime * 10) / 10;
        if (this.Trans.position.y > ordinaryForwardBorder)
        {
            this.ForwardEnemy(this.ordinaryForwardBorder);
        }
        else
        {
            this.StraightMove();
        }

	    this.GoBeyondLine();
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

	private void GoBeyondLine(float _border = 0.0f)
	{
		if (this.Trans.position.y < _border)
		{
			Vector3 pos = this.Trans.position;
			pos.y += -this.ordinaryForwardSpeed * Time.deltaTime;
			this.Trans.position = pos;
		}
	}

}
