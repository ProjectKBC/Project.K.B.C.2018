using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 30.0f;

    protected KeyCode normalShotKey;
    protected KeyCode specialShotKey;
    protected KeyCode skillKey;
    protected KeyCode moveUpKey;
    protected KeyCode moveLeftKey;
    protected KeyCode moveDownKey;
    protected KeyCode moveRightKey;

    private void Start()
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
                break;

            case "Player2":
                normalShotKey = PlayerManager.Pl2ButtleKey.normalShotKey;
                specialShotKey = PlayerManager.Pl2ButtleKey.specialShotKey;
                skillKey = PlayerManager.Pl2ButtleKey.skillKey;
                moveUpKey = PlayerManager.Pl2ButtleKey.moveUpKey;
                moveLeftKey = PlayerManager.Pl2ButtleKey.moveLeftKey;
                moveDownKey = PlayerManager.Pl2ButtleKey.moveDownKey;
                moveRightKey = PlayerManager.Pl2ButtleKey.moveRightKey;
                break;
        }
    }

    protected void Move()
    {
        if (Input.GetKey(moveUpKey)) { this.transform.position += Vector3.up * moveSpeed * Time.deltaTime; }
        if (Input.GetKey(moveLeftKey)) { this.transform.position += Vector3.left * moveSpeed * Time.deltaTime; }
        if (Input.GetKey(moveDownKey)) { this.transform.position += Vector3.down * moveSpeed * Time.deltaTime; }
        if (Input.GetKey(moveRightKey)) { this.transform.position += Vector3.right * moveSpeed * Time.deltaTime; }
    }

}