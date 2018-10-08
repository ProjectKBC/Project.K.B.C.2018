using UnityEngine;
/// <summary>
/// ショットのボタンは仮の設定です
/// </summary>

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
        if (Input.GetKeyDown(normalShotKey) && 0 < this.shotLp)
        {
            CreateBullet();
        }

        if (Input.GetKey(normalShotKey) && 0 < this.shotLp)
        {
            // 弾を撃ってる間は弾のライフポイントが減る
            this.shotLp -= Time.deltaTime;
        }

        if (!Input.GetKey(normalShotKey) && this.shotLp < ShotMaxLp)
        {
            // 弾を撃ってない間は弾のライフポイントが増える
            this.shotLp += Time.deltaTime;
            this.normalBullet.shooter = "none";
        }

        // 弾のライフポイントが切れた時
        if (this.shotLp <= 0)
        {
            this.normalBullet.shooter = "none";
        }
    }

    private void CreateBullet()
    {
        this.normalBulletPrefab.transform.position = this.transform.position;
        // 通常弾のキャッシュを生成
        this.normalBullet = Instantiate(this.normalBulletPrefab).GetComponent<VeronicaNB>();
        this.normalBullet.shooter = this.tag;
    }
}
