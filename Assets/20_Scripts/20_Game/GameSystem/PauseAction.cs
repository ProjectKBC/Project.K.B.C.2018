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

			this.gm.UIController.PauseStart();
        }

        public override void Update()
        {
			//Debug.Log("PauseAction_Update");

			this.gm.UIController.PauseUpdate();
		}

        public override void End()
        {
			//Debug.Log("PauseAction_End");

			this.gm.UIController.PauseEnd();
        }
    }
}