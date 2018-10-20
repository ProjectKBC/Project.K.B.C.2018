using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// todo: スコアによるスキルのリキャストの実装
// todo: スムーズなアルファ値の設定
// このサブクラスに必要なパラメータ
// ・通常ショットのプレハブ
// ・通常ショットの弾のインターバル
// ・特殊ショットの弾の範囲
// ・１回のサーチにかかる時間
// ・サーチする回数
// ・特殊ショットのクールタイム
// ・スキルの効果時間
// ・通常ショットをインターバルの間隔で連射するためのカウント
// ・サーチした敵を入れる配列
// ・前回のサーチからの経過時間
// ・前回の特殊ショットからの経過時間
// ・現在のサーチ回数
// ・スキルの視覚妨害のためのアルファ値
// ・相手の移動速度
// ・スキルを発動してからの経過時間
// ・スキルを発動中かどうかのフラグ
// ・(便宜上)相手のスピードを変更するためのレート
// ・スキルの視覚妨害の最大アルファ値
// 最初に行う処理
// (・スーパークラスのスタートをオーバーライド)
// ・最初の特殊ショットを撃てるようにクールタイムを経過したことにする
// 常時行う処理
// ・スーパークラスのMoveの呼び出し
// ・通常ショットを撃つ関数の呼び出し
// ・特殊ショットを撃つ関数の呼び出し
// ・スキルを発動する関数の呼び出し

public class AilosPlayer : PlayerData
{
    private static readonly float AlphaMaxValue = 0.75f;

    [SerializeField]
    private GameObject normalBulletPrefab = null;
    [SerializeField]
    private int normalShotInterval = 3;
    [SerializeField]
    private float spShotRange = 30.0f;
    [SerializeField]
    private float searchTime = 0.5f;
    [SerializeField]
    private int searchNumOfTimes = 3;
    [SerializeField]
    private float spShotCoolTime = 1.5f;
    [SerializeField]
    private float skillTime = 5.0f;

    private int normalShotTimeCount;
    private GameObject[] targetEnemys;
    private float searchCoolTimeCount = 0;
    private float spShotCoolTimeCount = 0;
    private int searchCount = 0;
    private float alphaValue = 0;
    private float opponentMoveSpeed;
    private float skillTimeCount = 0;
    private bool isSkillFlag = false;
    private float speedChangeRate = 0;

    protected override void Start()
    {
        base.Start();
        spShotCoolTimeCount = spShotCoolTime;
    }

    private void Update()
    {
        base.Move();
        NormalShot();
        SpecialShot();
        Skill();
    }

    private void NormalShot()
    {
        if (Input.GetKey(NormalShotKey))
        {
            CreateBullet();
        }
    }

    private void SpecialShot()
    {
        if(spShotCoolTimeCount < spShotCoolTime)
        {
            spShotCoolTimeCount += Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(SpecialShotKey))
        {
            searchCoolTimeCount = 0;
            searchCount = 0;
            targetEnemys = new GameObject[searchNumOfTimes];
        }

        if (Input.GetKey(SpecialShotKey))
        {
            if (NearSearchEnemy(spShotRange) == null || searchNumOfTimes <= searchCount) { return; }

            searchCoolTimeCount += Time.deltaTime;
            if (searchTime <= searchCoolTimeCount)
            {
                searchCount++;
                targetEnemys[searchCount - 1] = NearSearchEnemy(spShotRange);
                searchCoolTimeCount = 0;
            }
        }

        if (Input.GetKeyUp(SpecialShotKey))
        {
            for (int i = 0; i < searchCount; i++)
            {
                targetEnemys[i].SetActive(false);
                spShotCoolTimeCount = 0;
            }
        }
    }

    private void Skill()
    {
        if (Input.GetKeyDown(SkillKey) && isSkillFlag == false)
        {
            isSkillFlag = true;
            opponentMoveSpeed = OpponentPlayer.MoveSpeed;
        } 

        if (isSkillFlag)
        {
            float beforeAlphaValue = alphaValue;
            // このアルファ値の変動で画面が上手い具合にフェードするか正直わかんない
            alphaValue = Mathf.Lerp(0, AlphaMaxValue, Time.deltaTime);
            // アルファ値の変動に伴う相手プレイヤーのスピードの調整もよくわかんない
            speedChangeRate = (beforeAlphaValue <= alphaValue) ? 1 - alphaValue : 1 + (alphaValue * 2);
            OpponentPlayer.MoveSpeed = OpponentPlayer.MoveSpeed * speedChangeRate;
            skillTimeCount += Time.deltaTime;

            if (skillTime <= skillTimeCount)
            {
                skillTimeCount = 0;
                alphaValue = 0;
                OpponentPlayer.MoveSpeed = opponentMoveSpeed;
                isSkillFlag = false;
            }
        }
    }

    private void CreateBullet()
    {
        normalShotTimeCount++;
        if (normalShotInterval < normalShotTimeCount)
        {
            normalShotTimeCount = 0;

            GameObject normalBullets = Instantiate(normalBulletPrefab);
            normalBullets.transform.position = this.transform.position;
        }
    }

    private GameObject NearSearchEnemy(float _searchRadius)
    {
        SortedDictionary<float, GameObject> searchEnemys = new SortedDictionary<float, GameObject>();
        GameObject targetEnemy = null;
        bool isSearchFlag = false;

        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(EnemyTag))
        {
            // サーチ済みの敵は省く
            if(0 <= System.Array.IndexOf(targetEnemys, obs)) { continue; }
            float tmpDistance = Vector3.Distance(obs.transform.position, this.transform.position);
            if (tmpDistance <= _searchRadius)
            {
                searchEnemys.Add(Vector3.Distance(obs.transform.position, this.transform.position), obs);
                isSearchFlag = true;
            }

        }

        if (isSearchFlag) { targetEnemy = searchEnemys.First().Value; }
        return targetEnemy;
    }

}
