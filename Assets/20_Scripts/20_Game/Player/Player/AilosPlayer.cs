using System.Collections;
using UnityEngine;
/// <summary>
/// ショットのボタンは仮の設定です
/// </summary>

public class AilosPlayer : PlayerMove
{
    [SerializeField]
    private GameObject normalBulletPrefab = null;
    [SerializeField]
    private int shotInterval = 3;
    [SerializeField]
    private float spShotRange = 60.0f;
    [SerializeField]
    private float searchTime = 2.5f;
    [SerializeField]
    private int serchNumOfTimes = 3;

    private GameObject[] targetEnemys = new GameObject[3];

    // 弾を撃った後の経過時間
    private int shotTimeCount;

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
        switch (this.tag)
        {
            case "Player1":
                IEnumerator lockOn = LockOn(serchNumOfTimes);
                // x キーを押した瞬間
                if (Input.GetKeyDown(KeyCode.X))
                {
                    StartCoroutine(lockOn);
                }
                // x キーを離した瞬間
                if (Input.GetKeyUp(KeyCode.X))
                {
                    StopCoroutine(lockOn);
                    lockOn = LockOn(serchNumOfTimes);
                    for (int i = 0; i < targetEnemys.Length; i++)
                    {
                        Destroy(targetEnemys[i]);
                        targetEnemys[i] = null;
                    }
                }
                break;

            case "Player2":
                break;
        }
    }

    private void CreateBullet()
    {
        shotTimeCount++;
        if (shotInterval < shotTimeCount)
        {
            shotTimeCount = 0;

            GameObject normalBullets = Instantiate(normalBulletPrefab);
            normalBullets.transform.position = this.transform.position;
        }
    }

    private IEnumerator LockOn(int _serchNumOfTimes)
    {
        if (Input.GetKey(KeyCode.X))
        {
            for (int i = 0; i < _serchNumOfTimes; i++)
            {
                yield return new WaitForSeconds(searchTime);
                if (NearSearchEnemy(spShotRange) != null)
                {
                    Debug.Log(NearSearchEnemy(spShotRange));
                    targetEnemys[i] = NearSearchEnemy(spShotRange);
                }
            }
        }
    }

    private GameObject NearSearchEnemy(float _searchRadius)
    {
        GameObject targetEnemy = null;
        float tmpDistance = 0;
        float nearDistance = 0;

        foreach (GameObject obs in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            // サーチ済みの敵は省く
            if(0 <= System.Array.IndexOf(targetEnemys, obs)) { break; }
            tmpDistance = Vector3.Distance(obs.transform.position, this.transform.position);
            if (tmpDistance <= _searchRadius)
            {
                if (nearDistance == 0 || tmpDistance < nearDistance)
                {
                    nearDistance = tmpDistance;
                    targetEnemy = obs;
                }
            }
        }
        return targetEnemy;
    }
}
