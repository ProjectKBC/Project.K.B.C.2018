using UnityEngine;

public class GeneralPlayer : PlayerData
{
    [SerializeField]
    private GameObject normalBulletPrefab = null;
    [SerializeField]
    private int shotInterval = 3;

    // 弾を撃った後の経過時間
    private int shotTimeCount;

    private void Update()
    {
        base.Move();
        NormalShot();
    }

    private void NormalShot()
    {
        if (Input.GetKey(NormalShotKey))
        {
            CreateBullet();
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
}
