/* Author: flanny7
 * Update: 2018/10/28
*/

using System.Collections.Generic;
using UnityEngine;

namespace RiaSpriteAnimationSystem
{
	public sealed class RiaSpriteAnimator : MonoBehaviour
	{
		public bool IsStop { get { return !this.currentAnim.IsPlaying; } }
		public RiaSpriteAnimation[] Animations { get { return this.animations; } }
		
		[SerializeField]
		private SpriteRenderer spRender = null;
		[SerializeField]
		private RiaSpriteAnimation[] animations = null;

		private Dictionary<string, RiaSpriteAnimation> animDict = new Dictionary<string, RiaSpriteAnimation>();
		private RiaSpriteAnimation currentAnim = null;

		public void Init(string _firstAnimKey)
		{
			for (var i = 0; i < this.animations.Length; ++i)
			{
				var animation = this.animations[i];

				animation.Init(this.spRender);

				// Dictionaryに映す 重いかな？？？ by flanny
				this.animDict.Add(animation.KeyName, animation);
			}

			this.currentAnim = this.animDict[_firstAnimKey];
			this.currentAnim.Play();
		}

		public void ChangeAnim(string _animKey)
		{
			// Debug.Log("ChangeAnim: " + _animKey);
			this.currentAnim.Stop();
			this.currentAnim = this.animDict[_animKey];
			this.currentAnim.Play();
		}

		public void Run()
		{
			this.currentAnim.Run();
		}

		public void Stop()
		{
			this.currentAnim.Stop();
		}

		/// <summary>
		/// Animationをスクリプトからセットしたいときに使う
		/// </summary>
		/// <param name="_animations"></param>
		/// <param name="_firstAnimKey"></param>
		public void SetAnimations(RiaSpriteAnimation[] _animations, string _firstAnimKey)
		{
			this.animDict.Clear();
			this.animations = _animations;

			for (var i = 0; i < this.animations.Length; ++i)
			{
				var animation = this.animations[i];

				animation.Init(this.spRender);
				
				this.animDict.Add(animation.KeyName, animation);
				Debug.Log(animation.KeyName + ": " + this.animDict[animation.KeyName]);
			}

			this.currentAnim = this.animDict[_firstAnimKey];
			this.currentAnim.Play();
		}
	}
}