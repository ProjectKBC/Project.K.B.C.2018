using UnityEngine;

public class GeneralPlayer : MonoBehaviour
{
    [SerializeField]
    private int hitPoint = 100;
    [SerializeField]
    private float moveSpeed = 3;
    [SerializeField]
    private GameObject normalBulletPrefab = null;
    [SerializeField]
    private int shotInterval = 3;

    // 弾の間隔を管理
    private int timeCount;

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
                if (Input.GetKey(KeyCode.W)) { this.transform.position += Vector3.up * moveSpeed * Time.deltaTime; }
                if (Input.GetKey(KeyCode.S)) { this.transform.position += Vector3.down * moveSpeed * Time.deltaTime; }
                if (Input.GetKey(KeyCode.D)) { this.transform.position += Vector3.right * moveSpeed * Time.deltaTime; }
                if (Input.GetKey(KeyCode.A)) { this.transform.position += Vector3.left * moveSpeed * Time.deltaTime; }
                break;

            case "Player2":
                if (Input.GetKey(KeyCode.UpArrow)) { this.transform.position += Vector3.up * moveSpeed * Time.deltaTime; }
                if (Input.GetKey(KeyCode.DownArrow)) { this.transform.position += Vector3.down * moveSpeed * Time.deltaTime; }
                if (Input.GetKey(KeyCode.RightArrow)) { this.transform.position += Vector3.right * moveSpeed * Time.deltaTime; }
                if (Input.GetKey(KeyCode.LeftArrow)) { this.transform.position += Vector3.left * moveSpeed * Time.deltaTime; }
                break;
        }
    }

    private void NormalShot()
    {
        switch (this.tag)
        {
            case "Player1":
                // z キーを押している間
                if (Input.GetKey(KeyCode.Z))
                {
                    timeCount++;
                    if (shotInterval < timeCount)
                    {
                        timeCount = 0;

                        // 弾の生成
                        GameObject normalBullets = Instantiate(normalBulletPrefab);
                        
                        // 弾の発射点を更新
                        normalBullets.transform.position = this.transform.position;
                    }
                }

                break;

            case "Player2":
                // m キーを押している間
                if (Input.GetKey(KeyCode.M))
                {
                    timeCount++;
                    if (shotInterval < timeCount)
                    {
                        timeCount = 0;

                        // 弾の生成
                        GameObject normalBullets = Instantiate(normalBulletPrefab);

                        // 弾の発射点を更新
                        normalBullets.transform.position = this.transform.position;
                    }
                }

                break;
        }
    }
}
