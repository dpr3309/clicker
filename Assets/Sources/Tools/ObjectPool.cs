using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace Clicker.Tools
{
    public class ObjectPool<T>
    {
        private ConcurrentBag<T> pool;
        private Func<T> objectGenerator;

        public ObjectPool(Func<T> objectGenerator, int poolCapacity)
        {
            this.objectGenerator = objectGenerator;

            pool = new ConcurrentBag<T>();

            for (int i = 0; i < poolCapacity; i++)
            {
                pool.Add(objectGenerator());
            }
        }

        public T GetObject()
        {
            T item;
            if (pool.TryTake(out item))
                return item;
            return objectGenerator();
        }

        public void PutObject(T item)
        {
            pool.Add(item);
        }
    }
}