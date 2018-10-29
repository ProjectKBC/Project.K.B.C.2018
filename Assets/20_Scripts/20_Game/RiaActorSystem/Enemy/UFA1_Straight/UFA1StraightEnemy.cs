/* Author : flanny7
 * Update : 2018/10/28
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Enemy
{
	public sealed class UFA1StraightEnemy : RiaEnemy
	{
		// Bullet関係
		

		public UFA1StraightEnemy(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
		}
		
		// メイン関数

		protected override void OnInit()
		{

		}

		protected override void OnWait()
		{

		}

		protected override void OnPlay()
		{
			// 攻撃処理
			this.Shot();

			// 移動処理
			this.Move();

			// 衝突処理
			this.Collision();

			// 生死処理
			this.DeadCheck();
		}

		protected override void OnEnd()
		{

		}

		// 
		
		protected override void Shot()
		{
		}

		protected override void Move()
		{
			this.Trans.position += Vector3.down * this.MoveSpeed * Time.deltaTime * 60.0f;
		}

		protected override void Dead()
		{
			// todo: 爆発FXの生成
			// todo: 撃破SE
		}
	}
}