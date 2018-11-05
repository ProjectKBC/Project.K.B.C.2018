using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace RiaBehaviorSystem
{
    public sealed class KettyQuadraticEnemy : KettyEnemy
    {
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

        // [SerializeField]
		// private QuadraticEnemyDebugParam debug = null;

        // protected override void Awake()
        // {
        //     base.Awake();
        // }

        /// <summary>
        /// 
        /// </summary>
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

            // ElapsedTimeはEnemyで宣言されているので、Enemy内で更新すべき
            // this.ElapsedTime += Time.deltaTime;

            // _
            QuadraticMove(this.Trans.position.x, this.Trans.position.y, -10, 0);
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
            float progress = (_passingX - _maxX) / ((_passingY - _maxY) * (_passingY - _maxY));
            Vector3 pos = this.Trans.position;

            pos.y += -this.MoveSpeedRate * Time.deltaTime;
            pos.x = (progress * (pos.y - _maxY) * (pos.y - _maxY)) + _maxX;
            this.Trans.position = pos;
        }

        protected override void OnWakeUp(Vector3 _position, Quaternion _rotation)
        {
        }
    }
}