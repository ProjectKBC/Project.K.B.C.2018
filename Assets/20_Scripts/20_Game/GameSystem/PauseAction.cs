using UnityEngine;

namespace Game
{
    public class PauseAction : StateAction
    {
        private GameManager gm;

        public PauseAction()
        {
        }

        public override void Start()
        {
            Debug.Log("PauseAction_Start");
            this.gm = GameManager.Instance;

			this.gm.UIManager.PauseStart();
        }

        public override void Update()
        {
			//Debug.Log("PauseAction_Update");

			this.gm.UIManager.PauseUpdate();
		}

        public override void End()
        {
			//Debug.Log("PauseAction_End");

			this.gm.UIManager.PauseEnd();
        }
    }
}