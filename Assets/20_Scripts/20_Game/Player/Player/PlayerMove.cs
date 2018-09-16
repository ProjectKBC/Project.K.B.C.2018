using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 30.0f;

    protected void Move()
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

}