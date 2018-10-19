using UnityEngine;
// PlayerManagerに必要なパラメータ
// ・プレイヤーのオブジェクトにアクセス出来るプロパティ
// ・プレイヤーの操作キャラの名前
// ・プレイヤーの戦闘画面用の操作キー
// ・操作キーにアクセス出来るプロパティ
// 最初に行う処理
// ・操作キャラの名前を元にプレイヤーオブジェクトの生成
// ・プレイヤーオブジェクトを初期位置に設定
// ・どちらのプレイヤーの操作キャラなのかをタグで設定
// ・プレイヤーごとの操作キーの設定

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    public static GameObject GameObjectPl1 { get; private set; }
    public static GameObject GameObjectPl2 { get; private set; }

    // プレイヤーが選べるキャラクター
    public enum PlayerEnum
    {
        Veronica,
        Anoma,
    }

    [SerializeField]
    private PlayerEnum player1;
    [SerializeField]
    private PlayerEnum player2;

    [System.Serializable]
    public struct PlayerButtleKey
    {
        public KeyCode normalShotKey;
        public KeyCode specialShotKey;
        public KeyCode skillKey;
        public KeyCode moveUpKey;
        public KeyCode moveLeftKey;
        public KeyCode moveDownKey;
        public KeyCode moveRightKey;
    }

    public static PlayerButtleKey Pl1ButtleKey { get; private set; }
    public static PlayerButtleKey Pl2ButtleKey { get; private set; }

    [SerializeField]
    private PlayerButtleKey pl1ButtleKey;
    [SerializeField]
    private PlayerButtleKey pl2ButtleKey;

    
    protected override void OnInit()
    {
        string pl1Name = player1.ToString();
        string pl2Name = player2.ToString();

        // キャラの生成
        GameObjectPl1 = Instantiate((GameObject)Resources.Load("Prefabs/Character/" + pl1Name)) as GameObject;
        // GameObjectPl1.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        // GameObjectPl1.transform.position = new Vector3(-2.25f, 0, -2.25f);
        // GameObjectPl1.transform.rotation = Quaternion.Euler(90, 0, 0);
        GameObjectPl1.transform.position = new Vector3(-40, -40, 100);
        
        GameObjectPl2 = Instantiate((GameObject)Resources.Load("Prefabs/Character/" + pl2Name)) as GameObject;
        // GameObjectPl2.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        // GameObjectPl2.transform.position = new Vector3(2.25f, 0, -2.25f);
        // GameObjectPl2.transform.rotation = Quaternion.Euler(90, 0, 0);
        GameObjectPl2.transform.position = new Vector3(40, -40, 100);


        GameObjectPl1.tag = "Player1";
        Pl1ButtleKey = pl1ButtleKey;
        GameObjectPl2.tag = "Player2";
        Pl2ButtleKey = pl2ButtleKey;
    }
}