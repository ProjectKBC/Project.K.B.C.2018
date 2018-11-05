/* Author: flnany7
 * Update: 2018/11/2
*/


using UnityEngine;

namespace Game
{
	public class ResultAction : StateAction
	{
		private static readonly float intervel;
		
		private GameManager gm;
		private float elapsedTime = 0;
		private bool isInitialize = false;
		
		public ResultAction()
		{
		}

		public override void Start()
		{
			//Debug.Log("FinalizeAction_Start");
			this.gm = GameManager.Instance;
			this.elapsedTime = 0;
			this.isInitialize = false;

			//AudioManager.Instance.PlayBgm(BackGroundMusicEnum.);
		}

		public override void Update()
		{
			//Debug.Log("FinalizeAction_Update");
			this.elapsedTime += Time.deltaTime;

			if (isInitialize == false)
			{
				if (intervel <= this.elapsedTime)
				{
					this.gm.UIManager.ResultStart(this.gm.Winner);
					this.isInitialize = true;
				}
				else
				{
					return;
				}

			}

			this.gm.UIManager.ResultUpdate(this.gm.Winner);
		}

		public override void End()
		{
			//Debug.Log("FinalizeAction_End");
		}
	}
}