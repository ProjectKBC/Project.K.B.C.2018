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

            //this.gm.TestRBManager.Init();
            this.gm.RBEnemyManager.Init();
        }

        public override void Update()
        {
            Debug.Log("InitializeAction_Update");
        }

        public override void End()
        {
            Debug.Log("InitializeAction_End");
        }
    }
}