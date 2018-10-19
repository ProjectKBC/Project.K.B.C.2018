using UnityEngine;
// 現状スーパークラスに共通して必要なパラメータ
// ・通常ショットのキー
// ・特殊ショットのキー
// ・スキルのキー
// ・移動スピード
// ・HP
// ・エネミーのタグ(それぞれのプレイヤーがそれぞれのエネミーのみに干渉するように)
// ・相手プレイヤーのステータス(相手のステータスに干渉するスキル用)
// ・上記のパラメータにアクセス出来るプロパティ
// ・移動キー(上)
// ・移動キー(左)
// ・移動キー(下)
// ・移動キー(右)
// 最初に行う処理
// ・PlayerManagerから取得したキーの設定
// ・プレイヤーのエネミーの認識
// ・プレイヤーの相手プレイヤーのステータスの取得

public class PlayerData : MonoBehaviour
{
    protected KeyCode NormalShotKey { get { return this.normalShotKey; } set { this.normalShotKey = value; } }
    protected KeyCode SpecialShotKey { get { return this.specialShotKey; } set { this.specialShotKey = value; } }
    protected KeyCode SkillKey { get { return this.skillKey; } set { this.skillKey = value; } }
    public float MoveSpeed { get { return this.moveSpeed; } set { this.moveSpeed = value; } }
    public float HitPoint { get { return this.hitPoint; } set { this.hitPoint = value; } }
    public string EnemyTag { get { return this.enemyTag; } set { this.enemyTag = value; } }
    public PlayerData OpponentPlayer { get { return this.opponentPlayer; } set { this.opponentPlayer = value; } }

    private KeyCode normalShotKey;
    private KeyCode specialShotKey;
    private KeyCode skillKey;
    [SerializeField]
    private float moveSpeed = 30.0f;
    private float hitPoint = 5.0f;
    private string enemyTag;
    private PlayerData opponentPlayer;

    private KeyCode moveUpKey;
    private KeyCode moveLeftKey;
    private KeyCode moveDownKey;
    private KeyCode moveRightKey;

    protected virtual void Start()
    {
        switch(this.tag)
        {
            case "Player1":
                normalShotKey = PlayerManager.Pl1ButtleKey.normalShotKey;
                specialShotKey = PlayerManager.Pl1ButtleKey.specialShotKey;
                skillKey = PlayerManager.Pl1ButtleKey.skillKey;
                moveUpKey = PlayerManager.Pl1ButtleKey.moveUpKey;
                moveLeftKey = PlayerManager.Pl1ButtleKey.moveLeftKey;
                moveDownKey = PlayerManager.Pl1ButtleKey.moveDownKey;
                moveRightKey = PlayerManager.Pl1ButtleKey.moveRightKey;
                enemyTag = "Enemy1";
                opponentPlayer = PlayerManager.GameObjectPl2.GetComponent<PlayerData>();
                break;

            case "Player2":
                normalShotKey = PlayerManager.Pl2ButtleKey.normalShotKey;
                specialShotKey = PlayerManager.Pl2ButtleKey.specialShotKey;
                skillKey = PlayerManager.Pl2ButtleKey.skillKey;
                moveUpKey = PlayerManager.Pl2ButtleKey.moveUpKey;
                moveLeftKey = PlayerManager.Pl2ButtleKey.moveLeftKey;
                moveDownKey = PlayerManager.Pl2ButtleKey.moveDownKey;
                moveRightKey = PlayerManager.Pl2ButtleKey.moveRightKey;
                enemyTag = "Enemy2";
                opponentPlayer = PlayerManager.GameObjectPl1.GetComponent<PlayerData>();
                break;
        }
    }

    protected void Move()
    {
        if (Input.GetKey(this.moveUpKey)) { this.transform.position += Vector3.up * MoveSpeed * Time.deltaTime; }
        if (Input.GetKey(this.moveLeftKey)) { this.transform.position += Vector3.left * MoveSpeed * Time.deltaTime; }
        if (Input.GetKey(this.moveDownKey)) { this.transform.position += Vector3.down * MoveSpeed * Time.deltaTime; }
        if (Input.GetKey(this.moveRightKey)) { this.transform.position += Vector3.right * MoveSpeed * Time.deltaTime; }
    }

}