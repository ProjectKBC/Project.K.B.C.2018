using UnityEngine;

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
        GameObjectPl2.tag = "Player2";
    }
}