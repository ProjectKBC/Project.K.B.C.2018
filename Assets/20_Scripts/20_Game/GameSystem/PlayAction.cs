using UnityEngine;

namespace Game
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

            this.gm.UFAManagerPL1.Play();
            this.gm.UFAManagerPL2.Play();
        }

        public override void End()
        {
            //Debug.Log("PlayAction_End");
        }
    }
}