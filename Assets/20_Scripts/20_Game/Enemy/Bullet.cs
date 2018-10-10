using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected static readonly Vector3 SpownPos = new Vector3(0, 0, 200);
    protected static readonly float Bottom = -80.0f;

    public float BulletSpeed = 10.0f;

    public GameObject Go { get; protected set; }
    public Transform Trans { get; protected set; }
    public float MyPositionX;
    public float MyPositionY;
    public float PlayerPositionX;
    public float PlayerPositionY;

    public void Awake()
    {
        this.Go = this.gameObject;
        this.Trans = this.Go.transform;
    }

    public void OnEnable()
    {
        this.MyPositionX = Trans.position.x;
        this.MyPositionY = Trans.position.y;
        this.PlayerPositionX = PlayerManager.GameObjectPl1.transform.position.x;
        this.PlayerPositionY = PlayerManager.GameObjectPl1.transform.position.y;
    }

    public void Update()
    {
        GoPlayer(this.MyPositionX, this.MyPositionY,
                  this.PlayerPositionX, this.PlayerPositionY);
        BeyondLine();
    }

    public void OnDisable()
    {

    }

    public void GoPlayer(float _enemyX, float _enemyY, float _playerX, float _playerY)
    {
        Vector3 pos = this.Trans.position;
        float differenceX;
        float differenceY;

        if (_enemyY > _playerY)
        {
            differenceX = _playerX - _enemyX;
            differenceY = _playerY - _enemyY;
            pos.y -= BulletSpeed * Time.deltaTime;
            pos.x = (differenceX * (pos.y - _enemyY) / differenceY) + _enemyX;
            this.Trans.position = pos;
        }
        else if (_playerY > _enemyY)
        {
            differenceX = _enemyX - _playerX;
            differenceY = _enemyY - _playerY;
            pos.y += BulletSpeed * Time.deltaTime;
            pos.x = (differenceX * (pos.y - _enemyY) / differenceY) + _enemyX;
            this.Trans.position = pos;
        }
        else
        {
            NomalAttack();
            Debug.Log("それはレア");
        }
    }

    public void NomalAttack()
    {
        Vector3 pos = this.Trans.position;
        pos.y += -BulletSpeed * Time.deltaTime;
        this.Trans.position = pos;
    }

    public void BeyondLine()
    {
        if (this.Trans.position.y < Bottom)
        {
            HideBullet();
        }
    }

    public void HideBullet()
    {
        this.Go.SetActive(false);
        this.Trans.position = SpownPos;
    }
}
