namespace RiaBehavior
{
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;

    public sealed class StraightRBEnemy : RBEnemy
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
        protected override void OnRun()
        {
            base.OnRun();
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

        protected override void OnWakeUp(Vector3 _position, Quaternion _rotation)
        {
        }
    }
}