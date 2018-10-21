using UnityEngine;

namespace Ria
{
    public class InitializeAction : StateAction
    {
        private GameManager gm;

        public InitializeAction()
        {
        }

        public override void Start()
        {
            Debug.Log("InitializeAction_Start");
            this.gm = GameManager.Instance;

            this.gm.UFAManagerPL1.Init();
            this.gm.UFAManagerPL2.Init();
        }

        public override void Update()
        {
            //Debug.Log("InitializeAction_Update");
        }

        public override void End()
        {
            //Debug.Log("InitializeAction_End");
        }
    }
}