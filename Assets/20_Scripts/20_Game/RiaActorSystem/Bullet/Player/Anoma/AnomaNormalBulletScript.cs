﻿using UnityEngine;
using RiaActorSystem;

namespace Game.Bullet.Player
{
	[CreateAssetMenu(menuName = "RiaActorSystem/PlayerBullet/AnomaNormalBulletScript", fileName = "AnomaNormalBulletScript")]
	public class AnomaNormalBulletScript : RiaPlayerBulletScript
	{
		[SerializeField]
		private Sprite sprite;

		public Sprite Sprite { get { return this.sprite; } }
	}
}