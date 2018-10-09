using UnityEngine;

namespace Ria
{
    public abstract class TestChildManager
    {
        protected ScriptableObject scriptable = null;
        protected GameObject go = null;
        protected Transform trans = null;

        public void Init(GameObject _gameObject)
        {
            this.go = _gameObject;
            this.trans = this.go.transform;

            OnInit();
        }

        public void Init(GameObject _gameObject, ScriptableObject _scriptable)
        {
            scriptable = _scriptable;
            go = _gameObject;
            this.trans = this.go.transform;

            OnInit();
        }

        public void Run()
        {
            OnRun();
        }

        protected abstract void OnInit();
        protected abstract void OnRun();

    }
}