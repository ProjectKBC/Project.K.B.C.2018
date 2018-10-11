using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// ショットのボタンは仮の設定です
/// </summary>

public class AilosPlayer : PlayerMove
{
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

    private GameObject[] targetEnemys;
    private float currentTime = 0;
    private int searchCount = 0;
    // 弾を撃った後の経過時間
    private int normalShotTimeCount;

    private void Update()
    {
        base.Move();
        NormalShot();
        SpecialShot();
    }

    private void NormalShot()
    {
        if (Input.GetKey(normalShotKey))
        {
            CreateBullet();
        }
    }

    private void SpecialShot()
    {
        if (Input.GetKeyDown(specialShotKey))
        {
            currentTime = 0;
            searchCount = 0;
            targetEnemys = new GameObject[searchNumOfTimes];
        }

        if (Input.GetKey(specialShotKey))
        {
            if (NearSearchEnemy(spShotRange) == null || searchNumOfTimes <= searchCount) { return; }

            currentTime += Time.deltaTime;
            if (searchTime <= currentTime)
            {
                searchCount++;
                targetEnemys[searchCount - 1] = NearSearchEnemy(spShotRange);
                currentTime = 0;
            }
        }

        if (Input.GetKeyUp(specialShotKey))
        {
            for (int i = 0; i < searchCount; i++)
            {
                targetEnemys[i].SetActive(false);
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
        bool searchFlag = false;

        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(enemyTag))
        {
            // サーチ済みの敵は省く
            if(0 <= System.Array.IndexOf(targetEnemys, obs)) { continue; }
            float tmpDistance = Vector3.Distance(obs.transform.position, this.transform.position);
            if (tmpDistance <= _searchRadius)
            {
                searchEnemys.Add(Vector3.Distance(obs.transform.position, this.transform.position), obs);
                searchFlag = true;
            }

        }

        if (searchFlag)
        {
            targetEnemy = searchEnemys.First().Value;
        }
        return targetEnemy;
    }

}
