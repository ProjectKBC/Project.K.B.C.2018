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
            this.gm = GameManager.Instance;
			this.gm.UIManager.PauseStart();
        }

        public override void Update()
        {
			this.gm.UIManager.PauseUpdate();
		}

        public override void End()
        {
			this.gm.UIManager.PauseEnd();
        }
    }
}