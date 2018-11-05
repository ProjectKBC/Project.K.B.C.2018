namespace RiaBehaviorSystem
{
    /* Author : ketty
 * Last Update : 2018/09/07 flanny
 */

    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;

    /// <summary>
    /// 敵の生成や出現を管理するクラス
    /// </summary>
    [System.Serializable]
    public class RBEnemyChildManager : RiaBehaviorChildManager<RBEnemy>
    {
        private static readonly int AppearZPos = 100;

        // 使われていないのでコメントアウトした by flanny
        // private static readonly int BulletPool = 100;

        /// <summary>
        ///
        /// </summary>
        public enum WavePattern
        {
            StraightHorizontal,
            StraightVertical,
            Quadratic,
            Circle
        }

        [SuppressMessage("ReSharper", "StyleCop.SA1401")]
        [System.Serializable]
        public class EnemyManagerDebugParam
        {
            // = (-475, transform.position.y);
            public Vector2 StraightHorizontalPos;

            // = (100, 0);
            public Vector2 StraightHorizontalPosInterval;

            // = (-780, 560);
            public Vector2 QuadraticPos;

            // = (0, 20);
            public Vector2 QuadraticPosInterval;

            // = (-375, transform.position.y);
            public Vector2 CirclePos;

            // = (100, 0);
            public Vector2 CirclePosInterval;
        }

        // 何のIntervalなのか教えて by flanny
        [SerializeField]
        private float interval = 5;

        // [SerializeField]
        // private GameObject straightEnemy = null;
        // [SerializeField]
        // private GameObject quadraticEnemy = null;
        // [SerializeField]
        // private GameObject circleEnemy = null;
        [SerializeField]
        private WavePattern[] enemyPatterns = new WavePattern[0];

        [Space(16)]
        [SerializeField]
        private EnemyManagerDebugParam enemyManagerDebug = null;

        [SerializeField]
        private StraightRBEnemy[] straightRBEnemies = new StraightRBEnemy[0];

        [SerializeField]
        private QuadraticRBEnemy[] quadraticRBEnemies = new QuadraticRBEnemy[0];

        [SerializeField]
        private CircleRBEnemy[] circleRBEnemies = new CircleRBEnemy[0];

        // private GameObject [] sEnemys;
        // private GameObject [] qEnemys;
        // private GameObject [] cEnemys;

        // private static EnemyManager instance = null;
        // private float elapsedTime;

        // もっと明快な名前つけてあげて by flanny
        // intervalごとに出すためのポイント的な
        private float pass;

        // 今何wave目かの目印
        private int enemyCount;

        protected override void OnAwake()
        {
            // this.sEnemys = new GameObject[10];
            // this.qEnemys = new GameObject[10];
            // this.cEnemys = new GameObject[10];

            // for (int i = 0; i < 10; i++)
            // {
            //     this.sEnemys[i] = CreateEnemy(straightEnemy);
            //     this.qEnemys[i] = CreateEnemy(quadraticEnemy);
            //     this.cEnemys[i] = CreateEnemy(circleEnemy);
            // }

            // 初期化
            for (int i = 0; i < straightRBEnemies.Length; ++i) { straightRBEnemies[i].Init(); }
            for (int i = 0; i < quadraticRBEnemies.Length; ++i) { quadraticRBEnemies[i].Init(); }
            for (int i = 0; i < circleRBEnemies.Length; ++i) { circleRBEnemies[i].Init(); }

            this.enemyCount = 0;
            this.pass = interval;
        }

        protected override void OnUpdate()
        {
            // RiaBehaviorChildManagerでやってる
            // this.elapsedTime += Time.deltaTime;

            // ここの計算がすごく怖い どういう意味？ by flanny
            var nowPass = Mathf.Floor(this.elapsedTime * 10) / 10;

            if (nowPass.Equals(this.pass))
            {
                if (enemyCount < enemyPatterns.Length)
                {
                    switch (enemyPatterns[enemyCount].ToString())
                    {
                        case "StraightHorizontal":
                            StraightHorizontal(
                                3,
                                this.enemyManagerDebug.StraightHorizontalPos,
                                this.enemyManagerDebug.StraightHorizontalPosInterval);
                            break;

                        case "StraightVertical":
                            StraightVertical();
                            break;

                        case "Quadratic":
                            Quadratic(
                                3,
                                this.enemyManagerDebug.QuadraticPos,
                                this.enemyManagerDebug.QuadraticPosInterval);
                            break;

                        case "Circle":
                            Circle(
                                1,
                                this.enemyManagerDebug.CirclePos,
                                this.enemyManagerDebug.CirclePosInterval);
                            break;
                    }

                    enemyCount++;
                    pass += interval;
                }
            }
        }

        // public GameObject CreateEnemy(GameObject _obj)
        // {
        //     var enemy = Instantiate(_obj);
        //     enemy.SetActive (false);
        //     enemy.transform.parent = this.transform;

        //     return enemy;
        // }

        // ここから下は出現させる関数、どの関数も似たようなこと書いてるから改善できそう

        /// <summary>
        ///
        /// </summary>
        /// <param name="_appearNum">敵機の登場数</param>
        /// <param name="_appearPos">登場する座標</param>
        /// <param name="_posInterval">登場の座標の間隔</param>
        public void StraightHorizontal(int _appearNum, Vector2 _appearPos, Vector2 _posInterval)
        {
            // int appearCount = 0;

            // for (int i = 0; i < this.sEnemys.Length; i++)
            // {
            //     if (appearCount >= _appearNum) { break; }

            //     if (!this.sEnemys[i].activeSelf)
            //     {
            //         // 起点に設置
            //         this.sEnemys[i].transform.position = new Vector3(_appearPos.x, _appearPos.y, AppearZPos);
            //         this.sEnemys[i].SetActive(true);
            //         appearCount++;

            //         // 次の起点の間隔をあける
            //         _appearPos += _posInterval;
            //     }
            // }

            int appearCount = 0;

            for (int i = 0; i < this.straightRBEnemies.Length; i++)
            {
                if (appearCount >= _appearNum) { break; }
                var enemy = straightRBEnemies[i];

                if (!enemy.Alive)
                {
                    // 起点に設置
                    enemy.WakeUp(new Vector3(_appearPos.x, _appearPos.y, AppearZPos), Quaternion.identity);
                    appearCount++;

                    // 次の起点の間隔をあける
                    _appearPos += _posInterval;
                }
            }
        }

        public void StraightVertical()
        {
            Debug.Log("StraightVerticalまだ書いてない！！！");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_appearNum">敵機の出現数</param>
        /// <param name="_appearPos">登場する座標</param>
        /// <param name="_posInterval">登場する座標の間隔</param>
        public void Quadratic(int _appearNum, Vector2 _appearPos, Vector2 _posInterval)
        {
            // int appearCount = 0;

            // for (int i = 0; i < this.qEnemys.Length; i++)
            // {
            //     if (appearCount >= _appearNum) { break; }

            //     if (!this.qEnemys[i].activeSelf)
            //     {
            //         // 起点に設置
            //         this.qEnemys[i].transform.position = new Vector3(_appearPos.x, _appearPos.y, AppearZPos);
            //         this.qEnemys[i].SetActive(true);
            //         appearCount++;

            //         // 次の起点の間隔をあける
            //         _appearPos += _posInterval;
            //     }
            // }

            int appearCount = 0;

            for (int i = 0; i < this.quadraticRBEnemies.Length; i++)
            {
                if (appearCount >= _appearNum) { break; }

                var enemy = quadraticRBEnemies[i];

                if (!enemy.Alive)
                {
                    // 起点に設置
                    enemy.WakeUp(new Vector3(_appearPos.x, _appearPos.y, AppearZPos), Quaternion.identity);
                    appearCount++;

                    // 次の起点の間隔をあける
                    _appearPos += _posInterval;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_appearNum">敵機の登場数</param>
        /// <param name="_appearPos">登場する座標</param>
        /// <param name="_posInterval">登場する座標の間隔</param>
        public void Circle(int _appearNum, Vector2 _appearPos, Vector2 _posInterval)
        {
            // int count = 0;

            // for (int i = 0; i < this.cEnemys.Length; i++)
            // {
            //     if (count >= _appearNum) { break; }

            //     if (!this.cEnemys[i].activeSelf)
            //     {
            //         // 起点に設置
            //         this.cEnemys [i].transform.position = new Vector3 (_appearPos.x, _appearPos.y, AppearZPos);
            //         this.cEnemys [i].GetComponent<CircleRBEnemy>().CenterPos = this.transform.position;
            //         this.cEnemys [i].SetActive (true);
            //         count++;

            //         // 次の起点の間隔をあける
            //         _appearPos += _posInterval;
            //     }
            // }

            int count = 0;

            for (int i = 0; i < this.circleRBEnemies.Length; i++)
            {
                if (count >= _appearNum) { break; }

                var enemy = circleRBEnemies[i];

                if (!enemy.Alive)
                {
                    // 起点に設置
                    enemy.WakeUp(new Vector3(_appearPos.x, _appearPos.y, AppearZPos), Quaternion.identity);
                    enemy.CenterPos = this.transform.position;
                    count++;

                    // 次の起点の間隔をあける
                    _appearPos += _posInterval;
                }
            }
        }
    }
}