/* Author : ketty
 * Last Update : 2018/09/07 flanny
 */

using System.Runtime.CompilerServices;
using UnityEngine;

namespace Ria
{
    using System.Diagnostics.CodeAnalysis;

    public enum EnemyPattern
    {
        StraightHorizontal,
        StraightVertical,
        Quadratic,
        Circle,
        Stay,
        Sin,
        Bee
    }

    public class PlayersEnemyData
    {
        public string PlayerType;
        public Vector3 EnemyStartPos;
        public int PoolEnemy;
        public int EnemyCount;

        public EnemyPattern[] EnemyPatterns;
        public GameObject[] StraightEnemys;
        public GameObject[] QuadraticEnemys;
        public GameObject[] CircleEnemys;
        public GameObject[] StayEnemys;
        public GameObject[] SinEnemys;
        public GameObject BeeEnemy;
        public float EnemyAppearPass;
        public float RightPosX;
        public float LeftPosX;

        public PlayersEnemyData(string _playerType, EnemyPattern[] _enemyPatterns, int _poolEnemy)
        {
            this.PlayerType = _playerType;
            this.EnemyPatterns = _enemyPatterns;
            this.PoolEnemy = _poolEnemy;
            this.StraightEnemys = new GameObject[this.PoolEnemy];
            this.QuadraticEnemys = new GameObject[this.PoolEnemy];
            this.CircleEnemys = new GameObject[this.PoolEnemy];
            this.StayEnemys = new GameObject[this.PoolEnemy];
            this.SinEnemys = new GameObject[this.PoolEnemy];

            this.BeeEnemy = new GameObject();
            this.EnemyCount = 0;
            this.EnemyAppearPass = 0;

            if (_playerType.Equals("Enemy1"))
            {
                this.EnemyStartPos = new Vector3(-42.5f, 60, 100);
                this.RightPosX = -3.0f;
                this.LeftPosX = -85.0f;
            }
            else if (_playerType.Equals("Enemy2"))
            {
                this.EnemyStartPos = new Vector3(42.5f, 60, 100);
                this.RightPosX = 85.0f;
                this.LeftPosX = 3.0f;
            }
        }
    }

    /// <summary>
    /// 敵の生成や出現を管理するクラス
    /// </summary>
    [System.Serializable]
    public class EnemyManager : MonoBehaviour
    {
        private static readonly int AppearZPos = 100;

        // 使われていないのでコメントアウトした by flanny
        // private static readonly int BulletPool = 100;

        /// <summary>
        /// 
        /// </summary>
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
        [SerializeField] private float interval = 5;
        private int poolEnemy = 10;

        [Space(16)] [SerializeField] private EnemyManagerDebugParam enemyManagerDebug = null;

        [SerializeField] private GameObject straightEnemy = null;
        [SerializeField] private GameObject quadraticEnemy = null;
        [SerializeField] private GameObject circleEnemy = null;
        [SerializeField] private GameObject stayEnemy = null;
        [SerializeField] private GameObject sinEnemy = null;

        [SerializeField] private GameObject beeEnemy = null;

        public EnemyPattern[] Enemy1Patterns;
        public EnemyPattern[] Enemy2Patterns;

        public PlayersEnemyData Enemy1Data;
        public PlayersEnemyData Enemy2Data;

        // private static EnemyManager instance = null;
        private float elapsedTime;

        private void Awake()
        {
            this.Enemy1Data = new PlayersEnemyData("Enemy1", Enemy1Patterns, this.poolEnemy);
            this.Enemy2Data = new PlayersEnemyData("Enemy2", Enemy2Patterns, this.poolEnemy);
            this.SetEnemy(this.Enemy1Data);
            this.SetEnemy(this.Enemy2Data);
            this.Enemy1Data.EnemyAppearPass = this.interval;
            this.Enemy2Data.EnemyAppearPass = this.interval;
        }

        private void Update()
        {
            this.elapsedTime += Time.deltaTime;

            // ここの計算がすごく怖い どういう意味？ by flanny
            var nowPass = Mathf.Floor(this.elapsedTime * 10) / 10;
            EnemyAppear(nowPass, this.Enemy1Data);
            EnemyAppear(nowPass, this.Enemy2Data);
        }

        public GameObject CreateEnemy(GameObject _obj)
        {
            GameObject enemy = Instantiate(_obj);
            enemy.SetActive(false);
            enemy.transform.parent = this.transform;
            return enemy;
        }

