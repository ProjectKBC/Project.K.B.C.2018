/* Author: flanny7
 * Update: 2018/10/28
*/

using System.Collections.Generic;
using UnityEngine;

namespace RiaSpriteAnimation
{
	public sealed class RiaSpriteAnimator : MonoBehaviour
	{
		public bool IsStop() { return !this.currentAnim.IsPlaying; }

		[System.Serializable]
		public class AnimationMap
		{
			public string key;
			public RiaSpriteAnimation animation;
		}

		[SerializeField]
		private SpriteRenderer spRender = null;
		[SerializeField]
		private AnimationMap[] animations = null;

		private Dictionary<string, RiaSpriteAnimation> animDict = new Dictionary<string, RiaSpriteAnimation>();
		private RiaSpriteAnimation currentAnim = null;

		public void Init(string _firstAnimKey)
		{
			for (var i = 0; i < this.animations.Length; ++i)
			{
				var anim = this.animations[i];

				anim.animation.Init(this.spRender);

				// Dictionaryに映す 重いかな？？？ by flanny
				this.animDict.Add(anim.key, anim.animation);
			}

			this.currentAnim = this.animDict[_firstAnimKey];
			this.currentAnim.Play();
		}

		public void ChangeAnim(string _animKey)
		{
			Debug.Log("ChangeAnim: " + _animKey);
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
	}
}