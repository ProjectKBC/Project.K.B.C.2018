using UnityEngine;
// todo: 特殊ショットとスキルの実装！！！
// このサブクラスに必要なパラメータ
// ・通常ショットのプレハブ
// ・現在の通常ショットのライフポイント
// ・通常ショットのキャッシュ
// ・通常ショットの最大ライフポイント
// 最初に行う処理
// ・現在の通常ショットのライフポイントを最大ライフポイントにする
// 常時行う処理
// ・スーパークラスのMoveの呼び出し
// ・通常ショットを撃つ関数の呼び出し

public class VeronicaPlayer : PlayerData
{
    private static readonly float normalShotMaxLp = 5.0f;

    [SerializeField]
    private GameObject normalBulletPrefab = null;
    [SerializeField]
    private float normalShotLp = 5.0f;

    private VeronicaNB normalBullet;

    private void OnValidate()
    {
        this.normalShotLp = normalShotMaxLp;
    }

    private void Update()
    {
        base.Move();
        NormalShot();
    }

    private void NormalShot()
    {
        if (Input.GetKeyDown(NormalShotKey) && 0 < this.normalShotLp)
        {
            CreateBullet();
        }

        if (Input.GetKey(NormalShotKey) && 0 < this.normalShotLp)
        {
            // 弾を撃ってる間は弾のライフポイントが減る
            this.normalShotLp -= Time.deltaTime;
        }

        if (!Input.GetKey(NormalShotKey) && this.normalShotLp < normalShotMaxLp)
        {
            // 弾を撃ってない間は弾のライフポイントが増える
            this.normalShotLp += Time.deltaTime;
            this.normalBullet.shooter = "none";
        }

        // 弾のライフポイントが切れた時
        if (this.normalShotLp <= 0)
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
