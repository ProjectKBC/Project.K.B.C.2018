using UnityEngine;

namespace Ria
{
    public class PlayAction : StateAction
    {
        private GameManager gm;

        public PlayAction()
        {
        }

        public override void Start()
        {
            Debug.Log("PlayAction_Start");
            this.gm = GameManager.Instance;
        }

        public override void Update()
        {
            //Debug.Log("PlayAction_Update");

            this.gm.TestRBManager.Run();
        }

        public override void End()
        {
            Debug.Log("PlayAction_End");
        }
    }
}