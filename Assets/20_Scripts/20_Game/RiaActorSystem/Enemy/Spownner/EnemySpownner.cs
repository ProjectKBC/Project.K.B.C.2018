using Game.Enemy;
using UnityEditor;
using UnityEngine;

public abstract class EnemySpownner : ScriptableObject
{
	[SerializeField]
	private SpownPositionCatalog pl1Catalog = null;
	[SerializeField]
	private SpownPositionCatalog pl2Catalog = null;
	[SerializeField]
	protected float appearInterval;

	protected float elapsedTime;
	protected float appearTime;
	
	// spown position 系
	protected SpownPositionCatalog spownPos;

	protected float appearRightPosX;
	protected float appearLeftPosX;
	protected float topPosY;

	protected float topLeftPosX;
	protected float topRightPosX;

	protected int enemyCount;
	
	protected EnemyActorFactory factory = null;
	protected PlayerNumber playerNumber;
	protected EnemyActorManager manager = null;
	
	public void Init(EnemyActorFactory _factory, PlayerNumber _playerNumber, EnemyActorManager _manager)
	{
		this.elapsedTime = 0.0f;
		this.appearTime = 0.0f;
		this.topPosY = 60.0f;

		this.enemyCount = 0;
	
		this.factory = _factory;
		this.playerNumber = _playerNumber;
		this.manager = _manager;
		
		this.spownPos = (this.playerNumber == PlayerNumber.player1) ? this.pl1Catalog : this.pl2Catalog;

//
//		if (this.playerNumber.Equals(PlayerNumber.player1))
//		{
//			this.appearLeftPosX = -90.0f;
//			this.appearRightPosX = 8.0f;
//
//		}
//		else if (this.playerNumber.Equals(PlayerNumber.player2))
//		{
//			/*
//			this.topRightPosX = 78.0f;
//			this.topLeftPosX = 10.0f;
//			*/
//			
//			this.appearLeftPosX = -5.0f;
//			this.appearRightPosX = 90.0f;
//		}
		
	}
	
	public abstract void Spown();
}