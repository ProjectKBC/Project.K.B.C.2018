// 参考：UNITYゲームプログラミングバイブル / 2Dゴム紐パチンコゲーム
// Copyright C 2018 Yugo Fujioka　All rights reserved.

/* Author: flanny7
 * Update: 2018/10/30
*/

using UnityEngine;

namespace RiaSpriteAnimationSystem
{
	[CreateAssetMenu(menuName = "RiaSpriteAnimation/Animation", fileName = "Animation")]
	public sealed class RiaSpriteAnimation : ScriptableObject
	{
		public bool IsPlaying { get { return this.isPlaying; } }
		public string KeyName { get { return this.keyName; } }

		[SerializeField, Tooltip("")]
		private string keyName = "";
		[SerializeField, Tooltip("")]
		private bool isLoop = true;
		[SerializeField, Tooltip("更新フレーム")]
		private int updateFrame;
		[SerializeField, Tooltip("コマアニメ")]
		private Sprite[] sprites = null;

		private bool isPlaying = false;
		private SpriteRenderer spRender = null;
		private int totalFrame = 0;
		private int currentFrame = 0;
		private float waitTime = 0f;
		private float waitTimeMax = 0f;
		
		public void Init(SpriteRenderer _spRender)
		{
			// 値の初期化 by flanny
			this.currentFrame = 0;
			this.waitTime = 0;

			// 総フレーム数の算出 by flanny
			this.totalFrame = this.sprites.Length;

			// キャッシュ by flanny
			this.spRender = _spRender;

			// フレームレートとフレーム時間の算出 by flanny
			var frameRate = (float)Application.targetFrameRate;
			if (frameRate < 0)
			{
				frameRate = (QualitySettings.vSyncCount == 2) ? 30f : 60f;
			}
			var frameTime = 1.0f / frameRate;

			// 次のコマへ進めるまでの待ち時間の設定 by flanny
			this.waitTimeMax = frameTime * updateFrame;
		}

		public void Play()
		{
			// 値の初期化 by flanny
			this.currentFrame = 0;
			this.waitTime = 0;

			this.isPlaying = true;
		}

		public bool Run()
		{
			// バリア by flanny
			if (!this.isPlaying) { return false; }

			// 待機時間の更新 by flanny
			this.waitTime -= Time.deltaTime;

			// floatであるwaitTimeが、0超過であるとき
			// すなわち、まだコマを進める時でない場合のバリア by flanny
			if (Vector3.kEpsilon < this.waitTime) { return true; }

			// 描画するコマを更新 by flanny
			this.spRender.sprite = this.sprites[this.currentFrame];

			// フレーム値の更新 by flanny
			++this.currentFrame;

			// ループする or ストップする by flanny
			if (this.totalFrame <= this.currentFrame)
			{
				if (this.isLoop)
				{
					this.currentFrame = 0;
				}
				else
				{
					this.currentFrame = this.totalFrame - 1;
					this.isPlaying = false;
				}
			}

			// 待機時間のリセット by flanny
			this.waitTime += this.waitTimeMax;

			return this.isPlaying;
		}

		public void Stop()
		{
			this.isPlaying = false;
		}
	}
}

//public static class RiaSpriteAnimationExtension
//{
//	public static T DeepCopy<T>(this T src)
//	{
//		using (var memoryStream = new System.IO.MemoryStream())
//		{
//			var binaryFormatter
//			  = new System.Runtime.Serialization
//					.Formatters.Binary.BinaryFormatter();
//			binaryFormatter.Serialize(memoryStream, src);
//			memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
//			return (T)binaryFormatter.Deserialize(memoryStream);
//		}
//	}
//}