/* Author: flanny7
 * Update: 2018/10/31
*/

using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	[CreateAssetMenu(menuName = "RiaActorSystem/PlayerBullet/AirosNormalBulletScript", fileName = "AirosNormalBulletScript")]
	public class AirosNormalBulletScript : RiaPlayerBulletScript
	{
		[SerializeField]
		private Sprite sprite;

		public Sprite Sprite { get { return this.sprite; } }
	}
}