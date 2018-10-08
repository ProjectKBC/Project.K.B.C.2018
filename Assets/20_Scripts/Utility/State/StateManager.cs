using System;
using System.Collections.Generic;

public class StateManager<T>
{
    private class State
    {
        private readonly Action startAction;
        private readonly Action updateAction;
        private readonly Action endAction;

        public State(Action _startAct = null, Action _updateAct = null, Action _endAct = null)
        {
            this.startAction = _startAct ?? delegate { };
            this.updateAction = _updateAct ?? delegate { };
            this.endAction = _endAct ?? delegate { };
        }

        public void StartAction()
        {
            this.startAction();
        }

        public void UpDateAction()
        {
            this.updateAction();
        }

        public void EndAction()
        {
            this.endAction();
        }
    }

    private Dictionary<T, State> stateTable = new Dictionary<T, State>();
    private State current;

    public void Add(T _key, Action _startAct = null, Action _updateAct = null, Action _endAct = null)
    {
        this.stateTable.Add(_key, new State(_startAct, _updateAct, _endAct));
    }

    public void Add(T _key, StateAction _act)
    {
        this.stateTable.Add(_key, new State(_act.Start, _act.Update, _act.End));
    }

    public void SetState(T _key)
    {
        if (this.current != null)
        {
            this.current.EndAction();
        }

        this.current = this.stateTable[_key];
        this.current.StartAction();
    }

    public void Update()
    {
        if (this.current == null)
        {
            return;
        }

        this.current.UpDateAction();
    }

    public void Clear()
    {
        this.stateTable.Clear();
        this.current = null;
    }
}