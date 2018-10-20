namespace RiaBehaviorSystem
{
    using UnityEngine;

    public abstract class RiaBehaviorChildManager<T> : SingletonMonoBehaviour<RiaBehaviorChildManager<T>> where T : RiaBehavior
    {
        [SerializeField]
        private GameObject[] objects = new GameObject[0];
        private T[] behaviors = null;

        protected GameObject[] Objects { get { return this.objects; } set { this.objects = value; } }
        protected T[] Behaviors { get { return this.behaviors; } set { this.behaviors = value; } }

        protected float elapsedTime;

        protected override void OnInit()
        {
            this.elapsedTime = Time.deltaTime;

            if (objects.Length == 0) { return; }

            behaviors = new T[objects.Length];
            for (int i = 0; i < objects.Length; ++i)
            {
                behaviors[i] = objects[i].GetComponent<T>();
                if (behaviors[i] == null)
                {
                    Debug.LogError("<color=#f00>" + objects[i].name + "has not " + typeof(T) + ".</color>", objects[i]);
                }

                behaviors[i].Init();
            }
            objects = null;
        }

        public void Init()
        {
            OnAwake();
        }

        public void Run()
        {
            this.elapsedTime += Time.deltaTime;

            if (behaviors != null)
            {
                for (int i = 0; i < behaviors.Length; ++i)
                {
                    behaviors[i].Run();
                }
            }

            OnUpdate();
        }

        protected abstract void OnAwake();
        protected abstract void OnUpdate();
    }

}