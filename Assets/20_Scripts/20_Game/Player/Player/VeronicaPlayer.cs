using UnityEngine;

public class VeronicaPlayer : MonoBehaviour
{
    private static readonly float ShotMaxLp = 5.0f;

    // [SerializeField]
    // private int hitPoint = 100;
    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private GameObject normalBulletPrefab = null;
    [SerializeField]
    private float shotLp = 5;

    private VeronicaNB normalBullet;
    
    private void OnValidate()
    {
        this.shotLp = ShotMaxLp;
    }

    private void Update()
    {
        Move();
        NormalShot();
    }

    private void Move()
    {
        switch (this.tag)
        {
            case "Player1":
                if (Input.GetKey(KeyCode.W)) { this.transform.position += Vector3.up * this.moveSpeed * Time.deltaTime; }
                if (Input.GetKey(KeyCode.S)) { this.transform.position += Vector3.down * this.moveSpeed * Time.deltaTime; }
                if (Input.GetKey(KeyCode.D)) { this.transform.position += Vector3.right * this.moveSpeed * Time.deltaTime; }
                if (Input.GetKey(KeyCode.A)) { this.transform.position += Vector3.left * this.moveSpeed * Time.deltaTime; }
                break;

            case "Player2":
                if (Input.GetKey(KeyCode.UpArrow)) { this.transform.position += Vector3.up * this.moveSpeed * Time.deltaTime; }
                if (Input.GetKey(KeyCode.DownArrow)) { this.transform.position += Vector3.down * this.moveSpeed * Time.deltaTime; }
                if (Input.GetKey(KeyCode.RightArrow)) { this.transform.position += Vector3.right * this.moveSpeed * Time.deltaTime; }
                if (Input.GetKey(KeyCode.LeftArrow)) { this.transform.position += Vector3.left * this.moveSpeed * Time.deltaTime; }
                break;
        }
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
                    this.normalBullet = Instantiate(this.normalBulletPrefab).GetComponent<VeronicaNB>(); // 通常弾のキャッシュを生成
                    this.normalBullet.shooter = this.tag;
                }

                // z キーを押している間
                if (Input.GetKey(KeyCode.Z) && this.shotLp > 0)
                {
                    this.shotLp -= Time.deltaTime; // 弾を撃ってる間は弾のライフポイントが減る
                }

                // z キーを離している間
                if (!Input.GetKey(KeyCode.Z) && this.shotLp < ShotMaxLp)
                {
                    this.shotLp += Time.deltaTime; // 弾を撃ってない間は弾のライフポイントが増える
                    this.normalBullet.shooter = "none";
                }

                break;

            case "Player2":
                // m キーを押した時
                if (Input.GetKeyDown(KeyCode.M) && this.shotLp > 0)
                {
                    this.normalBulletPrefab.transform.position = this.transform.position;
                    this.normalBullet = Instantiate(this.normalBulletPrefab).GetComponent<VeronicaNB>(); // 通常弾のキャッシュを生成
                    this.normalBullet.shooter = this.tag;
                }

                // m キーを押している間
                if (Input.GetKey(KeyCode.M) && this.shotLp > 0)
                {
                    this.shotLp -= Time.deltaTime; // 弾を撃ってる間は弾のライフポイントが減る
                }

                // m キーを離している間
                if (!Input.GetKey(KeyCode.M) && this.shotLp < ShotMaxLp)
                {
                    this.shotLp += Time.deltaTime; // 弾を撃ってない間は弾のライフポイントが増える
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