        private void SetEnemy(PlayersEnemyData _enemyData)
        {
            for (int i = 0; i < _enemyData.PoolEnemy; i++)
            {
                _enemyData.StraightEnemys[i] = CreateEnemy(this.straightEnemy);
                _enemyData.CircleEnemys[i] = CreateEnemy(this.circleEnemy);
                _enemyData.QuadraticEnemys[i] = CreateEnemy(this.quadraticEnemy);
                _enemyData.StraightEnemys[i].tag = _enemyData.PlayerType;
                _enemyData.CircleEnemys[i].tag = _enemyData.PlayerType;
                _enemyData.QuadraticEnemys[i].tag = _enemyData.PlayerType;

                _enemyData.StayEnemys[i] = CreateEnemy(this.stayEnemy);
                _enemyData.StayEnemys[i].tag = _enemyData.PlayerType;
                _enemyData.SinEnemys[i] = CreateEnemy(this.sinEnemy);
                _enemyData.SinEnemys[i].tag = _enemyData.PlayerType;
            }

            _enemyData.BeeEnemy = CreateEnemy(this.beeEnemy);
            _enemyData.BeeEnemy.tag = _enemyData.PlayerType;
        }

        public void EnemyAppear(float _nowPass, PlayersEnemyData _enemyData)
        {
            if (_nowPass >= _enemyData.EnemyAppearPass)
            {
                if (_enemyData.EnemyCount < _enemyData.EnemyPatterns.Length)
                {
                    switch (_enemyData.EnemyPatterns[_enemyData.EnemyCount].ToString())
                    {
                        case "StraightHorizontal":
                            StraightHorizontal(
                                5,
                                _enemyData,
                                this.enemyManagerDebug.StraightHorizontalPos,
                                this.enemyManagerDebug.StraightHorizontalPosInterval);
                            break;

                        case "StraightVertical":
                            StraightVertical();
                            break;

                        case "Quadratic":
                            Quadratic(
                                3,
                                _enemyData,
                                this.enemyManagerDebug.QuadraticPos,
                                this.enemyManagerDebug.QuadraticPosInterval);
                            break;

                        case "Circle":
                            Circle(
                                1,
                                _enemyData,
                                this.enemyManagerDebug.CirclePos,
                                this.enemyManagerDebug.CirclePosInterval);
                            break;
                        
                        case "Stay":
                            Stay(
                                5,
                                _enemyData,
                                this.enemyManagerDebug.StraightHorizontalPos,
                                this.enemyManagerDebug.StraightHorizontalPosInterval);
                            break;
                        
                        case "Sin":
                            Sin(
                                3,
                                _enemyData,
                                this.enemyManagerDebug.StraightHorizontalPos,
                                this.enemyManagerDebug.StraightHorizontalPosInterval);
                            break;

                        case "Bee":
                            Bee(
                                _enemyData);
                            break;
                    }

                    _enemyData.EnemyCount++;
                    _enemyData.EnemyAppearPass += this.interval;
                }
            }
        }

