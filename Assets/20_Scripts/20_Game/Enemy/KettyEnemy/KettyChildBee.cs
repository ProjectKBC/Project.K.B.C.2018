using UnityEngine;

namespace RiaBehaviorSystem
{
    public sealed class KettyChildBee : KettyEnemy
    {
        [System.Serializable]
        class GoPosData
        {
            public float Second;
            public float PosX;
            public float PosY;

            [HideInInspector] public float Speed;
            [HideInInspector] public bool MoveFlag = false;

            public GoPosData(float _second, float _posX, float _posY)
            {
                this.Second = _second;
                this.PosX = _posX;
                this.PosY = _posY;
            }
        }

        [SerializeField] private GoPosData[] movePosDatas;

        // 上の配列を回すためのカウント変数
        private int movePosDataCount = 0;

        private Vector3 VectorMyselfPosition;

        //private int bulletPool = 10;

        protected override void OnInit()
        {
            base.OnInit();
        }

        protected override void Start()
        {
            this.CreateBullet(this.NomalBullet);
        }

        protected override void OnRun()
        {
            base.OnRun();
            //float nowPass = Mathf.Floor(this.ElapsedTime * 10) / 10;


            //Debug.Log(this.movePosDataCount < this.movePosData.Length);

            // 移動しきってなかったら
            if (this.movePosDataCount < this.movePosDatas.Length)
            {
                SecondMovePosition(this.movePosDatas[this.movePosDataCount]);
            }
        }

        private void SecondMovePosition(GoPosData _movePosDatas)
        {
            if (_movePosDatas.MoveFlag)
            {
                Vector3 myNowPos = this.Trans.position;

                myNowPos.x += (this.VectorMyselfPosition.x * _movePosDatas.Speed * Time.deltaTime);
                myNowPos.y += (this.VectorMyselfPosition.y * _movePosDatas.Speed * Time.deltaTime);

                this.Trans.position = myNowPos;

                if (this.VectorMyselfPosition.x >= 0 && this.VectorMyselfPosition.y >= 0)
                {
                    if (this.Trans.position.x >= _movePosDatas.PosX && this.Trans.position.y >= _movePosDatas.PosY)
                    {
                        _movePosDatas.MoveFlag = false;
                        this.movePosDataCount += 1;
                    }
                }
                else if (this.VectorMyselfPosition.x < 0 && this.VectorMyselfPosition.y >= 0)
                {
                    if (this.Trans.position.x < _movePosDatas.PosX && this.Trans.position.y >= _movePosDatas.PosY)
                    {
                        _movePosDatas.MoveFlag = false;
                        this.movePosDataCount += 1;
                    }
                }
                else if (this.VectorMyselfPosition.x >= 0 && this.VectorMyselfPosition.y < 0)
                {
                    if (this.Trans.position.x >= _movePosDatas.PosX && this.Trans.position.y < _movePosDatas.PosY)
                    {
                        _movePosDatas.MoveFlag = false;
                        this.movePosDataCount += 1;
                    }
                }
                else if (this.VectorMyselfPosition.x < 0 && this.VectorMyselfPosition.y < 0)
                {
                    if (this.Trans.position.x < _movePosDatas.PosX && this.Trans.position.y < _movePosDatas.PosY)
                    {
                        _movePosDatas.MoveFlag = false;
                        this.movePosDataCount += 1;
                    }
                }
            }
            else
            {
                float d = (float)System.Math.Sqrt(System.Math.Pow(_movePosDatas.PosX - this.Trans.position.x, 2)
                                                   + System.Math.Pow(_movePosDatas.PosY - this.Trans.position.y, 2));
                _movePosDatas.Speed = d / _movePosDatas.Second;
                this.VectorMyselfPosition = new Vector3(_movePosDatas.PosX - this.Trans.position.x,
                    _movePosDatas.PosY - this.Trans.position.y, this.Trans.position.z).normalized;
                _movePosDatas.MoveFlag = true;
            }
        }

        protected override void OnWakeUp(Vector3 _position, Quaternion _rotation)
        {
        }
    }
}