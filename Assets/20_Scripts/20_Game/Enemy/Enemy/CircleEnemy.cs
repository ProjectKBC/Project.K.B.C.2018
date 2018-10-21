using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class CircleEnemy : Enemy
{
    [SuppressMessage("ReSharper", "StyleCop.SA1401")]
    [System.Serializable]
    public class CircleEnemyDebugParam
    {
        // = 150.0f
        public float Radius;
        // // = (-375.0f, 600.0f)
        // public Vector2 centerPos;
        // = (0, 100.0f)
        public Vector2 EndStraightPos;
    }

    public Vector2 CenterPos { get { return centerPos; } set { this.centerPos = value; } }

    [SerializeField]
    private CircleEnemyDebugParam debug = null;

    private float rotationInterval = 0;
    private Vector2 centerPos = Vector2.zero;

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
        CreateBullet(this.NomalBullet);
    }
    
    protected override void Update()
    {
        base.Update();

        // ElapsedTimeはEnemyで宣言されているので、Enemy内で更新すべき
        // this.ElapsedTime += Time.deltaTime;

        // CircleMove(this.centerX, this.centerY, 150, rotationInterval);
        CircleMove(
            this.debug.Radius,
            this.debug.EndStraightPos);
    }

    // protected override void OnDisable()
    // {
    //     base.OnDisable();
    // }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_radius"></param>
    /// <param name="_endStraightPos"></param>
    /// <param name="_straightSpeed"></param>
    protected void CircleMove(float _radius, Vector2 _endStraightPos, float _straightSpeed = 10)
    {
        Vector3 pos = Trans.position;

        pos.x = this.centerPos.x + (_radius * Mathf.Cos(this.rotationInterval));
        pos.y = this.centerPos.y + (_radius * Mathf.Sin(this.rotationInterval));

        if (_endStraightPos.y < this.CenterPos.y)
        {
            var tmpPos = this.CenterPos;
            tmpPos.y += -1 * _straightSpeed * Time.deltaTime;
            this.CenterPos = tmpPos;
        }

        this.rotationInterval += this.MoveSpeedRate * Time.deltaTime;

        this.transform.position = pos;
    }
}