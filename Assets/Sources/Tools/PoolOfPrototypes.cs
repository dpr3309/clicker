using UnityEngine;
using Zenject;

namespace Clicker.Tools
{
    public abstract class PoolOfPrototypes<T> : MonoBehaviour
        where T : Component
    {
        private ObjectPool<T> pool;

        private T prototype = null;

        private bool isConstructed = false;

        [Inject]
        private void Construct(T prototype)
        {
            if (isConstructed)
                throw new System.Exception($"[{GetType().Name}.Construct] object already constructed");

            this.prototype = prototype;

            isConstructed = true;
        }

        private void Awake()
        {
            pool = new ObjectPool<T>(
                () =>
                {
                    var instance = prototype.Clone(false);
                    instance.transform.SetParent(transform);
                    return instance;
                },
                10);
        }

        public T GetObject()
        {
            return pool.GetObject();
        }

        public void PutObject(T clone)
        {
            pool.PutObject(clone);
        }
    }
}