using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Spownner", fileName = "EnemyStage1Spownner")]
public class EnemyStage1Spownner : EnemySpownner
{
	[SerializeField] private EnemyPattern[] enemyPatterns;

	public enum EnemyPattern
	{
		StraightHorizontal,
		StraightVertical,
		LStraights,
		RStraights,
		ToLeftSide,
		ToRightSide,
		Quadratic,
		Circle,
		Coaster,
		Stay,
		Sin,
		OutToIn,
		InToOut,
		Bee
	}

	public override void Spown()
	{
		this.elapsedTime += Time.deltaTime;
		this.appearTime += Time.deltaTime;

		if (this.enemyCount < this.enemyPatterns.Length)
		{

			if (this.appearInterval <= this.appearTime )
			{
				int figs = 5;

				switch (this.enemyPatterns[this.enemyCount].ToString())
				{
					case "StraightHorizontal":
						for (var i = 1; i <= figs; i++)
						{
							var space = 10.0f;
							var equalPos = Mathf.Abs(this.spownPos.RightEdgeTop.x - this.spownPos.LeftEdgeTop.x) / (figs + 1);
							Debug.Log(this.topLeftPosX + i * equalPos);

							this.factory.Create(
								EnemyCharacterEnum.UAF1StraightEnemy,
								this.playerNumber,
								this.manager.GetFreeActorForSpowner(),
								new Vector3(this.spownPos.LeftEdgeTop.x + i * equalPos, this.topPosY, 0.0f)
							);
						}

						break;

					case "StraightVertical":


						break;

					/*
					case "LStraights":
						for (var i = 1; i <= figs; i++)
						{
							var space = 10.0f;
							var equalPos = Mathf.Abs(this.spownPos.RightEdgeTop.x - this.spownPos.LeftEdgeTop.x) / (figs + 1);

							
							this.factory.Create(
								EnemyCharacterEnum.UAF1StraightEnemy,
								this.playerNumber,
								this.manager.GetFreeActorForSpowner(),
								new Vector3(, this.topPosY + space * i, 0.0f)
							);
							
						}

						break;
					*/

					/*
					case "RStraights":
						for (var i = 1; i <= figs; i++)
						{
							var space = 10.0f;
							var equalPos = Mathf.Abs(this.topLeftPosX - this.topRightPosX) / (figs + 1);

							this.factory.Create(
								EnemyCharacterEnum.UAF1StraightEnemy,
								this.playerNumber,
								this.manager.GetFreeActorForSpowner(),
								new Vector3(this.topLeftPosX + i * equalPos, this.topPosY + space * i, 0.0f)
							);
						}

						break;
					*/

					case "ToLeftSide":
						for (var i = 1; i <= figs; i++)
						{
							var rand = Random.Range(0, 30);
							var space = 10.0f;
							var equalPos = Mathf.Abs(this.topLeftPosX - this.topRightPosX) / (figs + 1);

							this.factory.Create(
								EnemyCharacterEnum.UAF1ToLeftSideEnemy,
								this.playerNumber,
								this.manager.GetFreeActorForSpowner(),
								//new Vector3(this.appearRightPosX + i * space, rand, 0.0f)
								new Vector3(this.spownPos.RightEdgeTop.x + i * space, rand, 0.0f)
							);
						}

						break;

					case "ToRightSide":
						for (var i = 1; i <= figs; i++)
						{
							var rand = Random.Range(0, 30);
							var space = 10.0f;
							var equalPos = Mathf.Abs(this.topLeftPosX - this.topRightPosX) / (figs + 1);

							this.factory.Create(
								EnemyCharacterEnum.UAF1ToRightSideEnemy,
								this.playerNumber,
								this.manager.GetFreeActorForSpowner(),
								//new Vector3(this.appearLeftPosX + i * space, rand, 0.0f)
								new Vector3(this.spownPos.LeftEdgeTop.x + i * space, rand, 0.0f)
							);
						}

						break;

					case "Quadratic":

						break;

					case "Circle":

						break;

					case "Coaster":
						/*
						Coaster(
							20,
							_enemyData,
							this.enemyManagerAppearParam.Radius,
							this.enemyManagerAppearParam.CoasterPos,
							this.enemyManagerAppearParam.CoasterPosInterval);
							*/


						break;

					case "Stay":

						break;

					case "Sin":

						break;
					
					case "OutToIn":
						for (var i = 1; i <= figs; i++)
						{
							var rand = Random.Range(0, 30);
							var space = 10.0f;
							var x = 0.0f;
							if (this.playerNumber.Equals(PlayerNumber.player1))
							{
								x = this.spownPos.LeftEdgeTop.x;
								space = -5.0f;
							}
							else
							{
								x = this.spownPos.RightEdgeTop.x;
								space = 5.0f;
							}

							this.factory.Create(
								EnemyCharacterEnum.UAF1OutToInEnemy,
								this.playerNumber,
								this.manager.GetFreeActorForSpowner(),
								new Vector3(x + i * space, rand, 0.0f)
							);
						}

						break;
					
					case "InToOut":
						for (var i = 1; i <= figs; i++)
						{
							var rand = Random.Range(0, 30);
							var space = 10.0f;
							var x = 0.0f;
							if (this.playerNumber.Equals(PlayerNumber.player1))
							{
								x = this.spownPos.RightEdgeTop.x;
								space = 5.0f;
							}
							else
							{
								x = this.spownPos.LeftEdgeTop.x;
								space = -5.0f;
							}

							this.factory.Create(
								EnemyCharacterEnum.UAF1InToOutEnemy,
								this.playerNumber,
								this.manager.GetFreeActorForSpowner(),
								new Vector3(x + i * space, rand, 0.0f)
							);
						}
						
						break;

					case "Bee":

						break;
				}

				this.enemyCount += 1;
				this.appearTime = 0;
			}
		}
	}
}