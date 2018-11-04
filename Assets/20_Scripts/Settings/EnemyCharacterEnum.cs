/* Author: flanny7
 * Update: 2018/10/30
*/ 

using System.ComponentModel;

public enum EnemyCharacterEnum
{
	[Description("UAF1StraightEnemy")]
	UAF1StraightEnemy,
	[Description("UAF1StayEnemy")]
	UAF1StayEnemy,
	[Description("UAF1OutToInSinEnemy")]
	UAF1OutToInSinEnemy,
	[Description("UAF1InToOutSinEnemy")]
	UAF1InToOutSinEnemy,
	[Description("UAF1OutToInEnemy")]
	UAF1OutToInEnemy,
	[Description("UAF1InToOutEnemy")]
	UAF1InToOutEnemy,
	[Description("length")]
	length_empty,
}