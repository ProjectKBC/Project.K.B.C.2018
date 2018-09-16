using UnityEngine;

public class GeneralPlayer : PlayerMove
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
        switch (this.tag)
        {
            case "Player1":
                // z キーを押している間
                if (Input.GetKey(KeyCode.Z))
                {
                    shotTimeCount++;
                    if (shotInterval < shotTimeCount)
                    {
                        shotTimeCount = 0;

                        GameObject normalBullets = Instantiate(normalBulletPrefab);
                        normalBullets.transform.position = this.transform.position;
                    }
                }

                break;

            case "Player2":
                // m キーを押している間
                if (Input.GetKey(KeyCode.M))
                {
                    shotTimeCount++;
                    if (shotInterval < shotTimeCount)
                    {
                        shotTimeCount = 0;

                        GameObject normalBullets = Instantiate(normalBulletPrefab);
                        normalBullets.transform.position = this.transform.position;
                    }
                }

                break;
        }
    }
}
