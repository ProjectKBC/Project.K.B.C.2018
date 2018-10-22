/* Author : flanny7
 * Update : 2018/10/22
*/

using System.Linq;
using UnityEngine;

namespace RiaActorSystem
{
	public abstract class RiaActorManager : MonoBehaviour
	{
		[SerializeField]
		private RiaActor[] actors = new RiaActor[0];
		
		private bool isInit = false;

		public void Init()
		{
			for (int i = 0; i < actors.Length; ++i)
			{
				actors[i].Init();
			}

			this.isInit = true;

			this.OnInitialize();
		}

		public void Play()
		{
			if (!this.isInit) { Debug.LogError("Initializeされていません。", this.gameObject); return; }

			for (int i = 0; i < actors.Length; ++i)
			{
				actors[i].Play();
			}

			this.OnUpdate();
		}

		protected RiaActor GetFreeActor()
		{
			if (!this.isInit) { Debug.LogError("Initializeされていません。", this.gameObject); return null; }

			for (int i = 0; i < actors.Length; ++i)
			{
				if (!actors[i].IsActive)
				{
					return actors[i];
				}
			}

			Debug.LogWarning("キャパシティーを超えました", this.gameObject);
			return null;
		}

		protected RiaActor[] GetFreeActors(int _num)
		{
			if (!this.isInit) { Debug.LogError("Initializeされていません。", this.gameObject); return null; }
			
			RiaActor[] freeActors = this.actors.Where(x => !x.IsActive).Take(_num).ToArray<RiaActor>();

			if (freeActors.Length < _num)
			{
				Debug.LogWarning("キャパシティーを超えました", this.gameObject);
				return null;
			}

			return freeActors;
		}

		public RiaActor[] GetActiveActors()
		{
			if (!this.isInit) { Debug.LogError("Initializeされていません。", this.gameObject); return null; }

			return this.actors.Where(x => x.IsActive).ToArray<RiaActor>();
		}

		public RiaCharacter[] GetActiveCharacter()
		{
			if (!this.isInit) { Debug.LogError("Initializeされていません。", this.gameObject); return null; }

			return this.GetActiveActors().Select(x => x.Character).ToArray<RiaCharacter>();
		}

		protected abstract void OnInitialize();
		protected abstract void OnUpdate();
	}
}