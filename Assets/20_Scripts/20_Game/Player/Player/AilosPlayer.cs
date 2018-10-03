using System.Collections;
using UnityEngine;
/// <summary>
/// ショットのボタンは仮の設定です
/// </summary>

public class AilosPlayer : PlayerMove {
    [SerializeField]
    private GameObject normalBulletPrefab = null;
    [SerializeField]
    private int shotInterval = 3;
    [SerializeField]
    private float spShotRange = 60.0f;
    [SerializeField]
    private float searchTime = 3.5f;
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
        switch (this.tag)
        {
            case "Player1":
                // z キーを押している間
                if (Input.GetKey(KeyCode.Z))
                {
                    CreateBullet();
                }

                break;

            case "Player2":
                // m キーを押している間
                if (Input.GetKey(KeyCode.M))
                {
                    CreateBullet();
                }

                break;
        }
    }

    private void SpecialShot()
    {
        switch (this.tag)
        {
            case "Player1":
                // x キーを押した瞬間
                if (Input.GetKeyDown(KeyCode.X))
                {
                    StartCoroutine( LockOn(serchNumOfTimes) );
                }
                if (Input.GetKeyUp(KeyCode.X))
                {
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
        if(Input.GetKey(KeyCode.X))
        {
            for (int i = 0; i < _serchNumOfTimes; i++)
            {
                yield return new WaitForSeconds(searchTime);
                if (NearSearchEnemy(spShotRange) != null)
                {
                    targetEnemys[i] = NearSearchEnemy(spShotRange);
                    Debug.Log(targetEnemys[i]);
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
