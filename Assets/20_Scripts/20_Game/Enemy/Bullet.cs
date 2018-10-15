using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        NormalAttack,
        GoPlayer
    }
    
    protected static readonly Vector3 SpownPos = new Vector3(0, 0, 200);

    protected static readonly float Top = 80.0f;
    protected static readonly float Bottom = -80.0f;
    protected float LeftPosX;
    protected float RightPosX;

    public float BulletSpeed = 10.0f;

    public GameObject Go { get; protected set; }
    public Transform Trans { get; protected set; }
    protected float MyAppearPositionX;
    protected float MyAppearPositionY;
    protected float PlayerPositionX;
    protected float PlayerPositionY;
    protected Vector3 PlayerPosition;
    protected Vector3 MyAppearPosition;
    protected Vector3 VectorMyselfPlayer;

    [SerializeField]
    private BulletType bulletType;

    protected virtual void Awake()
    {
        this.Go = this.gameObject;
        this.Trans = this.Go.transform;
    }

    protected virtual void Start()
    {
        if (this.tag.Equals("Enemy1"))
        {
            this.LeftPosX = -85.0f;
            this.RightPosX = -3.0f;
        }
        else if (this.tag.Equals("Enemy2"))
        {
            this.LeftPosX = 3.0f;
            this.RightPosX = 85.0f;
        }
    }

    public void OnEnable()
    {
        if (this.tag.Equals("Enemy1"))
        {
            this.PlayerPosition = PlayerManager.GameObjectPl1.transform.position;
        }
        else if (this.tag.Equals("Enemy2"))
        {
            this.PlayerPosition = PlayerManager.GameObjectPl2.transform.position;
        }
        this.VectorMyselfPlayer = new Vector3 (this.PlayerPosition.x - this.MyAppearPosition.x,
            this.PlayerPosition.y - this.MyAppearPosition.y, this.MyAppearPosition.y).normalized;
        /*
        this.MyAppearPositionX = Trans.position.x;
        this.MyAppearPositionY = Trans.position.y;
        this.PlayerPositionX = PlayerManager.GameObjectPl1.transform.position.x;
        this.PlayerPositionY = PlayerManager.GameObjectPl1.transform.position.y;
        */
    }

    protected virtual void Update()
    {
        /*
        GoPlayer(this.MyAppearPositionX, this.MyAppearPositionY,
            this.PlayerPositionX, this.PlayerPositionY);
            */
        //GoPlayer();
        switch (this.bulletType.ToString())
        {
            case "NormalAttack":
                NormalAttack();
                break;

            case "GoPlayer":
                GoPlayer();
                break;
        }
        BeyondLine();
    }

    public void OnDisable()
    {
    }
        
    /*
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
    */
    
    public void GoPlayer()
    {
        Vector3 myNowPos = this.Trans.position;

        myNowPos.x += (this.VectorMyselfPlayer.x * this.BulletSpeed * Time.deltaTime);
        myNowPos.y += (this.VectorMyselfPlayer.y * this.BulletSpeed * Time.deltaTime);
        //myNowPos.z += (this.vectorMyselfPlayer.z * this.BulletSpeed * Time.deltaTime);

        
        this.Trans.position = myNowPos;
        //this.Trans.position = Vector3.MoveTowards(this.Trans.position, this.PlayerPosition, 10.0f * Time.deltaTime);
    }
    
    public void NormalAttack()
    {
        Vector3 pos = this.Trans.position;
        pos.y += -BulletSpeed * Time.deltaTime;
        this.Trans.position = pos;
    }

    public void BeyondLine()
    {
        if (this.Trans.position.y < Bottom || this.Trans.position.y > Top
                                           || this.Trans.position.x < this.LeftPosX ||
                                           this.Trans.position.x > this.RightPosX)
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