namespace RiaBehaviorSystem
{
    using UnityEngine;

    public abstract class RiaBehaviorManager<T> : SingletonMonoBehaviour<RiaBehaviorManager<T>> where T : RiaBehavior
    {
        [SerializeField]
        private GameObject[] objects = new GameObject[1];
        private T[] behaviors = null;

        protected GameObject[] Objects { get { return this.objects; } set { this.objects = value; } }
        protected T[] Behaviors { get { return this.behaviors; } set { this.behaviors = value; } }

        protected override void OnInit()
        {
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

            OnAwake();
        }

        private void Update()
        {
            for (int i = 0; i < behaviors.Length; ++i)
            {
                behaviors[i].Run();
            }

            OnUpdate();
        }

        protected abstract void OnAwake();
        protected abstract void OnUpdate();
    }

}