using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class CircleEnemy : Enemy
{
    [SuppressMessage("ReSharper", "StyleCop.SA1401")]
    [System.Serializable]
    public class CircleEnemyParam
    {
        // = 150.0f
        public float Radius;
        // // = (-375.0f, 600.0f)
        // public Vector2 centerPos;
        // = (0, 100.0f)
	    
        public Vector2 CenterPoint;
    }

    public Vector2 CenterPos { get { return centerPos; } set { this.centerPos = value; } }

    [SerializeField]
    private CircleEnemyParam circleShape = null;

    private float rotationInterval = 0;
    private Vector2 centerPos = Vector2.zero;
	private bool forwardFlag = true;

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
        //base.Update();
	    this.ElapsedTime += Time.deltaTime;
	    this.ShotForm();
	    //Debug.Log(this.Trans.position.y + "あああ" + this.circleShape.CenterPoint.y);
	    if (this.Trans.position.y > this.circleShape.CenterPoint.y && this.forwardFlag)
	    {
		    this.YForwardEnemy(this.circleShape.CenterPoint.y);
	    }
	    else
	    {
		    this.forwardFlag = false;
		    this.CircleMove(this.circleShape.Radius);
	    }
	    this.Dead();
	    //NomalAttack ();
	    this.BeyondLine();
    }

    // protected override void OnDisable()
    // {
    //     base.OnDisable();
    // }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_radius"></param>
    protected void CircleMove(float _radius)
    {
        Vector3 pos = this.Trans.position;

        pos.x = this.circleShape.CenterPoint.x + _radius * (Mathf.Cos(this.rotationInterval));
        pos.y = this.circleShape.CenterPoint.y + _radius * (Mathf.Sin(this.rotationInterval));

	    /*
        if (_endStraightPos.y < this.CenterPos.y)
        {
            var tmpPos = this.CenterPos;
            tmpPos.y += -1 * _straightSpeed * Time.deltaTime;
            this.CenterPos = tmpPos;
        }
        */

        this.rotationInterval += this.MoveSpeedRate * Time.deltaTime;
	

        this.transform.position = pos;
    }
}