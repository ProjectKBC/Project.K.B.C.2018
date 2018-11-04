using UnityEngine;

namespace Game
{
    public class CutInAction : StateAction
    {
        private GameManager gm;

        public CutInAction()
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