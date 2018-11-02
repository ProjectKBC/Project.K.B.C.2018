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
            Debug.Log("FinalizeAction_Start");
            this.gm = GameManager.Instance;
        }

        public override void Update()
        {
            //Debug.Log("FinalizeAction_Update");
	        FadeManager.Instance.LoadScene(1.0f, SceneEnum.Select.ToDescription());
        }

        public override void End()
        {
            //Debug.Log("FinalizeAction_End");
        }
    }
}