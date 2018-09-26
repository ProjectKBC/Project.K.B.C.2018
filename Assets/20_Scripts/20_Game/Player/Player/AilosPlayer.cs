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
                // x キーを押している間
                if (Input.GetKey(KeyCode.X))
                {
                    
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
}
