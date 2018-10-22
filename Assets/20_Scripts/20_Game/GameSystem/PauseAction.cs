using UnityEngine;

namespace Ria
{
    public class PauseAction : StateAction
    {
        //private GameManager gm;

        public PauseAction()
        {
        }

        public override void Start()
        {
            Debug.Log("PauseAction_Start");
            //this.gm = GameManager.Instance;
        }

        public override void Update()
        {
            //Debug.Log("PauseAction_Update");
        }

        public override void End()
        {
            //Debug.Log("PauseAction_End");
        }
    }
}