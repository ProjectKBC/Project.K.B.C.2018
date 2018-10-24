using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet
{
	public class RiaBullet : RiaCharacter
	{
		public RiaBullet(GameObject _go, RiaCharacterScript _script, PlayerNumber _playerNumber) : base(_go, _script, _playerNumber)
		{
		}

		protected override void OnInit()
		{
		}
		
		protected override void OnWait()
		{
		}

		protected override void OnPlay()
		{
		}

		protected override void OnEnd()
		{
		}
	}
}