using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IObjectPool<T> where T: class
{
    private Stack<T> pool;
    private Func<T> createFunc;
    private Action<T> actionOnGet;
    private Action<T> actionOnReturn;

    public ObjectPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnReturn = null)
    {
        pool = new Stack<T>();
        this.createFunc = createFunc;
        this.actionOnGet = actionOnGet;
        this.actionOnReturn = actionOnReturn;
    }

    public int CountAll { get; private set; }
    public int CountInactive => pool.Count;
    public int CountActive => CountAll - CountInactive;
  
    public T GetFromPool()
    {
        T result;

        if (pool.Count <= 0) // if there is none left in pool
        {
            result = createFunc(); // create one
            ++CountAll;
        }
        else // if there is one left in the pool
        {
            result = pool.Pop(); // take it out from pool
        }
        
        actionOnGet?.Invoke(result);

        return result;
    }

    public void ReturnToPool(T element)
    {
        actionOnReturn?.Invoke(element);
        pool.Push(element);
    }

    public void ClearPool()
    {
        pool.Clear();
        CountAll = 0;
    }

}