        // ここから下は出現させる関数、どの関数も似たようなこと書いてるから改善できそう

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_appearNum">敵機の登場数</param>
        /// <param name="_appearPos">登場する座標</param>
        /// <param name="_posInterval">登場の座標の間隔</param>
        public void StraightHorizontal(int _appearNum, PlayersEnemyData _enemyData, Vector2 _appearPos,
            Vector2 _posInterval)
        {
            int appearCount = 0;
            float appearSpace = System.Math.Abs(_enemyData.LeftPosX - _enemyData.RightPosX) / (_appearNum + 1);
            float appearRightPosX = _enemyData.RightPosX - appearSpace;
            float appearLeftPosX = _enemyData.LeftPosX + appearSpace;
            
            for (int i = 0; i < _enemyData.StraightEnemys.Length; i++)
            {
                if (appearCount >= _appearNum)
                {
                    break;
                }

                if (!_enemyData.StraightEnemys[i].activeSelf)
                {
                    // 起点に設置
                    _enemyData.StraightEnemys[i].transform.position =
                        new Vector3(appearLeftPosX + appearCount * appearSpace, _enemyData.EnemyStartPos.y, _enemyData.EnemyStartPos.z);
                    _enemyData.StraightEnemys[i].SetActive(true);
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
        public void Quadratic(int _appearNum, PlayersEnemyData _enemyData, Vector2 _appearPos, Vector2 _posInterval)
        {
            int appearCount = 0;
            float appearSpace = -20;
            float appearPosX = -90;
            float appearPosY = 40;

            if (_enemyData.PlayerType.Equals("Enemy1"))
            {
                appearPosX = -90.0f;
            }
            else if(_enemyData.PlayerType.Equals("Enemy2"))
            {
                appearPosX = -8.0f;
            }

            for (int i = 0; i < _enemyData.QuadraticEnemys.Length; i++)
            {
                if (appearCount >= _appearNum)
                {
                    break;
                }

                if (!_enemyData.QuadraticEnemys[i].activeSelf)
                {
                    // 起点に設置
                    _enemyData.QuadraticEnemys[i].transform.position =
                        new Vector3(appearPosX + appearCount * appearSpace, appearPosY, AppearZPos);
                    _enemyData.QuadraticEnemys[i].SetActive(true);
                    appearCount++;

                    // 次の起点の間隔をあける
                    _appearPos += _posInterval;
                }
            }
        }

        /*
        public void Appear(int _appearNum, GameObject[] _enemyPool, Vector2 _appearPos, Vector2 _posInterval)
        {
            int appearCount = 0;

            for (int i = 0; i < _enemyPool.Length; i ++)
            {
                if (appearCount >= _appearNum) { break; }

                if (_enemyPool[i].activeSelf)
                {
                    // 起点に設置
                    _enemyPool[i].transform.position = new Vector3 (_appearPos.x, _appearPos.y, AppearZPos);
                    _enemyPool[i].SetActive (true);
                    appearCount++;

                    // 次の起点の間隔をあける
                    _appearPos += _posInterval;
                }
            }
        }
        */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_appearNum">敵機の登場数</param>
        /// <param name="_appearPos">登場する座標</param>
        /// <param name="_posInterval">登場する座標の間隔</param>
        public void Circle(int _appearNum, PlayersEnemyData _enemyData, Vector2 _appearPos, Vector2 _posInterval)
        {
            int appearCount = 0;

            for (int i = 0; i < _enemyData.CircleEnemys.Length; i++)
            {
                if (appearCount >= _appearNum)
                {
                    break;
                }

                if (!_enemyData.CircleEnemys[i].activeSelf)
                {
                    // 起点に設置
                    _enemyData.CircleEnemys[i].transform.position = new Vector3(_appearPos.x, _appearPos.y, AppearZPos);
                    _enemyData.CircleEnemys[i].GetComponent<CircleEnemy>().CenterPos = this.transform.position;
                    _enemyData.CircleEnemys[i].SetActive(true);
                    appearCount++;

                    // 次の起点の間隔をあける
                    _appearPos += _posInterval;
                }
            }
        }

        public void Stay(int _appearNum, PlayersEnemyData _enemyData, Vector2 _appearPos,
            Vector2 _posInterval)
        {
            int appearCount = 0;
            float appearSpace = System.Math.Abs(_enemyData.LeftPosX - _enemyData.RightPosX) / (_appearNum + 1);
            float appearRightPosX = _enemyData.RightPosX - appearSpace;
            float appearLeftPosX = _enemyData.LeftPosX + appearSpace;
            
            for (int i = 0; i < _enemyData.StayEnemys.Length; i++)
            {
                if (appearCount >= _appearNum)
                {
                    break;
                }

                if (!_enemyData.StayEnemys[i].activeSelf)
                {
                    // 起点に設置
                    _enemyData.StayEnemys[i].transform.position =
                        new Vector3(appearLeftPosX + appearCount * appearSpace, _enemyData.EnemyStartPos.y, _enemyData.EnemyStartPos.z);
                    _enemyData.StayEnemys[i].SetActive(true);
                    appearCount++;
                    
                    // 次の起点の間隔をあける
                    _appearPos += _posInterval;
                }
            }
        }

        public void Sin(int _appearNum, PlayersEnemyData _enemyData, Vector2 _appearPos,
            Vector2 _posInterval)
        {
            int appearCount = 0;
            float appearSpace = -20;
            float appearPosX = -90;
            float appearPosY = 40;

            if (_enemyData.PlayerType.Equals("Enemy2"))
            {
                appearPosX = -2.0f;
            }

            for (int i = 0; i < _enemyData.SinEnemys.Length; i++)
            {
                if (appearCount >= _appearNum)
                {
                    break;
                }

                if (!_enemyData.SinEnemys[i].activeSelf)
                {
                    // 起点に設置
                    _enemyData.SinEnemys[i].transform.position =
                        new Vector3(appearPosX + appearCount * appearSpace, appearPosY, AppearZPos);
                    _enemyData.SinEnemys[i].SetActive(true);
                    appearCount++;
                    
                    // 次の起点の間隔をあける
                    _appearPos += _posInterval;
                }
            }
        }

        public void Bee(PlayersEnemyData _enemyData)
        {
            _enemyData.BeeEnemy.transform.position = _enemyData.EnemyStartPos;
            _enemyData.BeeEnemy.SetActive(true);
        }
    }
}