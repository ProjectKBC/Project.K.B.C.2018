using UnityEngine;

public class VeronicaPlayer : PlayerMove
{
    private static readonly float ShotMaxLp = 5.0f;

    [SerializeField]
    private GameObject normalBulletPrefab = null;
    [SerializeField]
    private float shotLp = 5.0f;

    private VeronicaNB normalBullet;

    private void OnValidate()
    {
        this.shotLp = ShotMaxLp;
    }

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
                // z キーを押した時
                if (Input.GetKeyDown(KeyCode.Z) && this.shotLp > 0)
                {
                    this.normalBulletPrefab.transform.position = this.transform.position;
                    // 通常弾のキャッシュを生成
                    this.normalBullet = Instantiate(this.normalBulletPrefab).GetComponent<VeronicaNB>();
                    this.normalBullet.shooter = this.tag;
                }

                // z キーを押している間
                if (Input.GetKey(KeyCode.Z) && this.shotLp > 0)
                {
                    // 弾を撃ってる間は弾のライフポイントが減る
                    this.shotLp -= Time.deltaTime;
                }

                // z キーを離している間
                if (!Input.GetKey(KeyCode.Z) && this.shotLp < ShotMaxLp)
                {
                    // 弾を撃ってない間は弾のライフポイントが増える
                    this.shotLp += Time.deltaTime;
                    this.normalBullet.shooter = "none";
                }

                break;

            case "Player2":
                // m キーを押した時
                if (Input.GetKeyDown(KeyCode.M) && this.shotLp > 0)
                {
                    this.normalBulletPrefab.transform.position = this.transform.position;
                    // 通常弾のキャッシュを生成
                    this.normalBullet = Instantiate(this.normalBulletPrefab).GetComponent<VeronicaNB>();
                    this.normalBullet.shooter = this.tag;
                }

                // m キーを押している間
                if (Input.GetKey(KeyCode.M) && this.shotLp > 0)
                {
                    // 弾を撃ってる間は弾のライフポイントが減る
                    this.shotLp -= Time.deltaTime;
                }

                // m キーを離している間
                if (!Input.GetKey(KeyCode.M) && this.shotLp < ShotMaxLp)
                {
                    // 弾を撃ってない間は弾のライフポイントが増える
                    this.shotLp += Time.deltaTime;
                    this.normalBullet.shooter = "none";
                }

                break;
        }

        // 弾のライフポイントが切れた時
        if (this.shotLp <= 0)
        {
            this.normalBullet.shooter = "none";
        }
    }
}
