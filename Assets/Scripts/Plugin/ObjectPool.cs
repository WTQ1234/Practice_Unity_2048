// 用例：public ObjectPool<Ball> PoolOfBalls = new ObjectPool<Ball>(32);  Ball ball = ObjectPoolsManager.Instance.PoolOfBalls.New();
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResetable
{
    // 此接口的问题在于，初始化时无法传递参数
    void onPopObj(int i);
    void onStroeObj();
}

public class ObjectPool<T> where T : UnityEngine.Object, IResetable, new()
{
    public Stack<T> m_objectStack;

    private T m_prefab;
    private Transform m_parent;
    private Action<T> m_onPopAction;

    public ObjectPool(int size)
    {
        m_objectStack = new Stack<T>(size);
    }

    public void OnSetData(T prefab, Transform parent, Action<T> onPopAction = null)
    {
        m_prefab = prefab;
        m_parent = parent;
        m_onPopAction = onPopAction;
    }

    public T New(Action<T> m_onNewAction = null, int i = 0)
    {
        T t;
        if (m_objectStack.Count > 0)
        {
            t = m_objectStack.Pop();
        }
        else
        {
            if (m_prefab && m_parent)
            {
                t = GameObject.Instantiate<T>(m_prefab, m_parent);
            }
            else
            {
                t = new T();
            }

        }
        t.onPopObj(i);               // 此函数受接口限制无法传参
        m_onPopAction?.Invoke(t);   // 若对象池初始化时传入了action，则调用，用于带参数的统一初始化
        // 若New函数传入了action，则调用，用于带参数的特殊初始化
        // 但是，这种带参的action其实可以在New函数后直接执行，这种其实不是很必要并且会造成性能消耗
        m_onNewAction?.Invoke(t);
        return t;
    }

    public void Store(T obj)
    {
        obj.onStroeObj();
        m_objectStack.Push(obj);
    }
}
