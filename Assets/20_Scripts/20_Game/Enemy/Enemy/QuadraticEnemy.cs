using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public sealed class QuadraticEnemy : Enemy
{

    [SuppressMessage("ReSharper", "StyleCop.SA1401")]
    [System.Serializable]
    public class QuadraticEnemyDebugParam
    {
        // = -780.0f
        public float PassingX;
        // = 560.0f
        public float PassingY;
        // = -50.0f
        public float MaxX;
        // = 250.0f
        public float MaxY;
    }

	[SerializeField]
	private bool xAxis;
	[SerializeField]
	private Vector2 maxPos;

    // [SerializeField]
    // private QuadraticEnemyDebugParam debug = null;

    // protected override void Awake()
    // {
    //     base.Awake();
    // }

    /// <summary>
    /// 
    /// </summary>

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

        // ElapsedTimeはEnemyで宣言されているので、Enemy内で更新すべき
        // this.ElapsedTime += Time.deltaTime;

        // _
        QuadraticMove(this.Trans.position.x, this.Trans.position.y, this.maxPos.x, this.maxPos.y);
    }

    // protected override void OnDisable()
    // {
    //     base.OnDisable();
    // }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_passingX"></param>
    /// <param name="_passingY"></param>
    /// <param name="_maxX"></param>
    /// <param name="_maxY"></param>
    private void QuadraticMove(float _passingX, float _passingY, float _maxX, float _maxY)
    {
	    if (this.xAxis)
	    {
		    Debug.Log("あああ");
		    float progress = _passingY / ((_passingX - _maxX) * (_passingX - _maxX)) + _maxY;
		    Vector3 pos = this.Trans.position;

		    pos.x = -this.MoveSpeedRate * Time.deltaTime;
		    pos.y += (progress * (pos.x - _maxX) * (pos.x - _maxX)) + _maxY;
		    this.Trans.position = pos;
	    }
	    
	    if (!this.xAxis)
	    {
		    float progress = (_passingX - _maxX) / ((_passingY - _maxY) * (_passingY - _maxY));
		    Vector3 pos = this.Trans.position;

		    pos.y += -this.MoveSpeedRate * Time.deltaTime;
		    pos.x = (progress * (pos.y - _maxY) * (pos.y - _maxY)) + _maxX;
		    this.Trans.position = pos;
	    }
    }
}
