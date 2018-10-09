﻿using System;
using UnityEngine;

namespace Ria
{
    public sealed class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public enum State
        {
            Initialize,
            Play,
            Pause,
            Finalize,

            Length,
        }

        // ステートマシン系
        private StateManager<State> stateManager = new StateManager<State>();
        private State currentState;
        private InitializeAction initAct = new InitializeAction();
        private PlayAction playAct = new PlayAction();
        private PauseAction pauseAct = new PauseAction();
        private FinalizeAction finalAct = new FinalizeAction();
        
        [SerializeField]
        private TestRiaBehaviorChildManager testRBManager = null; 
        public TestRiaBehaviorChildManager TestRBManager { get { return testRBManager; } }

        // Loading系

        // Play系

        // Pause系

        // Result系

        public void ChageState(State _state)
        {
            this.stateManager.SetState(_state);
        }

        protected override void OnInit()
        {
            this.stateManager.Add(State.Initialize, this.initAct);
            this.stateManager.Add(State.Play, this.playAct);
            this.stateManager.Add(State.Pause, this.pauseAct);
            this.stateManager.Add(State.Finalize, this.finalAct);

            this.SetState(State.Initialize);
        }
        
        private void Update()
        {
            this.stateManager.Update();

            if (Input.GetKeyDown(KeyCode.Return))
            {
                int tmpI = ((int)this.currentState + 1) % (int)State.Length;
                State tmp = (State)Enum.ToObject(typeof(State), tmpI);
                this.SetState(tmp);
            }
        }

        private void SetState(State _state)
        {
            this.currentState = _state;
            this.stateManager.SetState(_state);
        }
    }
}