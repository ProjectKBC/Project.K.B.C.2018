using UnityEngine;

/// <summary>
/// 取りあえず弾が出るだけです
/// </summary>
public class PlayerShot : MonoBehaviour
{
    // 通常弾のprefab
    [SerializeField]
    private GameObject normalBullet = null;
    // 特殊弾のprefab
    [SerializeField]
    private GameObject specialBullet = null;
    // 弾の発射点
    [SerializeField]
    private Transform muzzle = null;
    // 弾の速度
    [SerializeField]
    private float bulletSpeed = 1000;
    // ショットの間隔
    [SerializeField]
    private int shotInterval = 3;

    // 弾の間隔を管理
    private int timeCount;
    private float playerSpeed;

    // private float startTime;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        playerSpeed = GetComponent<PlayerTest>().Speed;
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {

        // z キーを押している間
        if (Input.GetKey(KeyCode.Z))
        {
            timeCount++;

            if (shotInterval < timeCount)
            {
                timeCount = 0;

                // 弾の複製
                GameObject normalBullets = Instantiate(normalBullet);

                Vector3 force = this.gameObject.transform.up * this.bulletSpeed;
                // Rigidbodyに力を加えて発射
                normalBullets.GetComponent<Rigidbody>().AddForce(force);
                // 弾の発射点を更新
                normalBullets.transform.position = muzzle.position;
            }
            // 発射してない時は次弾装填
            else
            {
                timeCount = shotInterval;
            }
        }
        // x:特殊ショットのつもりです
        /*if (Input.GetKeyDown(KeyCode.X))
        {
            Vector3 force;
            startTime = Time.time;

            GameObject specialBullets = GameObject.Instantiate(specialBullet) as GameObject; // 弾の複製

            GetComponent<PlayerTest>().bulletSpeed = 0;

            //force = this.gameObject.transform.up * bulletSpeed;
            //specialBullets.GetComponent<Rigidbody>().AddForce(force); // Rigidbodyに力を加えて発射
            specialBullets.transform.position = muzzle.position; // 弾の発射点を更新
        }
        if (Time.time - startTime > 0.5)
        {
            GetComponent<PlayerTest>().bulletSpeed = playerSpeed;
        }*/
    }
}
