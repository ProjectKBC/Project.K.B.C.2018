using UnityEngine;

namespace Game
{
    public class FinalizeAction : StateAction
    {
        private GameManager gm;

        public FinalizeAction()
        {
        }

        public override void Start()
        {
            this.gm = GameManager.Instance;
        }

        public override void Update()
		{
			FadeManager.Instance.LoadScene(2.0f, SceneEnum.Select.ToDescription());
        }

        public override void End()
        {
        }
    }
}